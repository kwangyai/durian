USE [master]
GO
/****** Object:  Database [durianDb]    Script Date: 10/19/2014 19:59:14 ******/
CREATE DATABASE [durianDb] ON  PRIMARY 
( NAME = N'durianDb', FILENAME = N'D:\Database\durianDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'durianDb_log', FILENAME = N'D:\Database\durianDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [durianDb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [durianDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [durianDb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [durianDb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [durianDb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [durianDb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [durianDb] SET ARITHABORT OFF
GO
ALTER DATABASE [durianDb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [durianDb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [durianDb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [durianDb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [durianDb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [durianDb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [durianDb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [durianDb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [durianDb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [durianDb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [durianDb] SET  DISABLE_BROKER
GO
ALTER DATABASE [durianDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [durianDb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [durianDb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [durianDb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [durianDb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [durianDb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [durianDb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [durianDb] SET  READ_WRITE
GO
ALTER DATABASE [durianDb] SET RECOVERY SIMPLE
GO
ALTER DATABASE [durianDb] SET  MULTI_USER
GO
ALTER DATABASE [durianDb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [durianDb] SET DB_CHAINING OFF
GO
USE [durianDb]
GO
/****** Object:  Table [dbo].[MemberUser]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[CreateBy] [nvarchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateBy] [nvarchar](250) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberRole]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](250) NULL,
 CONSTRAINT [PK_MemberRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](250) NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[FirstNameTH] [nvarchar](250) NULL,
	[LastNameTH] [nvarchar](50) NULL,
	[FirstNameEN] [nvarchar](250) NULL,
	[LastNameEN] [nvarchar](50) NULL,
	[AuthorStory] [text] NULL,
 CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NULL,
	[CourseName] [nvarchar](250) NULL,
	[CourseDetail] [nvarchar](3000) NULL,
	[CourseLevel] [char](1) NULL,
	[CourseSubject] [nvarchar](250) NULL,
	[CourseTools] [nvarchar](250) NULL,
	[PublishDate] [datetime] NULL,
	[GroupId] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseTopic]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseTopic](
	[CourseTopicId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NULL,
	[CourseTopic] [nvarchar](350) NOT NULL,
	[CourseTopicOrder] [int] NULL,
 CONSTRAINT [PK_CourseTopic] PRIMARY KEY CLUSTERED 
(
	[CourseTopicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseDetail]    Script Date: 10/19/2014 19:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseDetail](
	[CourseDetailId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NULL,
	[CourseTopicId] [int] NULL,
	[CourseDetail] [nvarchar](350) NULL,
	[CourseSource] [nvarchar](3000) NULL,
	[CourseDetailOrder] [int] NULL,
	[IsPublish] [bit] NULL,
 CONSTRAINT [PK_CourseDetail] PRIMARY KEY CLUSTERED 
(
	[CourseDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Course_Author]    Script Date: 10/19/2014 19:59:15 ******/
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Author]
GO
/****** Object:  ForeignKey [FK_CourseTopic_Course]    Script Date: 10/19/2014 19:59:15 ******/
ALTER TABLE [dbo].[CourseTopic]  WITH CHECK ADD  CONSTRAINT [FK_CourseTopic_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[CourseTopic] CHECK CONSTRAINT [FK_CourseTopic_Course]
GO
/****** Object:  ForeignKey [FK_CourseDetail_CourseTopic]    Script Date: 10/19/2014 19:59:15 ******/
ALTER TABLE [dbo].[CourseDetail]  WITH CHECK ADD  CONSTRAINT [FK_CourseDetail_CourseTopic] FOREIGN KEY([CourseTopicId])
REFERENCES [dbo].[CourseTopic] ([CourseTopicId])
GO
ALTER TABLE [dbo].[CourseDetail] CHECK CONSTRAINT [FK_CourseDetail_CourseTopic]
GO
