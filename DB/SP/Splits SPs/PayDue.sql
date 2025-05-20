create procedure PayDue
	@UserID int,
	@SplitID int
as
begin
	update SplitContacts
	set SplitStatus = 'APPROVED_PAID'
	where SplitID = @SplitID and SplitParticipantID = @UserID
end