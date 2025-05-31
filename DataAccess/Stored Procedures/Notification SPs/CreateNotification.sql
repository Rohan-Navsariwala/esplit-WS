USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure CreateNotification
	@NotifyFor int,
	@ActionPerformedBy varchar(30),
	@NotificationText varchar(100),
	@NotificationType int
as
begin
	insert into Notifications (NotifyFor, ActionPerformedBy,NotificationText,NotificationType)
	values (@NotifyFor, @ActionPerformedBy, @NotificationText, @NotificationType);
	SELECT SCOPE_IDENTITY();
end