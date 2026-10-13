-- Select rows to be processed
DROP TABLE IF EXISTS TempTrans;

CREATE TEMPORARY TABLE TempTrans
(
    Id CHAR(70), 
    Reviewed BOOLEAN,
    FromAccount CHAR(3),
    ToAccount CHAR(3),
    Date DATETIME,  
    Amount DECIMAL(10,2), 
    Description TEXT, 
    Category CHAR(3),    
    ProcHint TEXT,    
    Color CHAR(50),
    IsDuplicate BOOLEAN,
    MCC CHAR(4),
    IsMerged BOOLEAN,
    MergedId CHAR(70),
    FromEndBalance DECIMAL(10,2),
    ToEndBalance DECIMAL(10,2),
    RowNum INTEGER
);

DROP TABLE IF EXISTS TmpResult;

CREATE TEMPORARY TABLE TmpResult
(
    Id CHAR(70),    
    FromEndBalance DECIMAL(10,2),
    ToEndBalance DECIMAL(10,2),
    FromAccount CHAR(3),
    ToAccount CHAR(3)
);

DROP TABLE IF EXISTS TmpVars;

CREATE TEMPORARY TABLE TmpVars
(
    MinDate DATETIME
);

-- Determine previous transaction to be given one. This is needed to get correct starting point to calculate EndBalances in chain
INSERT INTO TmpVars(MinDate)
SELECT Date
FROM Transactions T
WHERE ((T.FromAccount = 'AKB' AND T.FromEndBalance IS NOT NULL) OR (T.ToAccount = 'AKB') AND (T.ToEndBalance IS NOT NULL))
        AND T.Date < '2020-09-24 18:12:29'
ORDER BY T.Date DESC, T.Id DESC
LIMIT 1;

-- If MinDate was not found in Transactions then assume we do not have EndBalance in Transactions at all. Then, we will take StartBalance from Accounts table later on
UPDATE TmpVars
SET MinDate = 
(SELECT MIN(Date) FROM Transactions WHERE (FromAccount = 'AKB') OR (ToAccount = 'AKB'))
WHERE MinDate IS NULL;

SELECT *
FROM TmpVars;

-- Make basic Trans pool for EndBalance calculation
INSERT INTO TempTrans
SELECT     
    Id, 
    Reviewed,
    FromAccount,
    ToAccount,
    Date,
    Amount, 
    Description, 
    Category,    
    ProcHint,    
    '', -- Color
    0,  -- IsDuplicate
    MCC,
    IsMerged,
    MergedId,
    FromEndBalance,
    ToEndBalance,
    0   -- RowNum
FROM Transactions T
INNER JOIN TmpVars TV
WHERE (T.Date >= TV.MinDate)
    AND ((T.FromAccount = 'AKB') OR (T.ToAccount = 'AKB'))
    AND ((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))    
ORDER BY T.Date, T.Id ASC;

SELECT *
FROM TempTrans;

-- Generate sequential IDs
UPDATE TempTrans
SET RowNum = 
    (SELECT COUNT(1) FROM TempTrans T1 WHERE (T1.Date < TempTrans.Date) OR (T1.Date = TempTrans.Date) AND (T1.Id <= TempTrans.Id));
    
-- Calculate EndBalances in chain
WITH RECURSIVE _Tmp
(
    Id,
    Reviewed,
    FromAccount,
    ToAccount,
    Date,
    Amount,
    Description,
    Category,
    Color,
    ProcHint,
    IsDuplicate,
    MCC,
    IsMerged,
    MergedId,
    FromEndBalance,    
    ToEndBalance,
    RowNum
)
AS(SELECT
        TempTrans.Id,
        Reviewed,
        FromAccount,
        ToAccount,
        Date,
        Amount,
        Description,
        Category,
        Color,
        ProcHint,
        IsDuplicate,
        MCC,
        IsMerged,
        MergedId,
        case 
            when (TempTrans.FromAccount = 'AKB') then
                case
                    when (TempTrans.FromEndBalance IS NOT NULL) then TempTrans.FromEndBalance
                    else ACC.StartBalance - TempTrans.Amount
                end            
            else TempTrans.FromEndBalance            
        end as FromEndBalance,
        case 
            when (TempTrans.ToAccount = 'AKB') then
                case
                    when (TempTrans.ToEndBalance IS NOT NULL) then TempTrans.ToEndBalance
                    else ACC.StartBalance + TempTrans.Amount
                end            
            else TempTrans.ToEndBalance            
        end as ToEndBalance,
        RowNum
    FROM TempTrans
    INNER JOIN Accounts ACC ON ACC.Id = 'AKB'
    WHERE RowNum = 1
    UNION ALL
    SELECT
       TempTrans.Id,
       TempTrans.Reviewed,
       TempTrans.FromAccount,
       TempTrans.ToAccount,
       TempTrans.Date,
       TempTrans.Amount,
       TempTrans.Description,
       TempTrans.Category,
       TempTrans.Color,
       TempTrans.ProcHint,
       TempTrans.IsDuplicate,
       TempTrans.MCC,
       TempTrans.IsMerged,
       TempTrans.MergedId,
       case 
           when (TempTrans.FromAccount = 'AKB') then 
               case               
                   when (_Tmp.FromAccount = 'AKB') then _Tmp.FromEndBalance - TempTrans.Amount
                   else _Tmp.ToEndBalance - TempTrans.Amount                   
               end
           else TempTrans.FromEndBalance
       end AS FromEndBalance,
       case 
           when (TempTrans.ToAccount = 'AKB') then 
               case               
                   when (_Tmp.FromAccount = 'AKB') then _Tmp.FromEndBalance + TempTrans.Amount
                   else _Tmp.ToEndBalance + TempTrans.Amount                   
               end
           else TempTrans.ToEndBalance
       end as ToEndBalance,
       _Tmp.RowNum + 1
    FROM TempTrans
    INNER JOIN _Tmp on(_Tmp.RowNum + 1) = TempTrans.RowNum)
    
-- Save calculations into temp table
INSERT INTO TmpResult(Id, FromEndBalance, ToEndBalance, FromAccount, ToAccount)
SELECT Id, FromEndBalance, ToEndBalance, FromAccount, ToAccount FROM _Tmp;

-- Update EndBalances in Transactions table
UPDATE Transactions
SET FromEndBalance =     
(SELECT FromEndBalance FROM TmpResult T WHERE (T.Id = Transactions.Id) AND (T.FromAccount = 'AKB'))
WHERE Id IN
(
    SELECT Id FROM TmpResult
)
AND FromAccount = 'AKB';

UPDATE Transactions
SET ToEndBalance = 
(SELECT ToEndBalance FROM TmpResult T WHERE (T.Id = Transactions.Id) AND (T.ToAccount = 'AKB'))
WHERE Id IN
(
    SELECT Id FROM TmpResult
)
AND ToAccount = 'AKB';
