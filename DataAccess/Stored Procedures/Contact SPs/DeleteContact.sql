create procedure DeleteContact
	@ContactID int
as
begin
	update Contacts
	set ConnectionStatus='DELETED'
	WHERE ContactID=@ContactID
end