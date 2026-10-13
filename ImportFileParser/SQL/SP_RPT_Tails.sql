DROP TABLE IF EXISTS TempTrans;

CREATE TEMPORARY TABLE TempTrans
(    
    Id CHAR(70),
    Date DATETIME, 
    YearMonth VARCHAR(6),
    DispDate VARCHAR(6),
    Account CHAR(3),        
    Balance DECIMAL(10,2),
    SeqRN INTEGER DEFAULT 0
);

-- Select initial Trans pool
INSERT INTO TempTrans (Id, Account, Date, YearMonth, DispDate, Balance)
SELECT    
    SelTrans.Id,
    SelTrans.Account,        
    SelTrans.Date,
    SelTrans.YearMonth,
    SelTrans.DispDate,
    SelTrans.Balance
FROM 
(
    SELECT
        T.Id,
        T.Date,
        CAST(STRFTIME('%Y%m', T.Date) AS integer) AS YearMonth,
        STRFTIME('%m/%Y', T.Date) AS DispDate,
        T.FromAccount AS Account,            
        T.FromEndBalance AS Balance
    FROM Transactions T
    WHERE ((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))
        AND (T.Date >='{FROM_DATE}' AND T.Date <'{TO_DATE}')
    UNION ALL
    SELECT
        T.Id,
        T.Date,
        CAST(STRFTIME('%Y%m', T.Date) AS integer) AS YearMonth,
        STRFTIME('%m/%Y', T.Date) AS DispDate,
        T.ToAccount,            
        T.ToEndBalance AS Balance
    FROM Transactions T
    WHERE ((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))
        AND (T.Date >='{FROM_DATE}' AND T.Date <'{TO_DATE}')
) SelTrans
INNER JOIN Accounts ACC
    ON ACC.Id = SelTrans.Account
WHERE 
    (ACC.IsExternal = 0)
    AND (ACC.Active = 1)
    AND (ACC.IsBuiltin = 0)
ORDER BY SelTrans.Date, SelTrans.Id ASC;

-- Find which Accounts not present in Trans Pool
DROP TABLE IF EXISTS TmpAbsentAccounts;

CREATE TEMPORARY TABLE TmpAbsentAccounts
(    
    Account CHAR(3),
    MaxDate DATETIME
);

INSERT INTO TmpAbsentAccounts(Account, MaxDate)
SELECT
    SelACC.Id AS Account,
    MAX(T.Date) AS MaxDate
FROM 
(
    SELECT
        ACC.Id,
        ACC.StartBalance
    FROM Accounts ACC
    WHERE (ACC.IsExternal = 0)
        --AND (ACC.Active = 1)
        AND (ACC.IsBuiltin = 0)
        AND NOT EXISTS (SELECT 1 FROM TempTrans T WHERE T.Account = ACC.Id)
) SelACC
LEFT JOIN Transactions T
    ON ((SelACC.Id = T.FromAccount) OR (SelACC.Id = T.ToAccount))
GROUP BY SelACC.Id;

-- Get pool of latest trans per absent account
DROP TABLE IF EXISTS TmpAbsentTransPool;

CREATE TEMPORARY TABLE TmpAbsentTransPool
(    
    Id CHAR(70),
    Date DATETIME,     
    Account CHAR(3),        
    Balance DECIMAL(10,2),
    SeqRN INTEGER DEFAULT 0
);

INSERT INTO TmpAbsentTransPool(Id, Date, Account, Balance)
SELECT
    T.Id,
    T.Date,
    TAA.Account,
    case
        when TAA.Account = T.FromAccount then T.FromEndBalance
        else T.ToEndBalance
    end AS Balance
FROM Transactions T
INNER JOIN TmpAbsentAccounts TAA
    ON (TAA.MaxDate = T.Date )
        AND ((TAA.Account = T.FromAccount) OR (TAA.Account = T.ToAccount));

-- Generate sequential IDs inside Account & Date to get latest balance
UPDATE TmpAbsentTransPool
SET SeqRN = 
    (SELECT COUNT(1) FROM TmpAbsentTransPool T1 
    WHERE (T1.Date = TmpAbsentTransPool.Date) 
        AND (T1.Account = TmpAbsentTransPool.Account) 
            AND ((T1.Id > TmpAbsentTransPool.Id))
    );    

-- Insert absent transactions into TempTrans table
INSERT INTO TempTrans(Id, Date, YearMonth, DispDate, Account, Balance)
SELECT 
    TATP.Id,
    '{FROM_DATE}',
    CAST(STRFTIME('%Y%m', '{FROM_DATE}') AS integer),
    STRFTIME('%m/%Y', '{FROM_DATE}'),
    TATP.Account,
    TATP.Balance
FROM TmpAbsentTransPool TATP
WHERE SeqRN = 0;  -- SeqRN = 0 means latest per-month transaction. This trans already contains pre-calculated accumulative Balance field

