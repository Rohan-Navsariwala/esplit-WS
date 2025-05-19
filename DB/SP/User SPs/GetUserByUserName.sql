CREATE PROCEDURE GetUserByUserName
	@UserName varchar(30)
AS
BEGIN
	SELECT * FROM dbo.Users WHERE UserID = @UserName;
END