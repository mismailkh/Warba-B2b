USE [master]
GO
/****** Object:  Database [WARBA_B2B_DEV]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE DATABASE [WARBA_B2B_DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WARBA_B2B_DEV', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\WARBA_B2B_DEV.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WARBA_B2B_DEV_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\WARBA_B2B_DEV_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WARBA_B2B_DEV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ARITHABORT OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET  MULTI_USER 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WARBA_B2B_DEV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WARBA_B2B_DEV] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WARBA_B2B_DEV] SET QUERY_STORE = ON
GO
ALTER DATABASE [WARBA_B2B_DEV] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [WARBA_B2B_DEV]
GO
/****** Object:  Schema [app]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [app]
GO
/****** Object:  Schema [lob]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [lob]
GO
/****** Object:  Schema [motor_comp]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [motor_comp]
GO
/****** Object:  Schema [notif]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [notif]
GO
/****** Object:  Schema [org]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [org]
GO
/****** Object:  Schema [ums]    Script Date: 10/11/2025 5:17:50 pm ******/
CREATE SCHEMA [ums]
GO
/****** Object:  Table [app].[ERROR_LOG]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[ERROR_LOG](
	[Id] [uniqueidentifier] NOT NULL,
	[LogDate] [datetime] NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Module] [nvarchar](50) NULL,
	[Computer] [nvarchar](100) NULL,
	[TerminalId] [nvarchar](500) NOT NULL,
	[IPAddress] [nvarchar](50) NOT NULL,
	[Operation] [nvarchar](500) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[StackTrace] [nvarchar](1000) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ERRORLOG] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [app].[PROCESS_LOG]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[PROCESS_LOG](
	[Id] [uniqueidentifier] NOT NULL,
	[Process] [nvarchar](100) NOT NULL,
	[Module] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Computer] [nvarchar](100) NULL,
	[TerminalId] [nvarchar](500) NOT NULL,
	[IPAddress] [nvarchar](500) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[LogDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PROCESSLOG] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [app].[Translation]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Translation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[ValueEn] [nvarchar](250) NOT NULL,
	[ValueAr] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [UC_Translation] UNIQUE CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[PROCESS_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[PROCESS_PR](
	[Id] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__PROCESS__3214EC0791914FFD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[PROCESS_SUBPROCESS_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[PROCESS_SUBPROCESS_PR](
	[Id] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[SubprocessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__PROCESS___3214EC0762E8BE18] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[PRODUCT_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[PRODUCT_PR](
	[Id] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Description] [nvarchar](512) NULL,
 CONSTRAINT [PK__PRODUCT__3214EC07F6C6695E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[PRODUCT_PROCESS_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[PRODUCT_PROCESS_PR](
	[Id] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProcessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__PRODUCT___3214EC078D4C5B95] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[PRODUCT_PROCESS_SUBPROCESS_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[PRODUCT_PROCESS_SUBPROCESS_PR](
	[Id] [int] NOT NULL,
	[ProductProcessId] [int] NOT NULL,
	[ProcessSubprocessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__PRODUCT___3214EC07303C2022] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [lob].[SUBPROCESS_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [lob].[SUBPROCESS_PR](
	[Id] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__SUBPROCE__3214EC07DED5C921] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [motor_comp].[CAR_MAKE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [motor_comp].[CAR_MAKE](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__CAR_MAKE__3214EC078F37E85B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [motor_comp].[CAR_MODEL]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [motor_comp].[CAR_MODEL](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[CarMakeId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__CAR_MODE__3214EC074020B395] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [motor_comp].[PACKAGE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [motor_comp].[PACKAGE](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[YearMakeFrom] [datetime] NOT NULL,
	[YearMakeTo] [datetime] NOT NULL,
	[SumInsuredMin] [decimal](10, 2) NOT NULL,
	[SumInsuredMax] [decimal](10, 2) NOT NULL,
	[RiskRateMinPercentage] [int] NOT NULL,
	[RiskRateMaxPercentage] [int] NOT NULL,
	[AgeOfInsured] [int] NOT NULL,
	[PolicyDuration] [int] NOT NULL,
	[MinPremium] [decimal](10, 2) NOT NULL,
	[InsuranceFee] [decimal](10, 2) NOT NULL,
	[PurchaseCommissionPercentage] [int] NOT NULL,
	[RenewalCommissionPercentage] [int] NOT NULL,
	[TermsAndConditionsEn] [nvarchar](1000) NOT NULL,
	[TermsAndConditionsAr] [nvarchar](1000) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__PACKAGE__3214EC0747760C60] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [motor_comp].[PACKAGE_CAR_MAKE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [motor_comp].[PACKAGE_CAR_MAKE](
	[PackageId] [uniqueidentifier] NOT NULL,
	[CarMakeId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PACKAGE_CAR_MAKE] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC,
	[CarMakeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIF_NOTIFICATION_EVENT]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIF_NOTIFICATION_EVENT](
	[EventId] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[DescriptionEn] [nvarchar](500) NULL,
	[DescriptionAr] [nvarchar](500) NULL,
	[ReceiverTypeId] [int] NULL,
	[ReceiverTypeRefId] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_NOTIF_NOTIFICATION_EVENT] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIF_NOTIFICATION_EVENT_PLACEHOLDERS_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIF_NOTIFICATION_EVENT_PLACEHOLDERS_LKP](
	[PlaceHolderId] [int] IDENTITY(1,1) NOT NULL,
	[PlaceHolderName] [nvarchar](50) NOT NULL,
	[EventId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlaceHolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIF_NOTIFICATION_RECEIVER_TYPE_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIF_NOTIFICATION_RECEIVER_TYPE_LKP](
	[ReceiverTypeId] [int] NOT NULL,
	[ReceiverTypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiverTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIF_NOTIFICATION_STATUS_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIF_NOTIFICATION_STATUS_LKP](
	[StatusId] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIF_NOTIFICATION_TEMPLATE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIF_NOTIFICATION_TEMPLATE](
	[TemplateId] [uniqueidentifier] NOT NULL,
	[EventId] [int] NULL,
	[ChannelId] [int] NULL,
	[NameEn] [nvarchar](100) NULL,
	[NameAr] [nvarchar](100) NULL,
	[SubjectEn] [nvarchar](150) NULL,
	[SubjectAr] [nvarchar](150) NULL,
	[BodyEn] [nvarchar](1000) NULL,
	[BodyAr] [nvarchar](1000) NULL,
	[Footer] [nvarchar](1000) NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [Event_Channel_UQK] UNIQUE NONCLUSTERED 
(
	[EventId] ASC,
	[ChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION](
	[NotificationId] [uniqueidentifier] NOT NULL,
	[DueDate] [datetime] NULL,
	[ReadDate] [datetime] NULL,
	[SenderId] [nvarchar](450) NULL,
	[ReceiverId] [nvarchar](450) NULL,
	[NotificationURL] [nvarchar](450) NULL,
	[ModuleId] [int] NOT NULL,
	[NotificationMessageEn] [nvarchar](max) NULL,
	[NotificationMessageAr] [nvarchar](max) NULL,
	[NotificationStatusId] [int] NULL,
	[NotificationTemplateId] [uniqueidentifier] NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_CHANNEL_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_CHANNEL_LKP](
	[ChannelId] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_EVENT]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_EVENT](
	[EventId] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[DescriptionEn] [nvarchar](500) NULL,
	[DescriptionAr] [nvarchar](500) NULL,
	[ReceiverTypeId] [int] NULL,
	[ReceiverTypeRefId] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_NOTIFICATION_EVENT] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_EVENT_PLACEHOLDERS_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_EVENT_PLACEHOLDERS_LKP](
	[PlaceHolderId] [int] IDENTITY(1,1) NOT NULL,
	[PlaceHolderName] [nvarchar](50) NOT NULL,
	[EventId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlaceHolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_RECEIVER_TYPE_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_RECEIVER_TYPE_LKP](
	[ReceiverTypeId] [int] NOT NULL,
	[ReceiverTypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiverTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_STATUS_LKP]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_STATUS_LKP](
	[StatusId] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [notif].[NOTIFICATION_TEMPLATE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [notif].[NOTIFICATION_TEMPLATE](
	[TemplateId] [uniqueidentifier] NOT NULL,
	[EventId] [int] NULL,
	[ChannelId] [int] NULL,
	[NameEn] [nvarchar](100) NULL,
	[NameAr] [nvarchar](100) NULL,
	[SubjectEn] [nvarchar](150) NULL,
	[SubjectAr] [nvarchar](150) NULL,
	[BodyEn] [nvarchar](1000) NULL,
	[BodyAr] [nvarchar](1000) NULL,
	[Footer] [nvarchar](1000) NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_Event_Channel] UNIQUE NONCLUSTERED 
(
	[EventId] ASC,
	[ChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[DEPARTMENT]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[DEPARTMENT](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[BrokerageCode] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Logo] [nvarchar](max) NOT NULL,
	[IsDirectPurchaseAllowed] [bit] NOT NULL,
	[TypeId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION_PAYMENT_METHOD]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION_PAYMENT_METHOD](
	[Id] [uniqueidentifier] NOT NULL,
	[MethodId] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION_PRODUCT]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION_PRODUCT](
	[Id] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION_PRODUCT_PROCESS]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION_PRODUCT_PROCESS](
	[Id] [uniqueidentifier] NOT NULL,
	[OrganizationProductId] [uniqueidentifier] NOT NULL,
	[ProductProcessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION_PRODUCT_PROCESS_SUBPROCESS]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION_PRODUCT_PROCESS_SUBPROCESS](
	[Id] [uniqueidentifier] NOT NULL,
	[OrganizationProductProcessId] [uniqueidentifier] NOT NULL,
	[ProductProcessSubprocessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANIZATION_TYPE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANIZATION_TYPE](
	[Id] [int] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [org].[ORGANZIATION_PACKAGE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [org].[ORGANZIATION_PACKAGE](
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ORGANZIATION_PACKAGE] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC,
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[ACCESS_CATEGORY]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[ACCESS_CATEGORY](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[DescriptionEn] [nvarchar](512) NOT NULL,
	[DescriptionAr] [nvarchar](512) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_UMS_ACCESS_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[ACCESS_CATEGORY_CLAIM]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[ACCESS_CATEGORY_CLAIM](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[ClaimValue] [nvarchar](512) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[AUTHORIZATION_LEVEL]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[AUTHORIZATION_LEVEL](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[DescriptionEn] [nvarchar](512) NOT NULL,
	[DescriptionAr] [nvarchar](512) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_UMS_AUTHORIZATION_LEVEL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[AUTHORIZATION_LEVEL_ACCESS_CATEGORY]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[AUTHORIZATION_LEVEL_ACCESS_CATEGORY](
	[AuthorizationLevelId] [uniqueidentifier] NOT NULL,
	[AccessCategoryId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UMS_AUTHORIZATION_LEVEL_ACCESS_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[AuthorizationLevelId] ASC,
	[AccessCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[CLAIM_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[CLAIM_PR](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TitleEn] [nvarchar](50) NOT NULL,
	[TitleAr] [nvarchar](50) NOT NULL,
	[ClaimType] [nvarchar](512) NOT NULL,
	[ClaimValue] [nvarchar](512) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ModuleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_CLAIM_VALUE] UNIQUE NONCLUSTERED 
(
	[ClaimValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_TITLE_EN] UNIQUE NONCLUSTERED 
(
	[TitleEn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[DESIGNATION]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[DESIGNATION](
	[Id] [uniqueidentifier] NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[ROLE_PR]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[ROLE_PR](
	[Id] [nvarchar](150) NOT NULL,
	[NameEn] [nvarchar](50) NULL,
	[NameAr] [nvarchar](50) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[DescriptionEn] [nvarchar](500) NULL,
	[DescriptionAr] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NameEn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NameAr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[IsLocked] [bit] NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[IsPasswordReset] [bit] NOT NULL,
	[AllowAccess] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_ACTIVITY]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_ACTIVITY](
	[ActivityId] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[UserName] [nvarchar](256) NULL,
	[ComputerName] [nvarchar](100) NOT NULL,
	[IPAddress] [nvarchar](500) NOT NULL,
	[IsLoggedIn] [bit] NULL,
	[LoginDateTime] [datetime] NULL,
	[IsLoggedOut] [bit] NULL,
	[LogoutDateTime] [datetime] NULL,
	[ExceptionMessage] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_PERSONAL_INFORMATION]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_PERSONAL_INFORMATION](
	[UserId] [nvarchar](450) NOT NULL,
	[FirstNameEn] [nvarchar](50) NOT NULL,
	[FirstNameAr] [nvarchar](50) NOT NULL,
	[SecondNameEn] [nvarchar](50) NULL,
	[SecondNameAr] [nvarchar](50) NULL,
	[LastNameEn] [nvarchar](50) NOT NULL,
	[LastNameAr] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[CivilId] [varchar](15) NULL,
	[CivilIdExpiryDate] [datetime] NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Avatar] [nvarchar](max) NULL,
	[AuthorizationLevelId] [uniqueidentifier] NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[DesignationId] [uniqueidentifier] NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_CIVILID] UNIQUE NONCLUSTERED 
(
	[CivilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_PRODUCT]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_PRODUCT](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_PRODUCT_PROCESS]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_PRODUCT_PROCESS](
	[Id] [uniqueidentifier] NOT NULL,
	[UserProductId] [uniqueidentifier] NOT NULL,
	[ProductProcessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_PRODUCT_PROCESS_SUBPROCESS]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_PRODUCT_PROCESS_SUBPROCESS](
	[Id] [uniqueidentifier] NOT NULL,
	[UserProductProcessId] [uniqueidentifier] NOT NULL,
	[ProductProcessSubprocessId] [int] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ums].[USER_ROLE]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ums].[USER_ROLE](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](150) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](450) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](450) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Composite_RoleId_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [app].[ERROR_LOG] ADD  DEFAULT ('') FOR [TerminalId]
GO
ALTER TABLE [app].[ERROR_LOG] ADD  DEFAULT ('') FOR [Message]
GO
ALTER TABLE [app].[PROCESS_LOG] ADD  DEFAULT ('') FOR [TerminalId]
GO
ALTER TABLE [lob].[PROCESS_SUBPROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PROCESS_S__Proce__1D7B6025] FOREIGN KEY([ProcessId])
REFERENCES [lob].[PROCESS_PR] ([Id])
GO
ALTER TABLE [lob].[PROCESS_SUBPROCESS_PR] CHECK CONSTRAINT [FK__PROCESS_S__Proce__1D7B6025]
GO
ALTER TABLE [lob].[PROCESS_SUBPROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PROCESS_S__SubPr__1E6F845E] FOREIGN KEY([SubprocessId])
REFERENCES [lob].[SUBPROCESS_PR] ([Id])
GO
ALTER TABLE [lob].[PROCESS_SUBPROCESS_PR] CHECK CONSTRAINT [FK__PROCESS_S__SubPr__1E6F845E]
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PRODUCT_P__Proce__2B0A656D] FOREIGN KEY([ProcessId])
REFERENCES [lob].[PROCESS_PR] ([Id])
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_PR] CHECK CONSTRAINT [FK__PRODUCT_P__Proce__2B0A656D]
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PRODUCT_P__Produ__2BFE89A6] FOREIGN KEY([ProductId])
REFERENCES [lob].[PRODUCT_PR] ([Id])
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_PR] CHECK CONSTRAINT [FK__PRODUCT_P__Produ__2BFE89A6]
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_SUBPROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PRODUCT_P__Proce__22401542] FOREIGN KEY([ProcessSubprocessId])
REFERENCES [lob].[PROCESS_SUBPROCESS_PR] ([Id])
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_SUBPROCESS_PR] CHECK CONSTRAINT [FK__PRODUCT_P__Proce__22401542]
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_SUBPROCESS_PR]  WITH CHECK ADD  CONSTRAINT [FK__PRODUCT_P__Produ__214BF109] FOREIGN KEY([ProductProcessId])
REFERENCES [lob].[PRODUCT_PROCESS_PR] ([Id])
GO
ALTER TABLE [lob].[PRODUCT_PROCESS_SUBPROCESS_PR] CHECK CONSTRAINT [FK__PRODUCT_P__Produ__214BF109]
GO
ALTER TABLE [motor_comp].[CAR_MODEL]  WITH CHECK ADD  CONSTRAINT [FK__CAR_MODEL__CarMa__67DE6983] FOREIGN KEY([CarMakeId])
REFERENCES [motor_comp].[CAR_MAKE] ([Id])
GO
ALTER TABLE [motor_comp].[CAR_MODEL] CHECK CONSTRAINT [FK__CAR_MODEL__CarMa__67DE6983]
GO
ALTER TABLE [motor_comp].[PACKAGE_CAR_MAKE]  WITH CHECK ADD  CONSTRAINT [FK__PACKAGE_C__CarMa__6F7F8B4B] FOREIGN KEY([CarMakeId])
REFERENCES [motor_comp].[CAR_MAKE] ([Id])
GO
ALTER TABLE [motor_comp].[PACKAGE_CAR_MAKE] CHECK CONSTRAINT [FK__PACKAGE_C__CarMa__6F7F8B4B]
GO
ALTER TABLE [motor_comp].[PACKAGE_CAR_MAKE]  WITH CHECK ADD  CONSTRAINT [FK__PACKAGE_C__Packa__7073AF84] FOREIGN KEY([PackageId])
REFERENCES [motor_comp].[PACKAGE] ([Id])
GO
ALTER TABLE [motor_comp].[PACKAGE_CAR_MAKE] CHECK CONSTRAINT [FK__PACKAGE_C__Packa__7073AF84]
GO
ALTER TABLE [notif].[NOTIF_NOTIFICATION_EVENT]  WITH CHECK ADD FOREIGN KEY([ReceiverTypeId])
REFERENCES [notif].[NOTIF_NOTIFICATION_RECEIVER_TYPE_LKP] ([ReceiverTypeId])
GO
ALTER TABLE [notif].[NOTIF_NOTIFICATION_TEMPLATE]  WITH NOCHECK ADD FOREIGN KEY([EventId])
REFERENCES [notif].[NOTIF_NOTIFICATION_EVENT] ([EventId])
GO
ALTER TABLE [notif].[NOTIFICATION]  WITH CHECK ADD FOREIGN KEY([NotificationStatusId])
REFERENCES [notif].[NOTIFICATION_STATUS_LKP] ([StatusId])
GO
ALTER TABLE [notif].[NOTIFICATION_EVENT]  WITH CHECK ADD FOREIGN KEY([ReceiverTypeId])
REFERENCES [notif].[NOTIFICATION_RECEIVER_TYPE_LKP] ([ReceiverTypeId])
GO
ALTER TABLE [notif].[NOTIFICATION_TEMPLATE]  WITH NOCHECK ADD FOREIGN KEY([ChannelId])
REFERENCES [notif].[NOTIFICATION_CHANNEL_LKP] ([ChannelId])
GO
ALTER TABLE [notif].[NOTIFICATION_TEMPLATE]  WITH NOCHECK ADD FOREIGN KEY([EventId])
REFERENCES [notif].[NOTIFICATION_EVENT] ([EventId])
GO
ALTER TABLE [org].[DEPARTMENT]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [org].[ORGANIZATION] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [org].[ORGANIZATION_TYPE] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PAYMENT_METHOD]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [org].[ORGANIZATION] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [org].[ORGANIZATION] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK__ORGANIZAT__Produ__28ED12D1] FOREIGN KEY([ProductId])
REFERENCES [lob].[PRODUCT_PR] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT] CHECK CONSTRAINT [FK__ORGANIZAT__Produ__28ED12D1]
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS]  WITH CHECK ADD FOREIGN KEY([OrganizationProductId])
REFERENCES [org].[ORGANIZATION_PRODUCT] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS]  WITH CHECK ADD  CONSTRAINT [FK__ORGANIZAT__Produ__5F492382] FOREIGN KEY([ProductProcessId])
REFERENCES [lob].[PRODUCT_PROCESS_PR] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS] CHECK CONSTRAINT [FK__ORGANIZAT__Produ__5F492382]
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS_SUBPROCESS]  WITH CHECK ADD FOREIGN KEY([OrganizationProductProcessId])
REFERENCES [org].[ORGANIZATION_PRODUCT_PROCESS] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS_SUBPROCESS]  WITH CHECK ADD  CONSTRAINT [FK__ORGANIZAT__Produ__6319B466] FOREIGN KEY([ProductProcessSubprocessId])
REFERENCES [lob].[PRODUCT_PROCESS_SUBPROCESS_PR] ([Id])
GO
ALTER TABLE [org].[ORGANIZATION_PRODUCT_PROCESS_SUBPROCESS] CHECK CONSTRAINT [FK__ORGANIZAT__Produ__6319B466]
GO
ALTER TABLE [org].[ORGANZIATION_PACKAGE]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [org].[ORGANIZATION] ([Id])
GO
ALTER TABLE [org].[ORGANZIATION_PACKAGE]  WITH CHECK ADD  CONSTRAINT [FK__ORGANZIAT__Packa__74444068] FOREIGN KEY([PackageId])
REFERENCES [motor_comp].[PACKAGE] ([Id])
GO
ALTER TABLE [org].[ORGANZIATION_PACKAGE] CHECK CONSTRAINT [FK__ORGANZIAT__Packa__74444068]
GO
ALTER TABLE [ums].[ACCESS_CATEGORY_CLAIM]  WITH CHECK ADD  CONSTRAINT [FK_UMS_ACCESS_CATEGORY_CLAIMS_UMS_ACCESS_CATEGORY] FOREIGN KEY([CategoryId])
REFERENCES [ums].[ACCESS_CATEGORY] ([Id])
GO
ALTER TABLE [ums].[ACCESS_CATEGORY_CLAIM] CHECK CONSTRAINT [FK_UMS_ACCESS_CATEGORY_CLAIMS_UMS_ACCESS_CATEGORY]
GO
ALTER TABLE [ums].[ACCESS_CATEGORY_CLAIM]  WITH CHECK ADD  CONSTRAINT [FK_UMS_ACCESS_CATEGORY_CLAIMS_UMS_CLAIM] FOREIGN KEY([ClaimValue])
REFERENCES [ums].[CLAIM_PR] ([ClaimValue])
GO
ALTER TABLE [ums].[ACCESS_CATEGORY_CLAIM] CHECK CONSTRAINT [FK_UMS_ACCESS_CATEGORY_CLAIMS_UMS_CLAIM]
GO
ALTER TABLE [ums].[AUTHORIZATION_LEVEL_ACCESS_CATEGORY]  WITH CHECK ADD FOREIGN KEY([AccessCategoryId])
REFERENCES [ums].[ACCESS_CATEGORY] ([Id])
GO
ALTER TABLE [ums].[AUTHORIZATION_LEVEL_ACCESS_CATEGORY]  WITH CHECK ADD FOREIGN KEY([AuthorizationLevelId])
REFERENCES [ums].[AUTHORIZATION_LEVEL] ([Id])
GO
ALTER TABLE [ums].[USER_ACTIVITY]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [ums].[USER] ([Id])
GO
ALTER TABLE [ums].[USER_PERSONAL_INFORMATION]  WITH CHECK ADD FOREIGN KEY([AuthorizationLevelId])
REFERENCES [ums].[AUTHORIZATION_LEVEL] ([Id])
GO
ALTER TABLE [ums].[USER_PERSONAL_INFORMATION]  WITH CHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [org].[DEPARTMENT] ([Id])
GO
ALTER TABLE [ums].[USER_PERSONAL_INFORMATION]  WITH CHECK ADD FOREIGN KEY([DesignationId])
REFERENCES [ums].[DESIGNATION] ([Id])
GO
ALTER TABLE [ums].[USER_PERSONAL_INFORMATION]  WITH CHECK ADD  CONSTRAINT [FK_USER_PERSONAL_INFORMATION_UserId] FOREIGN KEY([UserId])
REFERENCES [ums].[USER] ([Id])
GO
ALTER TABLE [ums].[USER_PERSONAL_INFORMATION] CHECK CONSTRAINT [FK_USER_PERSONAL_INFORMATION_UserId]
GO
ALTER TABLE [ums].[USER_PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK__USER_PROD__Produ__4A4E069C] FOREIGN KEY([ProductId])
REFERENCES [lob].[PRODUCT_PR] ([Id])
GO
ALTER TABLE [ums].[USER_PRODUCT] CHECK CONSTRAINT [FK__USER_PROD__Produ__4A4E069C]
GO
ALTER TABLE [ums].[USER_PRODUCT]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [ums].[USER] ([Id])
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS]  WITH CHECK ADD  CONSTRAINT [FK__USER_PROD__Produ__50FB042B] FOREIGN KEY([ProductProcessId])
REFERENCES [lob].[PRODUCT_PROCESS_PR] ([Id])
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS] CHECK CONSTRAINT [FK__USER_PROD__Produ__50FB042B]
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS]  WITH CHECK ADD FOREIGN KEY([UserProductId])
REFERENCES [ums].[USER_PRODUCT] ([Id])
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS_SUBPROCESS]  WITH CHECK ADD  CONSTRAINT [FK__USER_PROD__Produ__57A801BA] FOREIGN KEY([ProductProcessSubprocessId])
REFERENCES [lob].[PRODUCT_PROCESS_SUBPROCESS_PR] ([Id])
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS_SUBPROCESS] CHECK CONSTRAINT [FK__USER_PROD__Produ__57A801BA]
GO
ALTER TABLE [ums].[USER_PRODUCT_PROCESS_SUBPROCESS]  WITH CHECK ADD FOREIGN KEY([UserProductProcessId])
REFERENCES [ums].[USER_PRODUCT_PROCESS] ([Id])
GO
ALTER TABLE [ums].[USER_ROLE]  WITH CHECK ADD  CONSTRAINT [FK_USER_ROLE_RoleId] FOREIGN KEY([RoleId])
REFERENCES [ums].[ROLE_PR] ([Id])
GO
ALTER TABLE [ums].[USER_ROLE] CHECK CONSTRAINT [FK_USER_ROLE_RoleId]
GO
ALTER TABLE [ums].[USER_ROLE]  WITH CHECK ADD  CONSTRAINT [FK_USER_ROLE_UserId] FOREIGN KEY([UserId])
REFERENCES [ums].[USER] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ums].[USER_ROLE] CHECK CONSTRAINT [FK_USER_ROLE_UserId]
GO
/****** Object:  StoredProcedure [app].[GetProcessLogs]    Script Date: 10/11/2025 5:17:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [app].[GetProcessLogs]
    @culture NVARCHAR(10)

AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pl.Id,
        pl.Description,
        uu.Id AS UserId,
        uu.CreatedBy,
		 CASE 
            WHEN @culture = 'en' THEN tt.ValueEn
            ELSE tt.ValueAr
        END AS Process,
        CAST(pl.LogDate AS time) AS TimeOnly,
        pl.TerminalId,
        pl.IPAddress,
		  'Admin' AS PortalName      
    FROM 
        [WARBA_B2B_DEV].[app].[PROCESS_LOG] AS pl
    LEFT JOIN 
        [WARBA_B2B_DEV].[ums].[USER] AS uu 
        ON pl.CreatedBy = uu.Id
    LEFT JOIN 
        [WARBA_B2B_DEV].[app].[Translation] AS tt 
        ON pl.Process = tt.[Key]   
    ORDER BY 
        pl.LogDate DESC;
END;
GO
USE [master]
GO
ALTER DATABASE [WARBA_B2B_DEV] SET  READ_WRITE 
GO
