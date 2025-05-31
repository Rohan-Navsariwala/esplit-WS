USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure DeleteContact
	@ContactID int
as
begin
	update Contacts
	set ContactStatus='4' -- 4 is DELETED
	WHERE ContactID=@ContactID
end