-- Insert accounts w/o transactions into TempTrans table
INSERT INTO TempTrans(Id, Date, YearMonth, DispDate, Account, Balance)
SELECT 
    '<empty>',
    '{FROM_DATE}',
    CAST(STRFTIME('%Y%m', '{FROM_DATE}') AS integer),
    STRFTIME('%m/%Y', '{FROM_DATE}'),
    TAA.Account,
    ACC.StartBalance
FROM TmpAbsentAccounts TAA
INNER JOIN Accounts ACC
    ON ACC.Id = TAA.Account
WHERE TAA.MaxDate IS NULL;

-- Generate sequential IDs inside Account & YearMonth values to find latest per-month per-account records
UPDATE TempTrans
SET SeqRN = 
    (SELECT COUNT(1) FROM TempTrans T1 
    WHERE (T1.YearMonth = TempTrans.YearMonth) 
        AND (T1.Account = TempTrans.Account) 
            AND ((T1.Date > TempTrans.Date) OR (T1.Date = TempTrans.Date) AND (T1.Id > TempTrans.Id))
    );    

-- Create Result table
DROP TABLE IF EXISTS TempTailEntries;

CREATE TEMPORARY TABLE TempTailEntries
(        
    SeqRN INTEGER DEFAULT 0,
    Account CHAR(3),    
    AccountName TEXT(15),  
    YearMonth VARCHAR(6),  
    DispDate VARCHAR(6),
    Balance DECIMAL(10,2),
    IsSavings BOOLEAN
);

-- Fill Result table from TempTrans
INSERT INTO TempTailEntries (Account, AccountName, YearMonth, DispDate, Balance, IsSavings)
SELECT
    TT.Account,
    ACC.Name,
    TT.YearMonth, 
    TT.DispDate,
    case
        when ACC.Currency = 'USD' then TT.Balance * {CFG_DollarRate}
        when ACC.Currency = 'EUR' then TT.Balance * {CFG_EuroRate}
        else TT.Balance
    end AS Balance,
    ACC.IsSavings
FROM TempTrans TT
INNER JOIN Accounts ACC
    ON ACC.Id = TT.Account
WHERE TT.SeqRN = 0;

-- Append TempTailEntries with absent Months
INSERT INTO TempTailEntries(Account, AccountName, YearMonth, DispDate, Balance, IsSavings)
SELECT    
    SelRowNums.Id,    
    SelRowNums.Name,
    SelRowNums.YearMonth,
    SelRowNums.DispDate,
    NULL,
    SelRowNums.IsSavings
FROM
(
    SELECT
        ACC.Id,
        ACC.Name,
        ACC.IsSavings,
        SelRowNums.YearMonth,
        SelRowNums.DispDate
    FROM Accounts ACC
    CROSS JOIN 
    (
        SELECT TT.YearMonth, TT.DispDate
        FROM TempTrans TT
        GROUP BY TT.YearMonth, TT.DispDate
     ) SelRowNums
    WHERE (ACC.IsExternal = 0) 
        --AND (ACC.Active = 1)
        AND (ACC.IsBuiltin = 0)
) SelRowNums
WHERE NOT EXISTS (SELECT 1 FROM TempTrans TT1 WHERE (TT1.YearMonth = SelRowNums.YearMonth) AND (TT1.Account = SelRowNums.Id));

-- Generate sequential IDs between YearMonth values
UPDATE TempTailEntries
SET SeqRN = 
    (SELECT COUNT(1) FROM TempTailEntries T1 WHERE (T1.Account = TempTailEntries.Account) AND (T1.YearMonth < TempTailEntries.YearMonth));

-- Calculate accumulative summary by account from month-to-month
WITH RECURSIVE _Tmp
(
    SeqRN,
    Account,
    AccountName,
    IsSavings,
    YearMonth,
    DispDate,
    Balance    
)
AS(
    SELECT
        SeqRN,
        Account,
        AccountName,
        IsSavings,
        YearMonth,
        DispDate,
        Balance
    FROM TempTailEntries    
    WHERE SeqRN = 0
    UNION ALL
    SELECT
        _Tmp.SeqRN + 1,
        TTE.Account,
        TTE.AccountName,
        TTE.IsSavings,
        TTE.YearMonth,
        TTE.DispDate,
        case
            when TTE.Balance IS NULL then _Tmp.Balance
            else TTE.Balance
        end
    FROM TempTailEntries TTE    
    INNER JOIN _Tmp 
        ON ((_Tmp.SeqRN + 1) = TTE.SeqRN)
            AND (_Tmp.Account = TTE.Account)
)

SELECT 
    SeqRN,
    Account AS AccountId,
    AccountName,
    IsSavings,
    DispDate AS Date,
    ROUND(Balance, 2) AS SumAmount
FROM _Tmp
ORDER BY YearMonth, AccountName;