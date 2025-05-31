USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure PayDue
	@UserID int,
	@SplitID int
as
begin
	update SplitContacts
	set SplitStatus = '4' --PAID
	where SplitID = @SplitID and SplitParticipantID = @UserID
end