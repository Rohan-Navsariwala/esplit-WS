ALTER PROCEDURE GetUserByUserName
	@UserName varchar(30)
AS
BEGIN
	SELECT * FROM dbo.Users WHERE UserName = @UserName;
END