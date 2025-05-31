USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure MarkClosed
	@SplitID int
as
begin
	update Splits
	set IsClosed = '1'
	where SPlitID = @SplitID
end