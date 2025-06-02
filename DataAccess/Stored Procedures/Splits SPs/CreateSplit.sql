USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure CreateSplit
	@UserID int,
	@SplitDescription varchar(100),
	@SplitAmount decimal(10,2)
as
begin
	insert into Splits (CreatedBy, SplitDescription, SplitAmount)
	values (@UserID, @SplitDescription, @SplitAmount);
	SELECT SCOPE_IDENTITY();
end