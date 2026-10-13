/*This script does find duplicate Alfa transactions (first trans was not reconciled and do not have constant Id. Second one has constant Id.
We need to copy constant Id to non-reconciled Trans to make it reconciled*/

DROP TABLE IF EXISTS TempTransConstIds;

CREATE TEMPORARY TABLE TempTransConstIds
(
    Id CHAR(70), 
    AmountFrom DECIMAL(10,2)
);

-- 1. Find Alfa Transactions which can have needed Id
INSERT INTO TempTransConstIds
SELECT     
    Id, 
    AmountFrom
FROM Transactions T
WHERE (T.FromAccount = '{Account}') AND (T.Reviewed = 0) AND (T.Id not like '{Account}_ % ');   -- TODO: no ALT codes must be hardcoded!! Move to a param

-- 2.Update Date of reconciled Transaction to make it replace non - reconclied one
 UPDATE Transactions
 SET Date =
 (
     SELECT Date
     FROM TempTransConstIds TT
    WHERE(TT.AmountFrom = Transactions.AmountFrom) AND(Transactions.Reviewed = 1) AND(Transactions.Id like '{Account}_%')
)
WHERE EXISTS
(
    SELECT Date
    FROM TempTransConstIds TT
    WHERE(TT.AmountFrom = Transactions.AmountFrom) AND(Transactions.Reviewed = 1) AND(Transactions.Id like '{Account}_%')
);

            --3.Delete non - reconciled Transactions
            DELETE FROM Transactions
WHERE EXISTS
(
    SELECT Date
    FROM TempTransConstIds TT
    WHERE(TT.AmountFrom = Transactions.AmountFrom) AND(Transactions.Reviewed = 1) AND(Transactions.Id like '{Account}_%')
); 