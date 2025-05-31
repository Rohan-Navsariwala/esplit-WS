USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure ToggleSplit
	@SplitID int,
	@UserID int,
	@SplitStatus int
as
begin 
	update SplitContacts
	set SplitStatus = @SplitStatus
	where SplitID = @SplitID and SplitParticipantID = @UserID;
end