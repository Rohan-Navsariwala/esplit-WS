ALTER PROCEDURE CreateUser
	@UserName varchar(30),
	@FullName varchar(50),
	@PasswordHash varchar(256)
AS
BEGIN
	INSERT INTO dbo.Users (UserName, FullName, PasswordHash)
    VALUES (@UserName, @FullName, @PasswordHash);
	SELECT SCOPE_IDENTITY();
END

