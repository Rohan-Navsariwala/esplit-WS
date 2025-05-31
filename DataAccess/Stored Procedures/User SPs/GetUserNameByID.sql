USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetUserNameByID
	@UserID int
as
begin
	select UserName from dbo.Users where UserID = @UserID;
end