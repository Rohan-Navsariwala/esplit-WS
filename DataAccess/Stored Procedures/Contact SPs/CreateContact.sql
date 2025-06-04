USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure CreateContact
	@UserID int,
	@toUserName varchar(30)
as
begin
	declare @toUserID int;

	select @toUserID=UserID from dbo.Users
	where UserName=@toUserName;

	insert into Contacts (UserID1, UserID2, ContactStatus)
	values (@UserID, @toUserID, '1'); -- 1 is for PENDING
	SELECT SCOPE_IDENTITY();
end