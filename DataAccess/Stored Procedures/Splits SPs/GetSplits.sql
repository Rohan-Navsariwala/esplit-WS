CREATE PROCEDURE GetSplits
    @UserID INT,
    @SplitStatus int
AS
BEGIN
    IF @SplitStatus = '10' --denotes own
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
                (@SplitStatus = '9' AND c.SplitStatus IN ('0', '1')) OR
                (@SplitStatus = '2' AND c.SplitStatus = @SplitStatus)
              ); -- 9 denotes all
    END
END
