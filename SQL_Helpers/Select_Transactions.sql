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
    0,  -- EndBalance
    '', -- Color
    0,  -- IsDuplicate
    MCC,
    IsMerged,
    MergedId,
    0   -- RowNum
FROM Transactions T