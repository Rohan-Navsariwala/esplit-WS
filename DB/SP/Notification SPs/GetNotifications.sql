create procedure GetNotifications
	@UserID int
as
begin
	select * from Notifications
	where NotifyFor=@UserID 
	and isDeleted=0
end