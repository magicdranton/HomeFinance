-- Setup AmountTo for transactions between same currency
UPDATE Transactions
SET AmountTo = AmountFrom
WHERE EXISTS
(
    SELECT 1 FROM Transactions T1 
    INNER JOIN Accounts FA ON FA.Id = T1.FromAccount
    INNER JOIN Accounts TA ON TA.Id = T1.ToAccount
    WHERE (FA.Currency = TA.Currency) AND (T1.Id = Transactions.Id)
);