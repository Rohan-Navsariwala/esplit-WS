USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure DeleteSplitParticipant
	@SplitID int,
	@UserID int
as
begin 
	delete from SplitContacts
	where SplitID = @SplitID and SplitParticipantID = @UserID
end