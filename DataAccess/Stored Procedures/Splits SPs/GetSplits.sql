CREATE PROCEDURE GetSplits
    @UserID INT,
    @SplitStatus VARCHAR(30)
AS
BEGIN
    IF @SplitStatus = 'OWNED'
    BEGIN
        SELECT *
        FROM dbo.Splits
        WHERE CreatedBy = @UserID
          AND isClosed = 0;
    END
    ELSE
    BEGIN
        SELECT s.*
        FROM SplitContacts c
        INNER JOIN Splits s ON c.SplitID = s.SplitID
        WHERE c.SplitParticipantID = @UserID
          AND (
                (@SplitStatus = 'ALL' AND c.SplitStatus IN ('PENDING_APPROVAL', 'APPROVED_UNPAID')) OR
                (@SplitStatus = 'APPROVED_PAID' AND c.SplitStatus = @SplitStatus)
              );
    END
END
