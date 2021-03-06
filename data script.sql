USE [durianDb]
GO
/****** Object:  Table [dbo].[MemberUser]    Script Date: 10/23/2014 19:04:25 ******/
SET IDENTITY_INSERT [dbo].[MemberUser] ON
INSERT [dbo].[MemberUser] ([UserId], [Username], [Password], [Email], [FirstName], [LastName], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate]) VALUES (1, N'na', N'1234', N'kowjongll@gmail.com', N'Krisana', N'Wangyai', NULL, NULL, NULL, NULL)
INSERT [dbo].[MemberUser] ([UserId], [Username], [Password], [Email], [FirstName], [LastName], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate]) VALUES (2, N'north', N'1234', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MemberUser] OFF
/****** Object:  Table [dbo].[MemberRole]    Script Date: 10/23/2014 19:04:25 ******/
/****** Object:  Table [dbo].[Group]    Script Date: 10/23/2014 19:04:25 ******/
/****** Object:  Table [dbo].[Category]    Script Date: 10/23/2014 19:04:25 ******/
/****** Object:  Table [dbo].[Author]    Script Date: 10/23/2014 19:04:25 ******/
SET IDENTITY_INSERT [dbo].[Author] ON
INSERT [dbo].[Author] ([AuthorId], [FirstNameTH], [LastNameTH], [FirstNameEN], [LastNameEN], [AuthorStory]) VALUES (1, N'Krisana', N'Wangyai', N'Krisana', N'Wangyai', N'1234')
SET IDENTITY_INSERT [dbo].[Author] OFF
/****** Object:  Table [dbo].[Course]    Script Date: 10/23/2014 19:04:25 ******/
SET IDENTITY_INSERT [dbo].[Course] ON
INSERT [dbo].[Course] ([CourseId], [AuthorId], [CourseName], [CourseDetail], [CourseLevel], [CourseSubject], [CourseTools], [PublishDate], [GroupId], [CategoryId]) VALUES (1, 1, N'Microsoft SQL Server 2010', N'1 2 3 4', N'1', N'Database', N'MS SQL 2010', CAST(0x0000A3C000000000 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Course] OFF
/****** Object:  Table [dbo].[CourseTopic]    Script Date: 10/23/2014 19:04:25 ******/
SET IDENTITY_INSERT [dbo].[CourseTopic] ON
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (1, 1, N'Introduction', 1)
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (2, 1, N'Dynamic Columns', 2)
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (5, 1, N'The Power of CONNECT', 3)
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (6, 1, N'Advanced CONNECT Storage Engine', 4)
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (7, 1, N'The Cassandra Storage Engine', 5)
INSERT [dbo].[CourseTopic] ([CourseTopicId], [CourseId], [CourseTopic], [CourseTopicOrder]) VALUES (8, 1, N'Conclusion', 6)
SET IDENTITY_INSERT [dbo].[CourseTopic] OFF
/****** Object:  Table [dbo].[CourseDetail]    Script Date: 10/23/2014 19:04:25 ******/
SET IDENTITY_INSERT [dbo].[CourseDetail] ON
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (1, 1, 1, N'Welcome', N'xxxxx', 1, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (2, 1, 1, N'What you should know before watching this course', N'xxxxx', 2, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (3, 1, 1, N'Using the exercise files', N'xxxxx', 3, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (4, 1, 1, N'Why use MariaDB?', N'xxxxx', 4, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (6, 1, 2, N'Why use a dynamic column?', N'ee', 1, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (7, 1, 2, N'Creating dynamic columns', N'ee', 2, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (8, 1, 2, N'Querying dynamic columns', N'ee', 3, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (9, 1, 2, N'Updating dynamic columns', N'ee', 4, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (10, 1, 2, N'Adding a dynamic column part to all records', N'ee', 5, NULL)
INSERT [dbo].[CourseDetail] ([CourseDetailId], [CourseId], [CourseTopicId], [CourseDetail], [CourseSource], [CourseDetailOrder], [IsPublish]) VALUES (11, 1, 2, N'Nested dynamic columns', N'ee', 6, NULL)
SET IDENTITY_INSERT [dbo].[CourseDetail] OFF
