USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure GetNotifications
	@NotifyFor int
as
begin
	select * from Notifications
	where NotifyFor=@NotifyFor
	and IsDeleted=0
end