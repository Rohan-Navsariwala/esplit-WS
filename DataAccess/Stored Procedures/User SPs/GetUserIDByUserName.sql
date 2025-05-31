USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetUserIDByUserName
	@UserName varchar(30)
as
begin
	select UserID from dbo.Users where UserName = @UserName;
end