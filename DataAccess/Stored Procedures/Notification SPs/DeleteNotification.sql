create procedure DeleteNotification
	@NotificationID int,
	@UserID int
as
begin
	update Notifications
	set isDeleted='1'
	where NotificationID=@NotificationID
	and NotifyFor=@UserID
end