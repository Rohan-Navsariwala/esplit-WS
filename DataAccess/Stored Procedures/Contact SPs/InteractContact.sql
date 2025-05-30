create procedure InteractContact
	@ContactID int,
	@ContactStatus int
as
begin 
	update Contacts 
	set ContactStatus=@ContactStatus
	where ContactID=@ContactID
end