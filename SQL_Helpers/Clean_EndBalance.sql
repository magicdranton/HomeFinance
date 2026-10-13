UPDATE Transactions
SET FromEndBalance = NULL
WHERE FromAccount = 'VAD';

UPDATE Transactions
SET ToEndBalance = NULL
WHERE ToAccount = 'VAD';