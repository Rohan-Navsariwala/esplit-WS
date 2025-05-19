CREATE PROCEDURE DeleteUser
	@UserID varchar(30)
AS
BEGIN
	UPDATE dbo.Users
	SET isActive = 0
	WHERE UserID = @UserID;
END