USE [master]
GO
/****** Object:  Database [esplit]    Script Date: 11/06/2025 10:49:12 AM ******/
CREATE DATABASE [esplit]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'esplit', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\esplit.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'esplit_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\esplit_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [esplit] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [esplit].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [esplit] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [esplit] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [esplit] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [esplit] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [esplit] SET ARITHABORT OFF 
GO
ALTER DATABASE [esplit] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [esplit] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [esplit] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [esplit] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [esplit] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [esplit] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [esplit] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [esplit] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [esplit] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [esplit] SET  ENABLE_BROKER 
GO
ALTER DATABASE [esplit] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [esplit] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [esplit] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [esplit] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [esplit] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [esplit] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [esplit] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [esplit] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [esplit] SET  MULTI_USER 
GO
ALTER DATABASE [esplit] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [esplit] SET DB_CHAINING OFF 
GO
ALTER DATABASE [esplit] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [esplit] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [esplit] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [esplit] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [esplit] SET QUERY_STORE = ON
GO
ALTER DATABASE [esplit] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [esplit]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[UserID1] [int] NOT NULL,
	[UserID2] [int] NOT NULL,
	[ContactStatus] [int] NOT NULL,
	[ContactInit] [datetime] NOT NULL,
	[StatusUpdateOn] [datetime] NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[NotifyFor] [int] NOT NULL,
	[ActionPerformedBy] [varchar](30) NOT NULL,
	[NotificationText] [varchar](100) NULL,
	[NotificationType] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SplitContacts]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SplitContacts](
	[SplitID] [int] NOT NULL,
	[SplitParticipantID] [int] NOT NULL,
	[OweAmount] [decimal](10, 2) NOT NULL,
	[SplitStatus] [int] NOT NULL,
	[StatusUpdateOn] [datetime] NULL,
 CONSTRAINT [PK_SplitContacts] PRIMARY KEY CLUSTERED 
(
	[SplitID] ASC,
	[SplitParticipantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Splits]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Splits](
	[SplitID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[SplitDescription] [varchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[SplitAmount] [decimal](10, 2) NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[IsClosed] [bit] NULL,
 CONSTRAINT [PK_Splits] PRIMARY KEY CLUSTERED 
(
	[SplitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](30) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](256) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT (getdate()) FOR [ContactInit]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Splits] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Splits] ADD  DEFAULT ((0)) FOR [IsClosed]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Users_1] FOREIGN KEY([UserID1])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Users_1]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Users_2] FOREIGN KEY([UserID2])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Users_2]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users] FOREIGN KEY([NotifyFor])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users]
