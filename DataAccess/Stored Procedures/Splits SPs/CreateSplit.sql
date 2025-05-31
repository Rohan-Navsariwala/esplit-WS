USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure CreateSplit
	@CreatedBy int,
	@SplitDescription varchar(100),
	@SplitAmount decimal(10,2)
as
begin
	insert into Splits (CreatedBy, SplitDescription, SplitAmount)
	values (@CreatedBy, @SplitDescription, @SplitAmount);
	SELECT SCOPE_IDENTITY();
end