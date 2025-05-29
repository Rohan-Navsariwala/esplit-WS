ALTER procedure CreateNotification
	@NotifyFor int,
	@ActionPerformedBy varchar(30),
	@NotificationText varchar(100),
	@NotificationType varchar(30)
as
begin
	insert into Notifications (NotifyFor, ActionPerformedBy,NotificationText,NotificationType)
	values (@NotifyFor, @ActionPerformedBy, @NotificationText, @NotificationType);
	SELECT SCOPE_IDENTITY();
end