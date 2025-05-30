create procedure AddSplitParticipant
	@SplitID int,
	@UserID int,
	@OweAmount decimal(10,2)
as
begin
	insert into SplitContacts (SplitID, SplitParticipantID, OweAmount, SplitStatus)
	values (@SplitID, @UserID,@OweAmount, '0');
end