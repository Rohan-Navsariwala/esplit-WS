ALTER procedure CreateContact
	@UserID int,
	@toUserName varchar(30)
as
begin
	declare @toUserID int;

	select @toUserID=UserID from dbo.Users
	where UserName=@toUserName;

	insert into Contacts (UserID1, UserID2, ContactStatus)
	values (@UserID, @toUserID, '0');
	SELECT SCOPE_IDENTITY();
end