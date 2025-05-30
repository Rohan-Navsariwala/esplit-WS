create procedure DeleteContact
	@ContactID int
as
begin
	update Contacts
	set ContactStatus='4'
	WHERE ContactID=@ContactID
end