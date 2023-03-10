/****** Object:  Database [MySample]    Script Date: 6.02.2023 16:53:10 ******/
CREATE DATABASE [MySample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MySample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MySample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MySample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MySample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MySample] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MySample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MySample] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MySample] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MySample] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MySample] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MySample] SET ARITHABORT OFF 
GO
ALTER DATABASE [MySample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MySample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MySample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MySample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MySample] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MySample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MySample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MySample] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MySample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MySample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MySample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MySample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MySample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MySample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MySample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MySample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MySample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MySample] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MySample] SET  MULTI_USER 
GO
ALTER DATABASE [MySample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MySample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MySample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MySample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MySample] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MySample] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MySample] SET QUERY_STORE = OFF
GO
/****** Object:  Table [dbo].[PageObjects]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageObjects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageID] [int] NOT NULL,
	[PageObjectName] [nvarchar](150) NOT NULL,
	[ServicesName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdateDate] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_PageObjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BindPageId] [int] NULL,
	[PageName] [nvarchar](150) NOT NULL,
	[IconName] [nvarchar](50) NULL,
	[PageURL] [nvarchar](250) NULL,
	[PageLevel] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdateDate] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePage]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_RolePage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePageObject]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePageObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[PageObjectID] [int] NOT NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_RolePageObject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdateDate] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](150) NULL,
	[Descriptions] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupID] [int] NOT NULL,
	[NameSurname] [nvarchar](250) NOT NULL,
	[UserName] [nvarchar](150) NOT NULL,
	[EMail] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[GSM] [nvarchar](50) NULL,
	[UserType] [nvarchar](50) NOT NULL,
	[BirthDay] [date] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdateDate] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 6.02.2023 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdateDate] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[ActivationStatus] [int] NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

INSERT [dbo].[Pages] ([ID], [BindPageId], [PageName], [IconName], [PageURL], [PageLevel], [Description], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (1, NULL, N'User Info', N'icon-user', NULL, 0, NULL, CAST(N'2023-01-11T00:00:00.0000000' AS DateTime2), 5, NULL, NULL, 1)
INSERT [dbo].[Pages] ([ID], [BindPageId], [PageName], [IconName], [PageURL], [PageLevel], [Description], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (3, 1, N'User List', N'icon-list', N'User/Index', 1, N'Users List', CAST(N'2023-01-11T00:00:00.0000000' AS DateTime2), 5, NULL, NULL, 1)
INSERT [dbo].[Pages] ([ID], [BindPageId], [PageName], [IconName], [PageURL], [PageLevel], [Description], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (5, 1, N'New User', N'icon-plus', N'User/NewUser', 1, N'New User', CAST(N'2023-01-11T00:00:00.0000000' AS DateTime2), 5, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[RolePage] ON 

INSERT [dbo].[RolePage] ([ID], [PageID], [RoleID], [ActivationStatus]) VALUES (1, 1, 1, 1)
INSERT [dbo].[RolePage] ([ID], [PageID], [RoleID], [ActivationStatus]) VALUES (2, 3, 1, 1)
INSERT [dbo].[RolePage] ([ID], [PageID], [RoleID], [ActivationStatus]) VALUES (3, 5, 1, 1)
SET IDENTITY_INSERT [dbo].[RolePage] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [RoleName], [Description], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (1, N'SuperAdmin', N'Super Admin', CAST(N'2023-01-11T00:00:00.0000000' AS DateTime2), 5, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGroup] ON 

INSERT [dbo].[UserGroup] ([ID], [GroupName], [Descriptions], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (1, N'Admin', N'Admin Group', CAST(N'2022-12-24T00:00:00.000' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([ID], [UserGroupID], [NameSurname], [UserName], [EMail], [Password], [GSM], [UserType], [BirthDay], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (5, 1, N'Admin', N'Admin', N'Admin@Admin.com', N'y2mWKUZhfaAGovlXdteLSeXseUHSvbLSXNsF+Vf2Q0Q=', N'Admin', N'Admin', CAST(N'2020-01-11' AS Date), CAST(N'2022-12-24T19:51:30.2433333' AS DateTime2), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([ID], [UserID], [RoleID], [CreatedDate], [CreatedBy], [UpdateDate], [UpdateBy], [ActivationStatus]) VALUES (2, 5, 1, CAST(N'2023-01-11T00:00:00.0000000' AS DateTime2), 5, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
ALTER TABLE [dbo].[PageObjects]  WITH CHECK ADD  CONSTRAINT [FK_PageObjects_PageObjects] FOREIGN KEY([PageID])
REFERENCES [dbo].[Pages] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageObjects] CHECK CONSTRAINT [FK_PageObjects_PageObjects]
GO
ALTER TABLE [dbo].[RolePageObject]  WITH CHECK ADD  CONSTRAINT [FK_RolePageObject_PageObjects] FOREIGN KEY([PageObjectID])
REFERENCES [dbo].[PageObjects] ([ID])
GO
ALTER TABLE [dbo].[RolePageObject] CHECK CONSTRAINT [FK_RolePageObject_PageObjects]
GO
ALTER TABLE [dbo].[RolePageObject]  WITH CHECK ADD  CONSTRAINT [FK_RolePageObject_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[RolePageObject] CHECK CONSTRAINT [FK_RolePageObject_Roles]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_UserGroup] FOREIGN KEY([UserGroupID])
REFERENCES [dbo].[UserGroup] ([ID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_UserGroup]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_UserInfo]
GO
ALTER DATABASE [MySample] SET  READ_WRITE 
GO
