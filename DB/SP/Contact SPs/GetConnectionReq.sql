create procedure GetConnectionReq
	@UserID int
as
begin
	select * from Contacts
	where UserID1=@UserID or UserID2=@UserID
end