USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure GetSplitParticipants
	@SplitID int
as
begin
	SELECT * FROM SplitContacts c
        INNER JOIN Users u ON c.SplitParticipantID = u.UserID
		WHERE c.SplitID = @SplitID;
END