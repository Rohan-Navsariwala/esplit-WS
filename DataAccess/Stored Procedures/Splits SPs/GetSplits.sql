USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetSplits
    @UserID INT,
    @SplitStatus int
AS
BEGIN
    IF @SplitStatus = '11' -- enum OWNED
    BEGIN
        SELECT *,@SplitStatus as SplitStatus
        FROM dbo.Splits
        WHERE CreatedBy = @UserID
          AND IsClosed = 0;
    END
    ELSE
    BEGIN
        SELECT s.*,c.SplitStatus
        FROM SplitContacts c
        INNER JOIN Splits s ON c.SplitID = s.SplitID
        WHERE c.SplitParticipantID = @UserID
          AND (
                (@SplitStatus = '12' AND c.SplitStatus IN ('1', '3')) OR -- 12 is for ALL case
                (@SplitStatus = '4' AND c.SplitStatus = @SplitStatus)
              ); -- 9 denotes all
    END
END
