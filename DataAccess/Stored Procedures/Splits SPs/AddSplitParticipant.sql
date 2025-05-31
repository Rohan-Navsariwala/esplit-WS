USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure AddSplitParticipant
	@SplitID int,
	@UserID int,
	@OweAmount decimal(10,2)
as
begin
	insert into SplitContacts (SplitID, SplitParticipantID, OweAmount, SplitStatus)
	values (@SplitID, @UserID,@OweAmount, '1');
end