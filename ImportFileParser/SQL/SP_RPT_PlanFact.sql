DROP TABLE IF EXISTS TempTrans;

CREATE TEMPORARY TABLE TempTrans
(    
    Category CHAR(3),
    Name TEXT(50),
    Count DECIMAL(10,2),
    Date CHAR(10),
    RowNum INTEGER    
);

-- select summary per category per month with ordering info
INSERT INTO TempTrans (Category, Name, Count, Date, RowNum)
SELECT 
    T1.Category, 
    C.Name, 
    ROUND(SUM(T1.Amount), 2) AS Count, 
    T1.Date, 
    T1.RowNum
FROM
(
    SELECT T.Category AS Category, 
        case
            when (FACC.IsExternal = 0) AND (TACC.IsExternal = 1) then 
                case
                    when FACC.Currency = 'RUR' then -T.AmountFrom
                    when TACC.Currency = 'RUR' then -T.AmountTo
                    else 0
                end
            when (FACC.IsExternal = 1) AND (TACC.IsExternal = 0) then 
                case
                   when FACC.Currency = 'RUR' then T.AmountFrom
                   when TACC.Currency = 'RUR' then T.AmountTo
                   else 0
                end
            else 0
        end AS Amount,
    STRFTIME('%m/%Y', T.Date) AS Date,
    CAST(STRFTIME('%Y%m', T.Date) AS integer) * 10 AS RowNum
    FROM Transactions T
    INNER JOIN Accounts FACC ON FACC.Id = T.FromAccount
    INNER JOIN Accounts TACC ON TACC.Id = T.ToAccount        
    WHERE 
        ((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))
        AND (T.Date >='{FROM_DATE}' AND T.Date <='{TO_DATE}')
        AND ('{CATEGORY}'='' OR (T.Category='{CATEGORY}'))
        AND (FACC.IsExternal <> TACC.IsExternal) = 1
) T1
INNER JOIN Categories C ON C.Id = T1.Category
    GROUP BY T1.Category, T1.Date;

-- Append selected summary with plan difference data per each month
INSERT INTO TempTrans (Category, Name, Count, Date, RowNum)
SELECT 
    TT.Category, 
    TT.Name, 
    CAST((TT.Count - C.PlannedValue) AS Decimal(10,2)) AS Count,
    TT.Date || ' Diff' AS Date,
    TT.RowNum + 1 AS RowNum
FROM TempTrans TT
INNER JOIN  Categories C
    ON C.Id = TT.Category;

-- Append summary with planned values
INSERT INTO TempTrans (Category, Name, Count, Date, RowNum)
SELECT 
C1.Id, C1.Name, CAST(C1.PlannedValue AS Decimal(10,2)), 'PLAN', 01
    FROM Categories C1
    WHERE C1.Id <> 'NUL';
    
SELECT *
FROM TempTrans
ORDER BY RowNum