USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE DeleteUser
	@UserID varchar(30)
AS
BEGIN
	UPDATE dbo.Users
	SET IsActive = 0 --this is boolean
	WHERE UserID = @UserID;
END