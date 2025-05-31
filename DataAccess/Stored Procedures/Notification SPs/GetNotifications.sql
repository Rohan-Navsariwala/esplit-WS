USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure GetNotifications
	@UserID int
as
begin
	select * from Notifications
	where NotifyFor=@UserID 
	and IsDeleted=0
end