GO
ALTER TABLE [dbo].[SplitContacts]  WITH CHECK ADD  CONSTRAINT [FK_SplitContacts_Splits] FOREIGN KEY([SplitID])
REFERENCES [dbo].[Splits] ([SplitID])
GO
ALTER TABLE [dbo].[SplitContacts] CHECK CONSTRAINT [FK_SplitContacts_Splits]
GO
ALTER TABLE [dbo].[SplitContacts]  WITH CHECK ADD  CONSTRAINT [FK_SplitContacts_Users] FOREIGN KEY([SplitParticipantID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[SplitContacts] CHECK CONSTRAINT [FK_SplitContacts_Users]
GO
ALTER TABLE [dbo].[Splits]  WITH CHECK ADD  CONSTRAINT [FK_Splits_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Splits] CHECK CONSTRAINT [FK_Splits_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddSplitParticipant]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AddSplitParticipant]
	@SplitID int,
	@SplitParticipantID int,
	@OweAmount decimal(10,2)
as
begin
	insert into SplitContacts (SplitID, SplitParticipantID, OweAmount, SplitStatus)
	values (@SplitID, @SplitParticipantID,@OweAmount, '1');
end
GO
/****** Object:  StoredProcedure [dbo].[CreateContact]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[CreateContact]
	@UserID int,
	@toUserName varchar(30)
as
begin
	declare @toUserID int;

	select @toUserID=UserID from dbo.Users
	where UserName=@toUserName;

	insert into Contacts (UserID1, UserID2, ContactStatus)
	values (@UserID, @toUserID, '1'); -- 1 is for PENDING
	SELECT SCOPE_IDENTITY();
end
GO
/****** Object:  StoredProcedure [dbo].[CreateNotification]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[CreateNotification]
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
GO
/****** Object:  StoredProcedure [dbo].[CreateSplit]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[CreateSplit]
	@UserID int,
	@SplitDescription varchar(100),
	@SplitAmount decimal(10,2)
as
begin
	insert into Splits (CreatedBy, SplitDescription, SplitAmount)
	values (@UserID, @SplitDescription, @SplitAmount);
	SELECT SCOPE_IDENTITY();
end
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateUser]
	@UserName varchar(30),
	@FullName varchar(50),
	@PasswordHash varchar(256)
AS
BEGIN
	INSERT INTO dbo.Users (UserName, FullName, PasswordHash)
    VALUES (@UserName, @FullName, @PasswordHash);
	SELECT SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteContact]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[DeleteContact]
	@ContactID int
as
begin
	update Contacts
	set ContactStatus='4' -- 4 is DELETED
	WHERE ContactID=@ContactID
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteNotification]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[DeleteNotification]
	@NotificationID int,
	@NotifyFor int
as
begin
	update Notifications
	set isDeleted='1' --this is boolean
	where NotificationID=@NotificationID
	and NotifyFor=@NotifyFor
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteSplitParticipant]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[DeleteSplitParticipant]
	@SplitID int,
	@SplitParticipantID int
as
begin 
	delete from SplitContacts
	where SplitID = @SplitID and SplitParticipantID = @SplitParticipantID
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
	@UserID varchar(30)
AS
BEGIN
	UPDATE dbo.Users
	SET IsActive = 0 --this is boolean
	WHERE UserID = @UserID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetContacts]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[GetContacts]
    @UserID INT,
	@ContactStatus int
AS
BEGIN
    SELECT	c.ContactID,
			c.ContactStatus,
			c.ContactInit,
			c.StatusUpdateOn,
			c.UserID1,
			c.UserID2,
			u.UserID,
			u.UserName,
			u.FullName,
			u.CreatedAt,
			u.isActive
    FROM dbo.Contacts c
    INNER JOIN dbo.Users u
        ON (u.UserID = c.UserID1 AND c.UserID2 = @UserID)
        OR (u.UserID = c.UserID2 AND c.UserID1 = @UserID)
	WHERE u.isActive=1 and c.ContactStatus = @ContactStatus
END
GO
/****** Object:  StoredProcedure [dbo].[GetNotifications]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetNotifications]
	@NotifyFor int
as
begin
	select * from Notifications
	where NotifyFor=@NotifyFor
	and IsDeleted=0
end
GO
/****** Object:  StoredProcedure [dbo].[GetSplitParticipants]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetSplitParticipants]
	@SplitID int
as
begin
	SELECT * FROM SplitContacts c
        INNER JOIN Users u ON c.SplitParticipantID = u.UserID
		WHERE c.SplitID = @SplitID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetSplits]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSplits]
    @UserID INT,
    @SplitStatus int
AS
BEGIN
    IF @SplitStatus = '11' -- enum OWNED
    BEGIN
        SELECT *,@SplitStatus as SplitStatus
        FROM dbo.Splits
        WHERE CreatedBy = @UserID
          AND IsClosed = 0;
    END
    ELSE
    BEGIN
        SELECT s.*,c.SplitStatus
        FROM SplitContacts c
        INNER JOIN Splits s ON c.SplitID = s.SplitID
        WHERE c.SplitParticipantID = @UserID
          AND (
                (@SplitStatus = '12' AND c.SplitStatus IN ('1', '3')) OR -- 12 is for ALL case
                (@SplitStatus = '4' AND c.SplitStatus = @SplitStatus)
              ); -- 9 denotes all
    END
END
GO
/****** Object:  StoredProcedure [dbo].[GetThisContact]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetThisContact]
	@ContactID int
as
begin 
	select * from dbo.Contacts
	where ContactID = @ContactID
end
GO
/****** Object:  StoredProcedure [dbo].[GetUserByID]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByID]
	@UserID varchar(30)
AS
BEGIN
	SELECT UserID, UserName, FullName, PasswordHash, CreatedAt, IsActive 
	FROM dbo.Users WHERE UserID = @UserID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByUserName]
	@UserName varchar(30)
AS
BEGIN
	SELECT UserID, UserName, FullName, PasswordHash, CreatedAt, IsActive 
	FROM dbo.Users WHERE UserName = @UserName;
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserIDByUserName]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserIDByUserName]
	@UserName varchar(30)
as
begin
	select UserID from dbo.Users where UserName = @UserName;
end
GO
/****** Object:  StoredProcedure [dbo].[GetUserNameByID]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserNameByID]
	@UserID int
as
begin
	select UserName from dbo.Users where UserID = @UserID;
end
GO
/****** Object:  StoredProcedure [dbo].[InteractContact]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[InteractContact]
	@ContactID int,
	@ContactStatus int
as
begin 
	update Contacts 
	set ContactStatus=@ContactStatus
	where ContactID=@ContactID
	
	select UserID1 from Contacts where ContactID = @ContactID;
end
GO
/****** Object:  StoredProcedure [dbo].[MarkClosed]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[MarkClosed]
	@UserID int,
	@SplitID int
as
begin
	update Splits
	set IsClosed = '1'
	where SplitID = @SplitID and CreatedBy = @UserID
end
GO
/****** Object:  StoredProcedure [dbo].[PayDue]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[PayDue]
	@UserID int,
	@SplitID int
as
begin
	update SplitContacts
	set SplitStatus = '4' --PAID
	where SplitID = @SplitID and SplitParticipantID = @UserID
end
GO
/****** Object:  StoredProcedure [dbo].[ToggleSplit]    Script Date: 11/06/2025 10:49:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[ToggleSplit]
	@SplitID int,
	@UserID int,
	@SplitStatus int
as
begin 
	update SplitContacts
	set SplitStatus = @SplitStatus
	where SplitID = @SplitID and SplitParticipantID = @UserID;
end
GO
USE [master]
GO
ALTER DATABASE [esplit] SET  READ_WRITE 
GO
