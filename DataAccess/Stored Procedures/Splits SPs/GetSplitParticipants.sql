create procedure GetSplitParticipants
	@SplitID int
as
begin
	SELECT * FROM SplitContacts c
        INNER JOIN Users u ON c.SplitParticipantID = u.UserID
		WHERE c.SplitID = @SplitID;
END