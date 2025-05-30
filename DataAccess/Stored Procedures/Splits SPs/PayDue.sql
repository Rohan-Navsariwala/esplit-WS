create procedure PayDue
	@UserID int,
	@SplitID int
as
begin
	update SplitContacts
	set SplitStatus = '3'
	where SplitID = @SplitID and SplitParticipantID = @UserID
end