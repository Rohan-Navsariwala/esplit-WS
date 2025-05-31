USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure DeleteNotification
	@NotificationID int,
	@UserID int
as
begin
	update Notifications
	set isDeleted='1' --this is boolean
	where NotificationID=@NotificationID
	and NotifyFor=@UserID
end