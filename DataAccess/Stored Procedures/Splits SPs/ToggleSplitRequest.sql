create procedure ToggleSplitRequest
	@SplitID int,
	@UserID int,
	@Change int
as
begin 
	update SplitContacts
	set SplitStatus = @Change
	where SplitID = @SplitID and SplitParticipantID = @UserID;
end