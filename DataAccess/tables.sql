create database esplit;
use esplit;

create table [dbo].[Users] (
	UserID int IDENTITY(1,1) not null,
	UserName varchar(30) not null,
	FullName varchar(50) not null,
	PasswordHash varchar(256) not null,
	CreatedAt DateTime default GETDATE() not null,
	IsActive bit not null default 1,
	CONSTRAINT PK_Users PRIMARY KEY (UserID)
);

create table [dbo].[Contacts] (
	ContactID int IDENTITY(1,1) not null,
	UserID1 int not null, --this is the person who initiated contact
	UserID2 int not null, --this is the user who was initiated with 
	ContactStatus int not null, -- (PENDING, APPROVED, REJECTED, DELETED)
	ContactInit DateTime default GETDATE() not null,
	StatusUpdateOn DateTime,
	CONSTRAINT PK_Contacts PRIMARY KEY (ContactID),
	CONSTRAINT FK_Contacts_Users_1 FOREIGN KEY (UserID1) REFERENCES dbo.Users(UserID),
	CONSTRAINT FK_Contacts_Users_2 FOREIGN KEY (UserID2) REFERENCES dbo.Users(UserID)
);

create table [dbo].[Splits] (
	SplitID int IDENTITY(1,1) not null,
	CreatedBy int not null,
	SplitDescription varchar(100),
	CreatedOn DateTime default GETDATE(),
	SplitAmount decimal(10,2) not null,
	UpdatedOn DateTime,
	IsClosed bit default 0,
	CONSTRAINT PK_Splits PRIMARY KEY (SplitID),
	CONSTRAINT FK_Splits_Users FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);

create table [dbo].[SplitContacts] (
	SplitID int not null,
	SplitParticipantID int not null,
	OweAmount Decimal(10,2) not null,
	SplitStatus int not null, -- (PENDING_APPROVAL, REJECTED, APPROVED_UNPAID, PAID)
	StatusUpdateOn DateTime,
	CONSTRAINT PK_SplitContacts PRIMARY KEY (SplitID, SplitParticipantID),
	CONSTRAINT FK_SplitContacts_Splits FOREIGN KEY (SplitID) REFERENCES dbo.Splits(SplitID),
	CONSTRAINT FK_SplitContacts_Users FOREIGN KEY (SplitParticipantID) REFERENCES dbo.Users(UserID)
);

create table [dbo].[Notifications] (
	NotificationID int IDENTITY(1,1) not null,
	NotifyFor int not null,
	ActionPerformedBy varchar(30) not null,
	NotificationText varchar(100),
	NotificationType int not null, --this is enum
	CreatedOn DateTime default GETDATE() not null,
	IsDeleted bit default 0,
	CONSTRAINT PK_Notifications PRIMARY KEY (NotificationID),
	CONSTRAINT FK_Notifications_Users FOREIGN KEY (NotifyFor) REFERENCES dbo.Users(UserID)
	--(TEST, SYSTEM, SPLIT_CREATED, SPLIT_APPROVAL, SPLIT_REJECTED, SPLIT_DELETE, SPLIT_PAYMENT,)
	--(CONNECTION_SENT, CONNECTION_ACCEPTED, CONNECTION_REJECTED, CONNECTION_DELETED, CONNECTION_REQUESTED)
);

