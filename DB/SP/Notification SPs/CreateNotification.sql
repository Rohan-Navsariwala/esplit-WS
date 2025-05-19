create procedure CreateNotification
	@NotifyFor int,
	@NotificationText varchar(100),
	@NotificationType varchar(30)
as
begin
	declare @ActionPerformedBy varchar(30);
	select @ActionPerformedBy = UserName 
	from Users where UserID = @NotifyFor;

	insert into Notifications (NotifyFor, ActionPerformedBy,NotificationText,NotificationType)
	values (@NotifyFor, @ActionPerformedBy, @NotificationText, @NotificationType)
end