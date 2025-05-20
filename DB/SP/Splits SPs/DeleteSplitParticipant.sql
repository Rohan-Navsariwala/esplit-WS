create procedure DeleteSplitParticipant
	@SplitID int,
	@UserID int
as
begin 
	delete from SplitContacts
	where SplitID = @SplitID and SplitParticipantID = @UserID
end