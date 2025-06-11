USE [esplit]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure GetThisContact
	@UserID int,
	@ContactID int,
	@ContactStatus int
as
begin 
	create table #TempContactsTable
	(
		ContactID int,
		ContactStatus int,
		ContactInit nvarchar(50),
		StatusUpdateOn datetime,
		UserID1 int,
		UserID2 int,
		UserID int,
		UserName nvarchar(50),
		FullName nvarchar(100),
		CreatedAt datetime,
		isActive bit
	);

	Insert into #TempContactsTable exec GetContacts @UserID, @ContactStatus;
	select * from #TempContactsTable where ContactID = @ContactID;
	drop table #TempContactsTable;
end