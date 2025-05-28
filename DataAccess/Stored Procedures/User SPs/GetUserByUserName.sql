USE [esplit]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 28/05/2025 11:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetUserByUserName]
	@UserName varchar(30)
AS
BEGIN
	SELECT UserID, UserName, FullName, PasswordHash, CreatedAt, IsActive 
	FROM dbo.Users WHERE UserName = @UserName;
END