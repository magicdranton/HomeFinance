DROP TABLE IF EXISTS TmpMsg;

CREATE TEMPORARY TABLE TmpMsg
(    
    Date DATETIME,
    Message TEXT
);

-- Check mandatory fields
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having empty FromAccount: ' || SelT.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE T.FromAccount = '' OR T.FromAccount IS NULL
) SelT
WHERE SelT.Cnt > 0;

INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having empty ToAccount: ' || SelT.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE T.ToAccount = '' OR T.ToAccount IS NULL
) SelT
WHERE SelT.Cnt > 0;

INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having empty Id: ' || SelT.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE T.Id = '' OR T.Id IS NULL
) SelT
WHERE SelT.Cnt > 0;

INSERT INTO TmpMsg
SELECT DATETIME(), 'Accounts having empty Currency: ' || SelA.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Accounts A
    WHERE A.Currency = '' OR A.Currency IS NULL
) SelA
WHERE SelA.Cnt > 0;


-- Check single-currency Trans Amounts consistency
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having diff AmountFrom vs AmountTo: ' || SelT.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    INNER JOIN Accounts FACC
        ON FACC.Id = T.FromAccount
    INNER JOIN Accounts TACC
        ON TACC.Id = T.ToAccount
    WHERE FACC.Currency = TACC.Currency
    AND T.AmountFrom <> T.AmountTo
) SelT
WHERE SelT.Cnt > 0;


-- Check Merged transactions fields consistency
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having MergedId and IsMerged=0: ' || SelT.Cnt
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE (IsMerged = 0) AND ((MergedId IS NOT NULL) OR MergedId <> '')
) SelT
WHERE SelT.Cnt > 0;

-- Check Merged transactions fields consistency
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having IsMerged=1 but no back reference from MergedId: ' || CAST(SelT.Cnt as text)
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE (IsMerged = 1) AND ((T.MergedId IS NULL) OR (T.MergedId = ''))
        AND NOT EXISTS
        (
            SELECT 1
            FROM Transactions T1
            WHERE T1.MergedId = T.Id
        )
) SelT
WHERE SelT.Cnt > 0;

INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having MergedId pointing nothing: ' || CAST(SelT.Cnt as text)
FROM
(
    SELECT  *
    FROM Transactions T
    WHERE T.MergedId NOT IN
    (
        SELECT Id
        FROM Transactions T1
    )
) SelT
WHERE SelT.Cnt > 0;

-- Check IsReviewed
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions having IsReviewed = 0: ' || CAST(SelT.Cnt as text)
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE (T.Reviewed = 0 OR T.Reviewed IS NULL)
        AND ((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))
) SelT
WHERE SelT.Cnt > 0;

-- Check Shop-to-Shop transactions
INSERT INTO TmpMsg
SELECT DATETIME(), 'Transactions Shop-to-Shop: ' || CAST(SelT.Cnt as text)
FROM
(
    SELECT COUNT(1) AS Cnt
    FROM Transactions T
    WHERE T.FromAccount IN ('NUL','DFL','SHP') AND
    T.ToAccount IN ('NUL','DFL','SHP')
) SelT
WHERE SelT.Cnt > 0;    

INSERT INTO TmpMsg
SELECT DATETIME(), 'DB consistency check finished.';

SELECT *
FROM TmpMsg