create procedure ToggleSplitRequest
	@SplitID int,
	@UserID int,
	@Change varchar(30)
as
begin 
	update SplitContacts
	set SplitStatus = @Change
	where SplitID = @SplitID and SplitParticipantID = @UserID;
end