create procedure GetSplitParticipants
	@SplitID int
as
begin
	SELECT * FROM SplitContacts c
        INNER JOIN Users u ON c.SplitID = u.SplitID
		WHERE u.SplitID = @SplitID;
END