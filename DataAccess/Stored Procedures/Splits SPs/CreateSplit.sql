ALTER procedure CreateSplit
	@CreatedBy int,
	@SplitDescription varchar(100),
	@Deadline DateTime,
	@SplitAmount decimal(10,2)
as
begin
	insert into Splits (CreatedBy, SplitDescription, Deadline, SplitAmount)
	values (@CreatedBy, @SplitDescription, @Deadline, @SplitAmount);
	SELECT SCOPE_IDENTITY();
end