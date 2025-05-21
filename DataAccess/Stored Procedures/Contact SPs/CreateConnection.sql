create procedure CreateConnection
	@UserID int,
	@toUserName varchar(30)
as
begin
	declare @toUserID int;

	select @toUserID=UserID from dbo.Users
	where UserName=@toUserName;

	insert into Contacts (UserID1, UserID2, ConnectionStatus)
	values (@UserID, @toUserID, 'PENDING');
end