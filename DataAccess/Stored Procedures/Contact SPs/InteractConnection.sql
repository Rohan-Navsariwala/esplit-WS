create procedure InteractConnection
	@ContactID int,
	@ConnectionStatus varchar(10)
as
begin 
	update Contacts 
	set ConnectionStatus=@ConnectionStatus
	where ContactID=@ContactID
end