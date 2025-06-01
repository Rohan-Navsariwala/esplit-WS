create procedure InteractContact
	@ContactID int,
	@ContactStatus int
as
begin 
	update Contacts 
	set ContactStatus=@ContactStatus
	where ContactID=@ContactID
	
	select UserID1 from Contacts where ContactID = @ContactID;
end