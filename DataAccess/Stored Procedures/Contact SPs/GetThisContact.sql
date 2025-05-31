USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure GetThisContact
	@ContactID int
as
begin 
	select * from dbo.Contacts
	where ContactID = @ContactID
end