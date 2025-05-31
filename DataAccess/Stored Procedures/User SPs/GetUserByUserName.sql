USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByUserName]
	@UserName varchar(30)
AS
BEGIN
	SELECT UserID, UserName, FullName, PasswordHash, CreatedAt, IsActive 
	FROM dbo.Users WHERE UserName = @UserName;
END