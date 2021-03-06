USE [nature-net]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Action](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[object_type] [nvarchar](64) NOT NULL,
	[object_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Action_Type]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Action_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Activity]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](32) NOT NULL,
	[avatar] [nvarchar](64) NULL,
	[description] [nvarchar](max) NULL,
	[creation_date] [datetime] NOT NULL,
	[expire_date] [datetime] NULL,
	[location_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Collection]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
	[activity_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Collection_Contribution_Mapping]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection_Contribution_Mapping](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[contribution_id] [int] NOT NULL,
	[collection_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Collection_Contribution_Mapping] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contribution]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contribution](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[note] [nvarchar](max) NULL,
	[media_url] [nvarchar](max) NULL,
	[tags] [nvarchar](max) NULL,
	[date] [datetime] NOT NULL,
	[location_id] [int] NOT NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contribution] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[note] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
	[type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[object_type] [nvarchar](64) NOT NULL,
	[object] [binary](1) NULL,
	[object_id] [int] NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedback_Type]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback_Type](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
	[data_type] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Feedback_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](64) NOT NULL,
	[email] [nvarchar](64) NULL,
	[password] [nchar](128) NULL,
	[avatar] [nvarchar](64) NULL,
	[technical_info] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[Design_Ideas]    Script Date: 3/18/2014 1:21:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Design_Ideas]
AS
SELECT     dbo.Contribution.*, dbo.[User].name, dbo.[User].avatar
FROM         dbo.Activity INNER JOIN
                      dbo.Collection ON dbo.Activity.id = dbo.Collection.activity_id INNER JOIN
                      dbo.Collection_Contribution_Mapping ON dbo.Collection.id = dbo.Collection_Contribution_Mapping.collection_id INNER JOIN
					  dbo.Contribution ON dbo.Collection_Contribution_Mapping.contribution_id = dbo.Contribution.id INNER JOIN
                      dbo.[User] ON dbo.Collection.user_id = dbo.[User].id
WHERE     (dbo.Activity.name = 'Design Idea')

GO
SET IDENTITY_INSERT [dbo].[Action_Type] ON 

INSERT [dbo].[Action_Type] ([id], [name], [description]) VALUES (1, N'Add', NULL)
INSERT [dbo].[Action_Type] ([id], [name], [description]) VALUES (2, N'Modify', NULL)
INSERT [dbo].[Action_Type] ([id], [name], [description]) VALUES (3, N'Delete', NULL)
SET IDENTITY_INSERT [dbo].[Action_Type] OFF
SET IDENTITY_INSERT [dbo].[Activity] ON 

INSERT [dbo].[Activity] ([id], [name], [avatar], [description], [creation_date], [expire_date], [location_id], [technical_info]) VALUES (0, N'Free Observation', NULL, NULL, CAST(0x0000A25E00000000 AS DateTime), NULL, 0, NULL)
INSERT [dbo].[Activity] ([id], [name], [avatar], [description], [creation_date], [expire_date], [location_id], [technical_info]) VALUES (1, N'Design Idea', NULL, NULL, CAST(0x0000A25E00000000 AS DateTime), NULL, 0, NULL)
INSERT [dbo].[Activity] ([id], [name], [avatar], [description], [creation_date], [expire_date], [location_id], [technical_info]) VALUES (2, N'Stump a naturalist!', NULL, N'Got a question about something you find at Hallam Lake? Make an observation about it and see if you can stump one of our ACES naturalists!', CAST(0x0000A25E00000000 AS DateTime), NULL, 0, NULL)
INSERT [dbo].[Activity] ([id], [name], [avatar], [description], [creation_date], [expire_date], [location_id], [technical_info]) VALUES (3, N'Tell us what you hear', NULL, N'Make an observation of somewhere where you''re surrounded by the sounds of nature, then stop and listen for a minute. What did you hear? Tell us all about it.', CAST(0x0000A25E00000000 AS DateTime), NULL, 0, NULL)
INSERT [dbo].[Activity] ([id], [name], [avatar], [description], [creation_date], [expire_date], [location_id], [technical_info]) VALUES (4, N'Tracks in the snow', NULL, N'Animals may be hard to spot at this time of year, but evidence of their passing is all around us. See if you can spot any animal tracks in snow or dirt, and take a guess what kind of animal it was!', CAST(0x0000A25E00000000 AS DateTime), NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Activity] OFF
SET IDENTITY_INSERT [dbo].[Collection] ON 

INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (0, N'Default Collection', NULL, 0, 0, CAST(0x0000A24500CC9ED0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (1, N'26 Sep 2013', NULL, 0, 2, CAST(0x0000A24500CC9ED0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (2, N'27 Sep 2013', NULL, 0, 2, CAST(0x0000A24600914B50 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (3, N'30 Sep 2013', NULL, 0, 2, CAST(0x0000A2490092AAE0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (4, N'30 Sep 2013', NULL, 0, 4, CAST(0x0000A24900B54640 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (5, N'4 Oct 2013', NULL, 0, 5, CAST(0x0000A24D00DC0050 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (6, N'4 Oct 2013', NULL, 0, 2, CAST(0x0000A24D00DC46A0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (7, N'4 Oct 2013', NULL, 0, 6, CAST(0x0000A24D00DC46A0 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (8, N'7 Oct 2013', NULL, 0, 9, CAST(0x0000A25000BAC480 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (9, N'8 Oct 2013', NULL, 1, 11, CAST(0x0000A25101137B70 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (10, N'9 Oct 2013', NULL, 1, 12, CAST(0x0000A25200F77790 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (11, N'10 Oct 2013', NULL, 1, 8, CAST(0x0000A25300F31290 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (12, N'10 Oct 2013', NULL, 1, 12, CAST(0x0000A25301133520 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (13, N'5 Dec 2013', NULL, 0, 55, CAST(0x0000A28B00DAD83D AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (14, N'12 Dec 2013', NULL, 3, 56, CAST(0x0000A292012928EC AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (15, N'18 Dec 2013', NULL, 4, 57, CAST(0x0000A29800A35D65 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (16, N'25 Nov 2013', NULL, 0, 56, CAST(0x0000A28101099FCD AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (17, N'11 Oct 2013', NULL, 0, 38, CAST(0x0000A254015B1E42 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (18, N'14 Oct 2013', NULL, 0, 41, CAST(0x0000A25701637E00 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (19, N'15 Oct 2013', NULL, 0, 37, CAST(0x0000A25800E8F24D AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (20, N'16 Oct 2013', NULL, 0, 43, CAST(0x0000A25900374AF5 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (21, N'14 Nov 2013', NULL, 0, 58, CAST(0x0000A276017E53B9 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (22, N'15 Nov 2013', NULL, 4, 58, CAST(0x0000A27700B411F7 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (23, N'15 Nov 2013', NULL, 0, 58, CAST(0x0000A27700B3D1B2 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (24, N'15 Nov 2013', NULL, 3, 58, CAST(0x0000A2770095F694 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (25, N'18 Nov 2013', NULL, 0, 48, CAST(0x0000A27A0104BC49 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (26, N'18 Nov 2013', NULL, 0, 40, CAST(0x0000A27A0107F508 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (27, N'19 Nov 2013', NULL, 2, 59, CAST(0x0000A27B0158AEA9 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (28, N'20 Nov 2013', NULL, 3, 56, CAST(0x0000A27C00D84199 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (29, N'19 Nov 2013', NULL, 3, 59, CAST(0x0000A27B0159413F AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (30, N'19 Nov 2013', NULL, 4, 59, CAST(0x0000A27B01598D82 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (31, N'21 Nov 2013', NULL, 3, 60, CAST(0x0000A27D0110EC7B AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (32, N'22 Nov 2013', NULL, 4, 61, CAST(0x0000A27E00B98862 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (33, N'22 Nov 2013', NULL, 4, 58, CAST(0x0000A27E0007EBFF AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (34, N'22 Nov 2013', NULL, 3, 61, CAST(0x0000A27E00B9B147 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (35, N'22 Nov 2013', NULL, 2, 61, CAST(0x0000A27E00B91705 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (36, N'22 Nov 2013', NULL, 2, 62, CAST(0x0000A27E00C0AA59 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (37, N'22 Nov 2013', NULL, 4, 62, CAST(0x0000A27E00C119C4 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (38, N'25 Nov 2013', NULL, 0, 14, CAST(0x0000A28100F027B4 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (39, N'26 Nov 2013', NULL, 0, 63, CAST(0x0000A28200D37928 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (40, N'26 Nov 2013', NULL, 0, 64, CAST(0x0000A28200D4139C AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (41, N'26 Nov 2013', NULL, 4, 65, CAST(0x0000A28200D79B4A AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (42, N'26 Nov 2013', NULL, 4, 66, CAST(0x0000A28200E8CA10 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (43, N'26 Nov 2013', NULL, 4, 57, CAST(0x0000A28200F1C528 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (44, N'25 Nov 2013', NULL, 2, 14, CAST(0x0000A28100F174AB AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (45, N'27 Nov 2013', NULL, 0, 63, CAST(0x0000A283001D169D AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (46, N'12 Oct 2013', NULL, 0, 40, CAST(0x0000A255012AC66F AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (47, N'12 Oct 2013', NULL, 0, 31, CAST(0x0000A25501299698 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (48, N'14 Oct 2013', NULL, 0, 37, CAST(0x0000A25700D37C24 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (49, N'15 Oct 2013', NULL, 0, 41, CAST(0x0000A25800DEC4BA AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (50, N'16 Oct 2013', NULL, 0, 37, CAST(0x0000A259002B304C AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (51, N'17 Oct 2013', NULL, 0, 41, CAST(0x0000A25A00EB9FAC AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (52, N'17 Oct 2013', NULL, 0, 43, CAST(0x0000A25A00A2B5C1 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (53, N'21 Oct 2013', NULL, 0, 18, CAST(0x0000A25E00E7B2AC AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (54, N'7 Oct 2013', NULL, 0, 10, CAST(0x0000A25000E765E6 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (55, N'7 Oct 2013', NULL, 0, 11, CAST(0x0000A2500127C7A8 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (56, N'8 Oct 2013', NULL, 0, 11, CAST(0x0000A25100CAEE79 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (57, N'9 Oct 2013', NULL, 0, 9, CAST(0x0000A252011CEE2C AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (58, N'10 Oct 2013', NULL, 0, 13, CAST(0x0000A25300E6240C AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (59, N'10 Oct 2013', NULL, 0, 14, CAST(0x0000A25300E70FCC AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (60, N'10 Oct 2013', NULL, 0, 32, CAST(0x0000A253012681F3 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (61, N'10 Oct 2013', NULL, 0, 16, CAST(0x0000A253012B1539 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (62, N'10 Oct 2013', NULL, 0, 17, CAST(0x0000A253012E32FE AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (63, N'10 Oct 2013', NULL, 0, 30, CAST(0x0000A2530125EDD6 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (64, N'10 Oct 2013', NULL, 0, 33, CAST(0x0000A253012D5C74 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (65, N'10 Oct 2013', NULL, 0, 18, CAST(0x0000A25301312536 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (66, N'11 Oct 2013', NULL, 0, 34, CAST(0x0000A25400EBA4CF AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (67, N'12 Oct 2013', NULL, 0, 39, CAST(0x0000A25500CB8BA9 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (68, N'25 Nov 2013', NULL, 2, 15, CAST(0x0000A2810115CC58 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (69, N'25 Nov 2013', NULL, 2, 57, CAST(0x0000A2810120A37C AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (70, N'26 Nov 2013', NULL, 3, 65, CAST(0x0000A28200CF91D4 AS DateTime), NULL)
INSERT [dbo].[Collection] ([id], [name], [description], [activity_id], [user_id], [date], [technical_info]) VALUES (71, N'24 Jan 2014', NULL, 0, 56, CAST(0x0000A2BD00B3F200 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Collection] OFF
SET IDENTITY_INSERT [dbo].[Collection_Contribution_Mapping] ON 

INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (1, CAST(0x0000A24500CC9ED0 AS DateTime), 1, 1, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (2, CAST(0x0000A24500D6C860 AS DateTime), 2, 1, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (3, CAST(0x0000A245010D7090 AS DateTime), 3, 1, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (4, CAST(0x0000A24600914B50 AS DateTime), 4, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (5, CAST(0x0000A24600914B50 AS DateTime), 5, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (6, CAST(0x0000A24600914B50 AS DateTime), 6, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (7, CAST(0x0000A24600914B50 AS DateTime), 7, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (8, CAST(0x0000A24600914B50 AS DateTime), 8, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (9, CAST(0x0000A24600914B50 AS DateTime), 9, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (10, CAST(0x0000A24600914B50 AS DateTime), 10, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (11, CAST(0x0000A24600914B50 AS DateTime), 11, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (12, CAST(0x0000A246009A1550 AS DateTime), 12, 2, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (13, CAST(0x0000A2490092AAE0 AS DateTime), 13, 3, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (14, CAST(0x0000A2490092AAE0 AS DateTime), 14, 3, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (15, CAST(0x0000A24900B54640 AS DateTime), 15, 4, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (16, CAST(0x0000A24D00DC0050 AS DateTime), 16, 5, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (17, CAST(0x0000A24D00DC46A0 AS DateTime), 17, 6, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (18, CAST(0x0000A24D00DC46A0 AS DateTime), 18, 7, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (19, CAST(0x0000A25000BAC480 AS DateTime), 19, 8, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (20, CAST(0x0000A25000F77790 AS DateTime), 20, 8, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (21, CAST(0x0000A25101137B70 AS DateTime), 22, 9, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (22, CAST(0x0000A2510113C1C0 AS DateTime), 23, 9, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (23, CAST(0x0000A2510115ADF0 AS DateTime), 24, 9, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (24, CAST(0x0000A2510115F440 AS DateTime), 25, 9, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (25, CAST(0x0000A25200F77790 AS DateTime), 26, 10, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (26, CAST(0x0000A25200F890D0 AS DateTime), 27, 10, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (27, CAST(0x0000A25200FB9640 AS DateTime), 28, 10, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (28, CAST(0x0000A25200FCAF80 AS DateTime), 29, 10, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (29, CAST(0x0000A25200FCF5D0 AS DateTime), 30, 10, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (30, CAST(0x0000A25300F31290 AS DateTime), 31, 11, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (31, CAST(0x0000A25301133520 AS DateTime), 32, 12, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (32, CAST(0x0000A25301140810 AS DateTime), 33, 12, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (33, CAST(0x0000A253011494B0 AS DateTime), 34, 12, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (34, CAST(0x0000A2B50103E8AD AS DateTime), 35, 13, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (35, CAST(0x0000A2B50103E8C0 AS DateTime), 36, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (36, CAST(0x0000A2B50103E8CE AS DateTime), 37, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (37, CAST(0x0000A2B50103E8E0 AS DateTime), 38, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (38, CAST(0x0000A2B50103E8EE AS DateTime), 39, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (39, CAST(0x0000A2B50103E8FC AS DateTime), 40, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (40, CAST(0x0000A2B50103E90F AS DateTime), 41, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (41, CAST(0x0000A2B50103E91D AS DateTime), 42, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (42, CAST(0x0000A2B50103E92B AS DateTime), 43, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (43, CAST(0x0000A2B50103E939 AS DateTime), 44, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (44, CAST(0x0000A2B50103E947 AS DateTime), 45, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (45, CAST(0x0000A2B50103E956 AS DateTime), 46, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (46, CAST(0x0000A2B50103E964 AS DateTime), 47, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (47, CAST(0x0000A2B50103E96D AS DateTime), 48, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (48, CAST(0x0000A2B50103E97B AS DateTime), 49, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (49, CAST(0x0000A2B50103E989 AS DateTime), 50, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (50, CAST(0x0000A2B50103E99C AS DateTime), 51, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (51, CAST(0x0000A2B50103E9AA AS DateTime), 52, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (52, CAST(0x0000A2B50103E9B8 AS DateTime), 53, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (53, CAST(0x0000A2B50103E9C6 AS DateTime), 54, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (54, CAST(0x0000A2B50103E9D4 AS DateTime), 55, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (55, CAST(0x0000A2B50103E9E2 AS DateTime), 56, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (56, CAST(0x0000A2B50103E9F0 AS DateTime), 57, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (57, CAST(0x0000A2B50103EA1A AS DateTime), 58, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (58, CAST(0x0000A2B50103EA28 AS DateTime), 59, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (59, CAST(0x0000A2B50103EA37 AS DateTime), 60, 14, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (60, CAST(0x0000A2B50103EA45 AS DateTime), 61, 15, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (61, CAST(0x0000A2B50103EA57 AS DateTime), 62, 15, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (62, CAST(0x0000A2B50103EA65 AS DateTime), 63, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (63, CAST(0x0000A2B5011E34B8 AS DateTime), 64, 17, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (64, CAST(0x0000A2B5011E34D0 AS DateTime), 65, 18, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (65, CAST(0x0000A2B5011E34E7 AS DateTime), 66, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (66, CAST(0x0000A2B5011E34FF AS DateTime), 67, 20, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (67, CAST(0x0000A2B5011E351B AS DateTime), 68, 21, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (68, CAST(0x0000A2B5011E3529 AS DateTime), 69, 22, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (69, CAST(0x0000A2B5011E353B AS DateTime), 70, 23, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (70, CAST(0x0000A2B5011E354A AS DateTime), 71, 24, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (71, CAST(0x0000A2B5011E355C AS DateTime), 72, 24, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (72, CAST(0x0000A2B5011E356F AS DateTime), 73, 25, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (73, CAST(0x0000A2B5011E3586 AS DateTime), 74, 26, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (74, CAST(0x0000A2B5011E3595 AS DateTime), 75, 27, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (75, CAST(0x0000A2B5011E35A7 AS DateTime), 76, 28, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (76, CAST(0x0000A2B5011E35BA AS DateTime), 77, 29, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (77, CAST(0x0000A2B5011E35C8 AS DateTime), 78, 30, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (78, CAST(0x0000A2B5011E35D6 AS DateTime), 79, 31, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (79, CAST(0x0000A2B5011E35E9 AS DateTime), 80, 32, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (80, CAST(0x0000A2B5011E35F7 AS DateTime), 81, 31, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (81, CAST(0x0000A2B5011E3605 AS DateTime), 82, 31, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (82, CAST(0x0000A2B5011E3618 AS DateTime), 83, 33, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (83, CAST(0x0000A2B5011E362B AS DateTime), 84, 34, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (84, CAST(0x0000A2B5011E3639 AS DateTime), 85, 35, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (85, CAST(0x0000A2B5011E3647 AS DateTime), 86, 36, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (86, CAST(0x0000A2B5011E3655 AS DateTime), 87, 37, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (87, CAST(0x0000A2B5011E3663 AS DateTime), 88, 38, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (88, CAST(0x0000A2B5011E3676 AS DateTime), 89, 38, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (89, CAST(0x0000A2B5011E3684 AS DateTime), 90, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (90, CAST(0x0000A2B5011E3692 AS DateTime), 91, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (91, CAST(0x0000A2B5011E36A4 AS DateTime), 92, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (92, CAST(0x0000A2B5011E36B2 AS DateTime), 93, 40, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (93, CAST(0x0000A2B5011E36C1 AS DateTime), 94, 41, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (94, CAST(0x0000A2B5011E36D3 AS DateTime), 95, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (95, CAST(0x0000A2B5011E36E1 AS DateTime), 96, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (96, CAST(0x0000A2B5011E36F4 AS DateTime), 97, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (97, CAST(0x0000A2B5011E3702 AS DateTime), 98, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (98, CAST(0x0000A2B5011E3715 AS DateTime), 99, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (99, CAST(0x0000A2B5011E3723 AS DateTime), 100, 42, NULL)
GO
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (100, CAST(0x0000A2B5011E3736 AS DateTime), 101, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (101, CAST(0x0000A2B5011E3744 AS DateTime), 102, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (102, CAST(0x0000A2B5011E3757 AS DateTime), 103, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (103, CAST(0x0000A2B5011E3765 AS DateTime), 104, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (104, CAST(0x0000A2B5011E3773 AS DateTime), 105, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (105, CAST(0x0000A2B5011E3785 AS DateTime), 106, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (106, CAST(0x0000A2B5011E3793 AS DateTime), 107, 44, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (107, CAST(0x0000A2B5011E37A2 AS DateTime), 108, 16, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (108, CAST(0x0000A2B5011E37B4 AS DateTime), 109, 44, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (109, CAST(0x0000A2B5011E37C2 AS DateTime), 110, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (110, CAST(0x0000A2B5011E37D5 AS DateTime), 111, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (111, CAST(0x0000A2BC0135C5F0 AS DateTime), 112, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (112, CAST(0x0000A2BC0135C603 AS DateTime), 113, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (113, CAST(0x0000A2BC0135C611 AS DateTime), 114, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (114, CAST(0x0000A2BC0135C61F AS DateTime), 115, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (115, CAST(0x0000A2BC0135C632 AS DateTime), 116, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (116, CAST(0x0000A2BC0135C644 AS DateTime), 117, 45, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (117, CAST(0x0000A2BC0135C652 AS DateTime), 118, 45, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (118, CAST(0x0000A2BC0135C661 AS DateTime), 119, 45, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (119, CAST(0x0000A2BC0135C673 AS DateTime), 120, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (120, CAST(0x0000A2BC01364963 AS DateTime), 121, 46, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (121, CAST(0x0000A2BC0136497B AS DateTime), 122, 47, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (122, CAST(0x0000A2BC0136498E AS DateTime), 123, 47, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (123, CAST(0x0000A2BC013649A5 AS DateTime), 124, 47, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (124, CAST(0x0000A2BC013649BC AS DateTime), 125, 47, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (125, CAST(0x0000A2BC013649CF AS DateTime), 126, 47, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (126, CAST(0x0000A2BC013649F0 AS DateTime), 127, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (127, CAST(0x0000A2BC01364A03 AS DateTime), 128, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (128, CAST(0x0000A2BC01364A1A AS DateTime), 129, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (129, CAST(0x0000A2BC01364A32 AS DateTime), 130, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (130, CAST(0x0000A2BC01364A44 AS DateTime), 131, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (131, CAST(0x0000A2BC01364A57 AS DateTime), 132, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (132, CAST(0x0000A2BC01364A6F AS DateTime), 133, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (133, CAST(0x0000A2BC01364A86 AS DateTime), 134, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (134, CAST(0x0000A2BC01364A99 AS DateTime), 135, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (135, CAST(0x0000A2BC01364AB5 AS DateTime), 136, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (136, CAST(0x0000A2BC01364AC8 AS DateTime), 137, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (137, CAST(0x0000A2BC01364ADF AS DateTime), 138, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (138, CAST(0x0000A2BC01364AF2 AS DateTime), 139, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (139, CAST(0x0000A2BC01364B05 AS DateTime), 140, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (140, CAST(0x0000A2BC01364B1C AS DateTime), 141, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (141, CAST(0x0000A2BC01364B2F AS DateTime), 142, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (142, CAST(0x0000A2BC01364B46 AS DateTime), 143, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (143, CAST(0x0000A2BC01364B59 AS DateTime), 144, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (144, CAST(0x0000A2BC01364B70 AS DateTime), 145, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (145, CAST(0x0000A2BC01364B83 AS DateTime), 146, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (146, CAST(0x0000A2BC01364B9B AS DateTime), 147, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (147, CAST(0x0000A2BC01364BB2 AS DateTime), 148, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (148, CAST(0x0000A2BC01364BC9 AS DateTime), 149, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (149, CAST(0x0000A2BC01364BE1 AS DateTime), 150, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (150, CAST(0x0000A2BC01364BF4 AS DateTime), 151, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (151, CAST(0x0000A2BC01364C0B AS DateTime), 152, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (152, CAST(0x0000A2BC01364C23 AS DateTime), 153, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (153, CAST(0x0000A2BC01364C3A AS DateTime), 154, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (154, CAST(0x0000A2BC01364C51 AS DateTime), 155, 49, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (155, CAST(0x0000A2BC01364C64 AS DateTime), 156, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (156, CAST(0x0000A2BC01364C7C AS DateTime), 157, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (157, CAST(0x0000A2BC01364C93 AS DateTime), 158, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (158, CAST(0x0000A2BC01364CAB AS DateTime), 159, 49, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (159, CAST(0x0000A2BC01364CBD AS DateTime), 160, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (160, CAST(0x0000A2BC01364CD5 AS DateTime), 161, 49, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (161, CAST(0x0000A2BC01364CEC AS DateTime), 162, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (162, CAST(0x0000A2BC01364D04 AS DateTime), 163, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (163, CAST(0x0000A2BC01364D16 AS DateTime), 164, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (164, CAST(0x0000A2BC01364D2E AS DateTime), 165, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (165, CAST(0x0000A2BC01364D45 AS DateTime), 166, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (166, CAST(0x0000A2BC01364D5D AS DateTime), 167, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (167, CAST(0x0000A2BC01364D6F AS DateTime), 168, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (168, CAST(0x0000A2BC01364D87 AS DateTime), 169, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (169, CAST(0x0000A2BC01364D9E AS DateTime), 170, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (170, CAST(0x0000A2BC01364DB1 AS DateTime), 171, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (171, CAST(0x0000A2BC01364DC8 AS DateTime), 172, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (172, CAST(0x0000A2BC01364DE0 AS DateTime), 173, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (173, CAST(0x0000A2BC01364DF3 AS DateTime), 174, 48, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (174, CAST(0x0000A2BC01364E0A AS DateTime), 175, 18, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (175, CAST(0x0000A2BC01364E22 AS DateTime), 176, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (176, CAST(0x0000A2BC01364E39 AS DateTime), 177, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (177, CAST(0x0000A2BC01364E4C AS DateTime), 178, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (178, CAST(0x0000A2BC01364E63 AS DateTime), 179, 19, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (179, CAST(0x0000A2BC01364E7B AS DateTime), 180, 50, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (180, CAST(0x0000A2BC01364E97 AS DateTime), 181, 51, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (181, CAST(0x0000A2BC01364EAE AS DateTime), 182, 52, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (182, CAST(0x0000A2BC01364EC6 AS DateTime), 183, 51, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (183, CAST(0x0000A2BC01364EDD AS DateTime), 184, 52, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (184, CAST(0x0000A2BC01364EF0 AS DateTime), 185, 52, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (185, CAST(0x0000A2BC01364F07 AS DateTime), 186, 53, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (186, CAST(0x0000A2BC0138C297 AS DateTime), 187, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (187, CAST(0x0000A2BC0138C2AF AS DateTime), 188, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (188, CAST(0x0000A2BC0138C2C2 AS DateTime), 189, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (189, CAST(0x0000A2BC0138C2D9 AS DateTime), 190, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (190, CAST(0x0000A2BC0138C303 AS DateTime), 191, 55, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (191, CAST(0x0000A2BC0138C31B AS DateTime), 192, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (192, CAST(0x0000A2BC0138C332 AS DateTime), 193, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (193, CAST(0x0000A2BC0138C345 AS DateTime), 194, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (194, CAST(0x0000A2BC0138C361 AS DateTime), 195, 54, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (195, CAST(0x0000A2BC0138C379 AS DateTime), 196, 56, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (196, CAST(0x0000A2BC0138C390 AS DateTime), 197, 57, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (197, CAST(0x0000A2BC0138C3A3 AS DateTime), 198, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (198, CAST(0x0000A2BC0138C3C8 AS DateTime), 199, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (199, CAST(0x0000A2BC0138C3E4 AS DateTime), 200, 59, NULL)
GO
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (200, CAST(0x0000A2BC0138C3F7 AS DateTime), 201, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (201, CAST(0x0000A2BC0138C40A AS DateTime), 202, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (202, CAST(0x0000A2BC0138C421 AS DateTime), 203, 59, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (203, CAST(0x0000A2BC0138C434 AS DateTime), 204, 59, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (204, CAST(0x0000A2BC0138C44B AS DateTime), 205, 59, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (205, CAST(0x0000A2BC0138C45E AS DateTime), 206, 59, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (206, CAST(0x0000A2BC0138C47A AS DateTime), 207, 60, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (207, CAST(0x0000A2BC0138C48D AS DateTime), 208, 61, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (208, CAST(0x0000A2BC0138C4A5 AS DateTime), 209, 61, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (209, CAST(0x0000A2BC0138C4B7 AS DateTime), 210, 60, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (210, CAST(0x0000A2BC0138C4D8 AS DateTime), 211, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (211, CAST(0x0000A2BC0138C4F0 AS DateTime), 212, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (212, CAST(0x0000A2BC0138C507 AS DateTime), 213, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (213, CAST(0x0000A2BC0138C51A AS DateTime), 214, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (214, CAST(0x0000A2BC0138C531 AS DateTime), 215, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (215, CAST(0x0000A2BC0138C549 AS DateTime), 216, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (216, CAST(0x0000A2BC0138C55B AS DateTime), 217, 58, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (217, CAST(0x0000A2BC0138C573 AS DateTime), 218, 59, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (218, CAST(0x0000A2BC0138C58A AS DateTime), 219, 63, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (219, CAST(0x0000A2BC0138C5A2 AS DateTime), 220, 63, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (220, CAST(0x0000A2BC0138C5B9 AS DateTime), 221, 63, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (221, CAST(0x0000A2BC0138C5CC AS DateTime), 222, 63, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (222, CAST(0x0000A2BC0138C5E3 AS DateTime), 223, 63, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (223, CAST(0x0000A2BC0138C5F6 AS DateTime), 224, 61, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (224, CAST(0x0000A2BC0138C609 AS DateTime), 225, 61, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (225, CAST(0x0000A2BC0138C620 AS DateTime), 226, 64, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (226, CAST(0x0000A2BC0138C633 AS DateTime), 227, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (227, CAST(0x0000A2BC0138C64A AS DateTime), 228, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (228, CAST(0x0000A2BC0138C65D AS DateTime), 229, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (229, CAST(0x0000A2BC0138C670 AS DateTime), 230, 64, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (230, CAST(0x0000A2BC0138C687 AS DateTime), 231, 62, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (231, CAST(0x0000A2BC0138C69F AS DateTime), 232, 65, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (232, CAST(0x0000A2BC0138C6B2 AS DateTime), 233, 60, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (233, CAST(0x0000A2BC0138C6C9 AS DateTime), 234, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (234, CAST(0x0000A2BC0138C6DC AS DateTime), 235, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (235, CAST(0x0000A2BC0138C6F3 AS DateTime), 236, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (236, CAST(0x0000A2BC0138C706 AS DateTime), 237, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (237, CAST(0x0000A2BC0138C71D AS DateTime), 238, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (238, CAST(0x0000A2BC0138C730 AS DateTime), 239, 66, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (239, CAST(0x0000A2BC0138C743 AS DateTime), 240, 17, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (240, CAST(0x0000A2BC0138C75A AS DateTime), 241, 17, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (241, CAST(0x0000A2BC0138C76D AS DateTime), 242, 67, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (242, CAST(0x0000A2BC013C7880 AS DateTime), 243, 68, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (243, CAST(0x0000A2BC013C7893 AS DateTime), 244, 69, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (244, CAST(0x0000A2BC013C78A6 AS DateTime), 245, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (245, CAST(0x0000A2BC013C78B4 AS DateTime), 246, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (246, CAST(0x0000A2BC013C78C2 AS DateTime), 247, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (247, CAST(0x0000A2BC013C78D0 AS DateTime), 248, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (248, CAST(0x0000A2BC013C78DE AS DateTime), 249, 70, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (249, CAST(0x0000A2BC013C78F1 AS DateTime), 250, 40, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (250, CAST(0x0000A2BC013C78FF AS DateTime), 251, 70, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (251, CAST(0x0000A2BC013C790D AS DateTime), 252, 70, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (252, CAST(0x0000A2BC013C791B AS DateTime), 253, 70, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (253, CAST(0x0000A2BC013C7929 AS DateTime), 254, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (254, CAST(0x0000A2BC013C793C AS DateTime), 255, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (255, CAST(0x0000A2BC013C794A AS DateTime), 256, 39, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (256, CAST(0x0000A2BC013C7958 AS DateTime), 257, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (257, CAST(0x0000A2BC013C796A AS DateTime), 258, 42, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (258, CAST(0x0000A2BC013C7979 AS DateTime), 259, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (259, CAST(0x0000A2BC013C798B AS DateTime), 260, 43, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (260, CAST(0x0000A2BD011A2DFC AS DateTime), 261, 71, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (261, CAST(0x0000A2BD011A2E0A AS DateTime), 262, 71, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (262, CAST(0x0000A2BD011A2E14 AS DateTime), 263, 71, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (263, CAST(0x0000A2BD011A2E1D AS DateTime), 264, 71, NULL)
INSERT [dbo].[Collection_Contribution_Mapping] ([id], [date], [contribution_id], [collection_id], [technical_info]) VALUES (264, CAST(0x0000A2BD011A2E2B AS DateTime), 265, 71, NULL)
SET IDENTITY_INSERT [dbo].[Collection_Contribution_Mapping] OFF
SET IDENTITY_INSERT [dbo].[Contribution] ON 

INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (1, NULL, N'1380218582271.jpg', N'Animal', CAST(0x0000A24500CC9ED0 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (2, NULL, N'1380219051751.jpg', N'Plant', CAST(0x0000A24500D6C860 AS DateTime), 6, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (3, NULL, N'1380218748963.jpg', N'Landscape', CAST(0x0000A245010D7090 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (4, NULL, N'1380218865847.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 5, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (5, NULL, N'1380219544910.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (6, NULL, N'1380219166696.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (7, NULL, N'1380218690979.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (8, NULL, N'1380219766692.jpg', N'Insect', CAST(0x0000A24600914B50 AS DateTime), 11, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (9, NULL, N'1380219716995.jpg', N'Animal', CAST(0x0000A24600914B50 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (10, NULL, N'1380219525204.jpg', N'Plant', CAST(0x0000A24600914B50 AS DateTime), 8, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (11, NULL, N'1380219248874.jpg', N'Landscape', CAST(0x0000A24600914B50 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (12, NULL, N'1380218954983.jpg', N'Animal', CAST(0x0000A246009A1550 AS DateTime), 5, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (13, NULL, N'1380218923624.jpg', N'Landscape', CAST(0x0000A2490092AAE0 AS DateTime), 4, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (14, NULL, N'1380218657954.jpg', N'Plant', CAST(0x0000A2490092AAE0 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (15, N'thistle gone to seed', N'1380560221183.jpg', N'Plant', CAST(0x0000A24900B54640 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (16, N'golden eagle', N'1380822975830.jpg', N'other', CAST(0x0000A24D00DC0050 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (17, NULL, N'1380814353134.jpg', N'Animal', CAST(0x0000A24D00DC46A0 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (18, N'snails in the indoor trout stream', N'1380814114675.jpg', N'Insect', CAST(0x0000A24D00DC46A0 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (19, NULL, N'1381165589057.jpg', N'Plant', CAST(0x0000A25000BAC480 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (20, NULL, N'1381178149410.jpg', N'other', CAST(0x0000A25000F77790 AS DateTime), 4, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (22, N'When I enter a design idea I have to select the default text manually. That shouldnt really happen.', NULL, NULL, CAST(0x0000A25101137B70 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (23, N'(The same happens to the post-submission text)', NULL, NULL, CAST(0x0000A2510113C1C0 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (24, N'Why does the shift key on this keyboard go blue some times', NULL, NULL, CAST(0x0000A2510115ADF0 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (25, N'I cant type a question mark using this keyboard.', NULL, NULL, CAST(0x0000A2510115F440 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (26, N'curation fxn - ability to recategorize images or comments when necessary', NULL, NULL, CAST(0x0000A25200F77790 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (27, N'allow users to contribute images from their own smart phones', NULL, NULL, CAST(0x0000A25200F890D0 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (28, N'stack program with excellent examples of good user data so people have guidance', NULL, NULL, CAST(0x0000A25200FB9640 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (29, N'text wrapping fix so long names still show', NULL, NULL, CAST(0x0000A25200FCAF80 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (30, N'rubber band effect on right side of image scrolling', NULL, NULL, CAST(0x0000A25200FCF5D0 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (31, N'This is a test of the new bluetooth keyboard.', NULL, NULL, CAST(0x0000A25300F31290 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (32, N'only the photos taken by user tom cardamone inside the building loaded', NULL, NULL, CAST(0x0000A25301133520 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (33, N'i became more engaged in using the app when i was participating in a guided task - ie take a picture of an insect', NULL, NULL, CAST(0x0000A25301140810 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (34, N'tasks should be tied to season - ie take a picture of an insect is more difficult in winter', NULL, NULL, CAST(0x0000A253011494B0 AS DateTime), 0, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (35, N'', N'1386267333888.jpg', N'Landscape', CAST(0x0000A28B00DAD83D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (36, N'', N'1386889286268.jpg', N'', CAST(0x0000A292012928EC AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (37, N'', N'1386889297474.jpg', N'', CAST(0x0000A29201292E58 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (38, N'', N'1386889322478.jpg', N'', CAST(0x0000A292016A5646 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (39, N'', N'1386889338187.jpg', N'', CAST(0x0000A292016A61B9 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (40, N'', N'1386889362974.jpg', N'', CAST(0x0000A292016A6BF8 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (41, N'', N'1386889370534.jpg', N'', CAST(0x0000A292016A72A4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (42, N'', N'1386889378449.jpg', N'', CAST(0x0000A292016A7799 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (43, N'', N'1386889419472.jpg', N'', CAST(0x0000A292016A8AB6 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (44, N'', N'1386889425465.jpg', N'', CAST(0x0000A292016A9513 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (45, N'', N'1386889452203.jpg', N'', CAST(0x0000A292016AA723 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (46, N'', N'1386889474052.jpg', N'', CAST(0x0000A292016AB641 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (47, N'', N'1386889498880.jpg', N'', CAST(0x0000A292016AC266 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (48, N'', N'1386889590667.jpg', N'', CAST(0x0000A292016AD6B1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (49, N'', N'1386903553670.jpg', N'', CAST(0x0000A292016B11AA AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (50, N'', N'1386889305219.jpg', N'', CAST(0x0000A29201293446 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (51, N'', N'1386889409367.jpg', N'', CAST(0x0000A292016A82EF AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (52, N'', N'1386889332441.jpg', N'', CAST(0x0000A292016A5C77 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (53, N'', N'1386889357649.jpg', N'', CAST(0x0000A292016A67B4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (54, N'', N'1386889310162.jpg', N'', CAST(0x0000A292016A512D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (55, N'', N'1386889430754.jpg', N'', CAST(0x0000A292016A9AA8 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (56, N'', N'1386889525892.jpg', N'', CAST(0x0000A292016ACFF1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (57, N'', N'1386889492809.jpg', N'', CAST(0x0000A292016ABC1D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (58, N'', N'1386889462304.jpg', N'', CAST(0x0000A292016AAEDE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (59, N'', N'1386889510964.jpg', N'', CAST(0x0000A292016AC93E AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (60, N'', N'1386889602701.jpg', N'', CAST(0x0000A292016AE655 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (61, N'', N'1385488604256.3gp', N'', CAST(0x0000A29800A35D65 AS DateTime), 8, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (62, N'giant fish!!', N'1385488760470.3gp', N'', CAST(0x0000A29800A375A4 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (63, N'', N'1385413469861.3gp', N'', CAST(0x0000A28101099FCD AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (64, N'', N'1381525650666.jpg', N'', CAST(0x0000A254015B1E42 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (65, N'beautiful', N'1381772678536.jpg', N'Landscape', CAST(0x0000A25701637E00 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (66, N'', N'1381769277949.jpg', N'', CAST(0x0000A25800E8F24D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (67, N'', N'1381859759764.jpg', N'', CAST(0x0000A25900374AF5 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (68, N'', N'1384488668975.jpg', N'', CAST(0x0000A276017E53B9 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (69, N'', N'1384530887400.jpg', N'', CAST(0x0000A27700B411F7 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (70, N'', N'1384530823892.jpg', N'', CAST(0x0000A27700B3D1B2 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (71, N'ys', N'1384524314589.jpg', N'Landscape', CAST(0x0000A2770095F694 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (72, N'', N'1384530805701.jpg', N'', CAST(0x0000A27700B3A775 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (73, N'test', N'1384807721624.jpg', N'', CAST(0x0000A27A0104BC49 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (74, N'', N'1381615362396.jpg', N'', CAST(0x0000A27A0107F508 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (75, N'', N'1384912463627.jpg', N'', CAST(0x0000A27B0158AEA9 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (76, N'', N'1384970803542.jpg', N'', CAST(0x0000A27C00D84199 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (77, N'cat meowing', N'1384912563834.jpg', N'', CAST(0x0000A27B0159413F AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (78, N'', N'1384912658146.jpg', N'', CAST(0x0000A27B01598D82 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (79, N'', N'1385069586058.jpg', N'', CAST(0x0000A27D0110EC7B AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (80, N'', N'1385136886093.jpg', N'', CAST(0x0000A27E00B98862 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (81, N'bla bla bla. bla ', N'1385068902474.jpg', N'Anima', CAST(0x0000A27D010DE5A9 AS DateTime), 11, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (82, N'', N'1385068962332.jpg', N'', CAST(0x0000A27D010E0EAE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (83, N'', N'1385098073436.3gp', N'', CAST(0x0000A27E0007EBFF AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (84, N'', N'1385136944587.jpg', N'', CAST(0x0000A27E00B9B147 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (85, N'', N'1385136803156.jpg', N'', CAST(0x0000A27E00B91705 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (86, N'', N'1385138442276.jpg', N'', CAST(0x0000A27E00C0AA59 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (87, N'', N'1385138557518.jpg', N'', CAST(0x0000A27E00C119C4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (88, N'', N'1385406286610.jpg', N'', CAST(0x0000A28100F027B4 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (89, N'beautiful snowfall on fallen log', N'1385406402918.jpg', N'Landscape', CAST(0x0000A28100F08718 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (90, N'', N'1385411482296.3gp', N'', CAST(0x0000A28101004B07 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (91, N'', N'1385413518144.jpg', N'', CAST(0x0000A2810109ED4D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (92, N'Ducks swimming.', N'1385487977242.jpg', N'', CAST(0x0000A28200D37928 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (93, N'ducks in the first pond', N'1385487905426.jpg', N'', CAST(0x0000A28200D4139C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (94, N'chickeree tracks', N'1385488904044.jpg', N'', CAST(0x0000A28200D79B4A AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (95, N'upper pond \na bit of green in winter', N'1385488161815.jpg', N'', CAST(0x0000A28200E8CA10 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (96, N'', N'1385488647405.jpg', N'', CAST(0x0000A28200EB51F7 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (97, N'They stir up the muck in the pond.', N'1385488430765.jpg', N'', CAST(0x0000A28200ECC763 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (98, N'Scary cat.', N'1385488809410.jpg', N'', CAST(0x0000A28200ED62A8 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (99, N'', N'1385489640887.jpg', N'', CAST(0x0000A28200EE1AB0 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (100, N'snow!!', N'1385490076350.jpg', N'', CAST(0x0000A28200EE707D AS DateTime), 5, NULL)
GO
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (101, N'', N'1385490616009.jpg', N'', CAST(0x0000A28200F05B5C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (102, N'these are Ring necked ducks', N'1385489101026.jpg', N'', CAST(0x0000A28200F1C528 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (103, N'more ringnecked ducks', N'1385489233844.jpg', N'', CAST(0x0000A28200F22673 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (104, N'snow on ice', N'1385489422193.jpg', N'', CAST(0x0000A28200F2659B AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (105, N'', N'1385408017582.3gp', N'', CAST(0x0000A28100F0400A AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (106, N'', N'1385408232014.jpg', N'', CAST(0x0000A28100F0FD31 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (107, N'fish in water', N'1385406685126.jpg', N'', CAST(0x0000A28100F174AB AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (108, N'', N'1385409196654.3gp', N'', CAST(0x0000A28100F72380 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (109, N'how much wind blew this tree down? ', N'1385407114469.jpg', N'Plant', CAST(0x0000A28100FCD2F1 AS DateTime), 6, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (110, N'A chewed log.', N'1385489704657.jpg', N'', CAST(0x0000A2820102EAB4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (111, N'', N'1385490588745.jpg', N'', CAST(0x0000A282010684CB AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (112, N'', N'1385491340421.jpg', N'', CAST(0x0000A282010BDEA2 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (113, N'', N'1385489295283.jpg', N'', CAST(0x0000A2820138AC5B AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (114, N'Snow on ice.', N'1385489422204.jpg', N'', CAST(0x0000A282013CCC12 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (115, N'A kayak.', N'1385490134861.jpg', N'', CAST(0x0000A28201460139 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (116, N'Cat tail.', N'1385491606305.jpg', N'', CAST(0x0000A28201533071 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (117, N'A fish under ice', N'1385489324696.jpg', N'', CAST(0x0000A283001D169D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (118, N'Berry''s', N'1385489933892.jpg', N'', CAST(0x0000A283001D5769 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (119, N'Squirrel and coyote tracks.', N'1385490244559.jpg', N'', CAST(0x0000A28300990583 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (120, N'Ducks', N'1385488024378.jpg', N'', CAST(0x0000A28200EC6372 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (121, N'', N'1381615324098.jpg', N'', CAST(0x0000A255012AC66F AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (122, N'', N'1381615248884.jpg', N'', CAST(0x0000A25501299698 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (123, N'', N'1381615234630.jpg', N'', CAST(0x0000A25501294AC3 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (124, N'', N'1381615243324.jpg', N'', CAST(0x0000A255012971BF AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (125, N'', N'1381615221592.jpg', N'', CAST(0x0000A255012926DE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (126, N'', N'1381615257845.jpg', N'', CAST(0x0000A2550129BAF4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (127, N'', N'1381767728488.jpg', N'', CAST(0x0000A25700D37C24 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (128, N'', N'1381767779409.jpg', N'', CAST(0x0000A25700D3BF6D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (129, N'', N'1381767803366.jpg', N'', CAST(0x0000A25700D44CCB AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (130, N'', N'1381767911048.jpg', N'', CAST(0x0000A25700D4F22E AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (131, N'', N'1381768006139.jpg', N'', CAST(0x0000A25700D5CBB7 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (132, N'', N'1381768175910.jpg', N'', CAST(0x0000A25700D79BD7 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (133, N'', N'1381768111750.jpg', N'', CAST(0x0000A25700D71B0C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (134, N'', N'1381768409687.jpg', N'', CAST(0x0000A25700DAF715 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (135, N'', N'1381768693454.jpg', N'', CAST(0x0000A25700DC7D45 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (136, N'', N'1381768730993.jpg', N'', CAST(0x0000A25700DD238D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (137, N'', N'1381768765108.jpg', N'', CAST(0x0000A25700DD7EDE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (138, N'', N'1381768825421.jpg', N'', CAST(0x0000A25700DDB05D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (139, N'', N'1381770749765.jpg', N'', CAST(0x0000A25700DA6B61 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (140, N'', N'1381768845236.jpg', N'', CAST(0x0000A25700DEF067 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (141, N'', N'1381768863545.jpg', N'', CAST(0x0000A25700DF7E2E AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (142, N'', N'1381768220576.jpg', N'', CAST(0x0000A25700E9C002 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (143, N'', N'1381768054624.jpg', N'', CAST(0x0000A25700E97CC5 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (144, N'', N'1381769169120.jpg', N'', CAST(0x0000A25700EB1D89 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (145, N'', N'1381769152590.jpg', N'', CAST(0x0000A25700EADC91 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (146, N'', N'1381769180444.jpg', N'', CAST(0x0000A25700EC7AB6 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (147, N'testing', N'1381767630138.jpg', N'', CAST(0x0000A25700CBC189 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (148, N'', N'1381767873870.jpg', N'', CAST(0x0000A25700D4AFB0 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (149, N'', N'1381767789576.jpg', N'', CAST(0x0000A25700D409F3 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (150, N'', N'1381767857048.jpg', N'', CAST(0x0000A25700D470EA AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (151, N'', N'1381767947275.jpg', N'', CAST(0x0000A25700D59C6C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (152, N'', N'1381768127599.jpg', N'', CAST(0x0000A25700D76C26 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (153, N'', N'1381768081929.jpg', N'', CAST(0x0000A25700D6E9CE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (154, N'', N'1381768667846.jpg', N'', CAST(0x0000A25700DC5A7D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (155, N'', N'1381772117169.jpg', N'', CAST(0x0000A25800DEC4BA AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (156, N'', N'1381768317572.jpg', N'', CAST(0x0000A25700DA756C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (157, N'', N'1381768565730.jpg', N'', CAST(0x0000A25700DBB49D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (158, N'', N'1381768439107.jpg', N'', CAST(0x0000A25700DB3C05 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (159, N'green!', N'1381772347214.jpg', N'Plant', CAST(0x0000A25800DF1452 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (160, N'', N'1381768339463.jpg', N'', CAST(0x0000A25700DAA97C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (161, N'let''s go fishing ( upside down )', N'1381772756609.jpg', N'Animal', CAST(0x0000A25800DFBF6D AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (162, N'', N'1381768291357.jpg', N'', CAST(0x0000A25700DA278D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (163, N'', N'1381768548671.jpg', N'', CAST(0x0000A25700DB796E AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (164, N'', N'1381768619306.jpg', N'', CAST(0x0000A25700DC021B AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (165, N'', N'1381768837085.jpg', N'', CAST(0x0000A25700DE82CF AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (166, N'', N'1381768883792.jpg', N'', CAST(0x0000A25700E0375E AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (167, N'', N'1381768917754.jpg', N'', CAST(0x0000A25700E0F5EC AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (168, N'', N'1381769021930.jpg', N'', CAST(0x0000A25700E1BCCE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (169, N'', N'1381769219547.jpg', N'', CAST(0x0000A25800E75F62 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (170, N'', N'1381769203857.jpg', N'', CAST(0x0000A25800E87560 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (171, N'', N'1381769250811.jpg', N'', CAST(0x0000A25800E8D1EC AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (172, N'', N'1381769108368.jpg', N'', CAST(0x0000A25700EA0086 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (173, N'', N'1381769129970.jpg', N'', CAST(0x0000A25700EA4C04 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (174, N'', N'1381769142411.jpg', N'', CAST(0x0000A25700EA90AD AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (175, N'', N'1381772923478.jpg', N'Insect', CAST(0x0000A25701709DB0 AS DateTime), 5, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (176, N'', N'1381769442377.jpg', N'', CAST(0x0000A25800DEA506 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (177, N'', N'1381769397392.jpg', N'', CAST(0x0000A25800DE7CE6 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (178, N'', N'1381769241461.jpg', N'', CAST(0x0000A25800E7D355 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (179, N'', N'1381769350716.jpg', N'', CAST(0x0000A2580148352A AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (180, N'', N'1381769325110.jpg', N'', CAST(0x0000A259002B304C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (181, N'don''t step on the slug', N'1381774013169.jpg', N'Animal', CAST(0x0000A25A00EB9FAC AS DateTime), 8, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (182, N'', N'1381858701747.jpg', N'', CAST(0x0000A25A00A2B5C1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (183, N'having lots of fun', N'1381773571322.jpg', N'', CAST(0x0000A25A00EB2F14 AS DateTime), 8, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (184, N'', N'1381859734269.jpg', N'', CAST(0x0000A25A00EC1DF5 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (185, N'', N'1381860125167.jpg', N'', CAST(0x0000A25A00ECF5A3 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (186, N'', N'1382377879591.jpg', N'', CAST(0x0000A25E00E7B2AC AS DateTime), 6, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (187, N'1 st duck sighting today', N'1381166969938.jpg', N'Animal', CAST(0x0000A25000E765E6 AS DateTime), 4, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (188, N'', N'1381167577619.jpg', N'Landscape', CAST(0x0000A25000E82749 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (189, N'', N'1381167993721.jpg', N'Landscape', CAST(0x0000A25000E87C8C AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (190, N'', N'1381166643772.jpg', N'Landscape', CAST(0x0000A25000E8CA71 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (191, N'', N'1381182907881.jpg', N'', CAST(0x0000A2500127C7A8 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (192, N'', N'1381167962902.jpg', N'Landscape', CAST(0x0000A25000E8549D AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (193, N'', N'1381167401728.jpg', N'Animal', CAST(0x0000A25000E7CF55 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (194, N'wildlife on two leg', N'1381167244204.jpg', N'Animal', CAST(0x0000A25000E7B444 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (195, N'', N'1381167046279.jpg', N'', CAST(0x0000A25000E78E3F AS DateTime), 4, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (196, N'', N'1381184193336.jpg', N'', CAST(0x0000A25100CAEE79 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (197, N'This photo was a test!', N'1381353259669.jpg', N'Plan', CAST(0x0000A252011CEE2C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (198, N'goldfish', N'1381425733005.jpg', N'Animal', CAST(0x0000A25300E6240C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (199, N'', N'1381426067019.jpg', N'', CAST(0x0000A25300E6A3DB AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (200, N'', N'1381426171461.jpg', N'Landscape', CAST(0x0000A25300E70FCC AS DateTime), 4, NULL)
GO
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (201, N'', N'1381426573616.jpg', N'', CAST(0x0000A25300E72759 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (202, N'', N'1381427258686.jpg', N'', CAST(0x0000A25300E7C221 AS DateTime), 11, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (203, N'weir', N'1381426529315.jpg', N'Landscape', CAST(0x0000A25300E92638 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (204, N'hear river and tangled carboniferous plant', N'1381427057529.jpg', N'Plant', CAST(0x0000A25300EA913F AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (205, N'yellow trees', N'1381425949113.jpg', N'Landscape', CAST(0x0000A25300F6DD42 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (206, N'', N'1381427525341.jpg', N'', CAST(0x0000A25300FE2202 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (207, N'', N'1381441843316.jpg', N'Landscape', CAST(0x0000A253012681F3 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (208, N'These leaves are still green', N'1381442485495.jpg', N'Plant', CAST(0x0000A253012B1539 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (209, N'Ripe berrie', N'1381442626641.jpg', N'Plant', CAST(0x0000A253012B2C39 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (210, N'', N'1381443054553.jpg', N'Plant', CAST(0x0000A253012D8069 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (211, N'mallard at 3x', N'1381442137365.jpg', N'', CAST(0x0000A253012E32FE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (212, N'', N'1381442235159.jpg', N'', CAST(0x0000A253012E58F0 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (213, N'yellow warbler nest', N'1381442524918.jpg', N'', CAST(0x0000A253012E7FBD AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (214, N'fall leaves in the rain', N'1381442004619.jpg', N'', CAST(0x0000A253017EA3BE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (215, N'QA', N'1381425897127.jpg', N'Animal', CAST(0x0000A25300E662BA AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (216, N'', N'1381426313643.jpg', N'', CAST(0x0000A25300E6EA94 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (217, N'this conifer looks old', N'1381426586046.jpg', N'', CAST(0x0000A25300E76FC1 AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (218, N'', N'1381427592061.jpg', N'', CAST(0x0000A2530117E38A AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (219, N'This is an ex-bear.', N'1381441757815.jpg', N'Animal', CAST(0x0000A2530125EDD6 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (220, N'', N'1381442033602.jpg', N'Plant', CAST(0x0000A2530128222D AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (221, N'', N'1381442002899.jpg', N'', CAST(0x0000A25301273889 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (222, N'Unknown seedpods.', N'1381442141108.jpg', N'', CAST(0x0000A253012842E7 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (223, N'', N'1381442203963.jpg', N'Plant', CAST(0x0000A25301286B41 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (224, N'The leaves are all gone!', N'1381442281795.jpg', N'Plant', CAST(0x0000A253012B8DD4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (225, N'This Oncoryhncus still has Parr markings! Idn''t that neat!', N'1381443139350.jpg', N'', CAST(0x0000A253012C9226 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (226, N'a pleasant surprise. I looked up to see a lone duck quietly feeling.', N'1381442411841.jpg', N'Animal', CAST(0x0000A253012D5C74 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (227, N'', N'1381442986485.jpg', N'', CAST(0x0000A253012EA9E1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (228, N'rose hip edible', N'1381443012335.jpg', N'', CAST(0x0000A253012EBB1A AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (229, N'teal at 3.9\n', N'1381442254503.jpg', N'Anima', CAST(0x0000A253012E63B3 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (230, N'Well I was on the phone tree andrew perspectives why did fall garage taking place on the shoreline near were the treehouse been happy for apparently many years and actually what did fall you suck', N'1381442148758.jpg', N'Plant', CAST(0x0000A253012DD026 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (231, N'', N'1381443196061.jpg', N'', CAST(0x0000A253012EF5B1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (232, N'', N'1381444161079.jpg', N'', CAST(0x0000A25301312536 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (233, N'fall has come a little later to aspen this year. very unsettled weather with lots of moisture flowing in to the state. is some of this caused by the typhoon in Asia a couple weeks ago or just a different ocean\/climate pattern?', N'1381442095554.jpg', N'', CAST(0x0000A253013600C1 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (234, N'', N'1381514388426.jpg', N'', CAST(0x0000A25400EBA4CF AS DateTime), 9, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (235, N'', N'1381514063980.jpg', N'', CAST(0x0000A25400EAD81D AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (236, N'', N'1381513201924.jpg', N'', CAST(0x0000A25400EA0EF7 AS DateTime), 6, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (237, N'', N'1381512527963.jpg', N'', CAST(0x0000A2540104D153 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (238, N'', N'1381514939702.jpg', N'', CAST(0x0000A2540105B010 AS DateTime), 10, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (239, N'', N'1381512463374.jpg', N'', CAST(0x0000A25401078BDA AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (240, N'Loud duck', N'1381524476967.jpg', N'Animal', CAST(0x0000A254013BBB6B AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (241, N'', N'1381524348426.jpg', N'Plant', CAST(0x0000A25401555658 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (242, N'', N'1381594521289.jpg', N'', CAST(0x0000A25500CB8BA9 AS DateTime), 2, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (243, N'', N'1385407006614.jpg', N'', CAST(0x0000A2810115CC58 AS DateTime), 4, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (244, N'mallard in breeding plumage', N'1385418451392.jpg', N'Animal', CAST(0x0000A2810120A37C AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (245, N'A stick stuck in ice.', N'1385489533966.jpg', N'', CAST(0x0000A282014083F9 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (246, N'Beaver tracks.', N'1385490488182.jpg', N'', CAST(0x0000A282014B8DB2 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (247, N'She is a red tailed hawk.', N'1385493357125.jpg', N'', CAST(0x0000A28201557E25 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (248, N'Divers.', N'1385488868695.jpg', N'', CAST(0x0000A2820159DABE AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (249, N'', N'1385487260847.jpg', N'', CAST(0x0000A28200CF91D4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (250, N'', N'1385487789266.jpg', N'', CAST(0x0000A28200D21457 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (251, N'beaver winter cache', N'1385487983676.jpg', N'', CAST(0x0000A28200D70208 AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (252, N'brown and brook trout continuing to spawn at hallam lake', N'1385487920234.jpg', N'', CAST(0x0000A28200D6B5A5 AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (253, N'tracks all over on the back porch', N'1385488960997.jpg', N'', CAST(0x0000A28200D7D962 AS DateTime), 7, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (254, N'', N'1385488557391.jpg', N'', CAST(0x0000A28200E9F4FA AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (255, N'', N'1385488633299.jpg', N'', CAST(0x0000A28200EAB844 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (256, N'', N'1385488171689.jpg', N'', CAST(0x0000A28200ECA1A0 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (257, N'tracks', N'1385490231205.jpg', N'', CAST(0x0000A28200EDAFB6 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (258, N'', N'1385490560589.jpg', N'', CAST(0x0000A28200EEF0A4 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (259, N'big fishies!\n', N'1385488452544.jpg', N'', CAST(0x0000A28200FF6F48 AS DateTime), 3, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (260, N'bobcat\/lynx rufus', N'1385493963877.jpg', N'', CAST(0x0000A2820107818B AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (261, N'', N'1390578848205.jpg', N'', CAST(0x0000A2BD00B3F200 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (262, N'', N'1390584852272.jpg', N'', CAST(0x0000A2BD00CF6D68 AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (263, N'', N'1390585757834.jpg', N'', CAST(0x0000A2BD00D3A0F6 AS DateTime), 5, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (264, N'', N'1390586013674.jpg', N'', CAST(0x0000A2BD00D4A6DA AS DateTime), 1, NULL)
INSERT [dbo].[Contribution] ([id], [note], [media_url], [tags], [date], [location_id], [technical_info]) VALUES (265, N'', N'1390586355713.jpg', N'', CAST(0x0000A2BD00D63662 AS DateTime), 9, NULL)
SET IDENTITY_INSERT [dbo].[Contribution] OFF
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (0, N'Default Parent', CAST(0x0000A25E00000000 AS DateTime), 0, 0, 0, N'Null', NULL, 0, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (1, N'ok', CAST(0x0000A2EB00C4AE19 AS DateTime), 1, 28, 0, N'nature_net.Contribution', NULL, 33, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (3, N'fine', CAST(0x0000A2EC00000000 AS DateTime), 1, 28, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (4, N'good; testing more and more longer comment', CAST(0x0000A2ED00000000 AS DateTime), 1, 28, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (5, N'nested comment 1', CAST(0x0000A2EE00000000 AS DateTime), 1, 28, 4, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (6, N'nested comment 2', CAST(0x0000A2F100000000 AS DateTime), 1, 28, 1, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (7, N'nested comment 3', CAST(0x0000A2F100000000 AS DateTime), 1, 28, 4, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (8, N'nested comment 4', CAST(0x0000A2F200000000 AS DateTime), 1, 28, 7, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (9, N'nested comment 5', CAST(0x0000A2F300000000 AS DateTime), 1, 28, 7, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (10, N'Thank you for your idea!', CAST(0x0000A2ED0189E99E AS DateTime), 1, 51, 0, N'nature_net.Contribution', NULL, 22, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (11, N'true', CAST(0x0000A2EE000CD9EA AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 22, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (12, N'true', CAST(0x0000A2EE000D4A73 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (13, N'true', CAST(0x0000A2EE000D523D AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (14, N'false', CAST(0x0000A2EE000D739B AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 32, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (15, N'false', CAST(0x0000A2EE000D7546 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 32, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (16, N'false', CAST(0x0000A2EE000D79D1 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 30, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (17, N'true', CAST(0x0000A2EE000D7B93 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 30, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (18, N'false', CAST(0x0000A2EE000D7F8D AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (19, N'true', CAST(0x0000A2EE000D9604 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 32, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (20, N'false', CAST(0x0000A2EE000E3BDF AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 26, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (21, N'true', CAST(0x0000A2EE008E1338 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 22, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (22, N'false', CAST(0x0000A2EE008E16FF AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 22, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (23, N'true', CAST(0x0000A2EE008E2525 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 22, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (24, N'comment on my comment!', CAST(0x0000A2EE008E5AAD AS DateTime), 1, 51, 10, N'nature_net.Contribution', NULL, 22, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (25, N'after deselected the comment...', CAST(0x0000A2EE008E8A44 AS DateTime), 1, 65, 0, N'nature_net.Contribution', NULL, 22, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (26, N'true', CAST(0x0000A2EE008ED70E AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 22, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (27, N'true', CAST(0x0000A2F000C06F20 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 23, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (28, N'true', CAST(0x0000A2F0011785BB AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 26, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (29, N'true', CAST(0x0000A2F001202384 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 26, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (30, N'true', CAST(0x0000A2F00122186E AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 23, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (31, N'true', CAST(0x0000A2F00122BDE1 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 23, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (32, N'true', CAST(0x0000A2F00122C156 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 23, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (33, N'true', CAST(0x0000A2F001250E69 AS DateTime), 2, 0, 0, N'nature_net.Contribution', NULL, 33, NULL)
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (34, N'fixed!', CAST(0x0000A2F001442C61 AS DateTime), 1, 51, 0, N'nature_net.Contribution', NULL, 30, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (35, N'It is possible now. Is it?', CAST(0x0000A2F00146811B AS DateTime), 1, 51, 0, N'nature_net.Contribution', NULL, 25, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (36, N'nice collection', CAST(0x0000A2F200B83478 AS DateTime), 1, 4, 0, N'nature_net.User', NULL, 2, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (37, N'yes indeed', CAST(0x0000A2F200B8B433 AS DateTime), 1, 51, 36, N'nature_net.User', NULL, 2, N'')
INSERT [dbo].[Feedback] ([id], [note], [date], [type_id], [user_id], [parent_id], [object_type], [object], [object_id], [technical_info]) VALUES (38, N'great activity', CAST(0x0000A2F200B8CE74 AS DateTime), 1, 51, 0, N'nature_net.Activity', NULL, 4, N'')
SET IDENTITY_INSERT [dbo].[Feedback] OFF
SET IDENTITY_INSERT [dbo].[Feedback_Type] ON 

INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (0, N'Default', NULL, N'Null')
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (1, N'Comment', NULL, N'String')
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (2, N'Like', NULL, N'Boolean')
INSERT [dbo].[Feedback_Type] ([id], [name], [description], [data_type]) VALUES (3, N'Rate', NULL, N'Integer')
SET IDENTITY_INSERT [dbo].[Feedback_Type] OFF
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([id], [name], [description]) VALUES (0, N'Default', N'Default Location')
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (1, N'Hub of Activities', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (2, N'Golden Eagle', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (3, N'A Safe Haven', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (4, N'Beavers', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (5, N'Outdoor Classroom', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (6, N'Past to Present', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (7, N'Overlook', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (8, N'Bird Hollow', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (9, N'Where Rivers Come Together', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (10, N'Birds of Prey', NULL)
INSERT [dbo].[Location] ([id], [name], [description]) VALUES (11, N'Journeys End', NULL)
SET IDENTITY_INSERT [dbo].[Location] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (0, N'Default User', NULL, NULL, NULL, NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (1, N'jimmypk', N'jkravitz@aspennature.org', NULL, N'nn_bearorange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (2, N'adam mccurdy', NULL, NULL, N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (3, N'Sam Cardick', N'samcardick@gmail.com', NULL, N'nn_bisongreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (4, N'denali', NULL, NULL, N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (5, N'bob romeo', NULL, NULL, N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (6, N'elin binck', NULL, NULL, N'NN_GatorRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (7, N'lily', N'lilybq@gmail.com', NULL, N'nn_caribougreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (8, N'James', NULL, NULL, N'nn_squirrelpurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (9, N'Bowman Leigh', NULL, NULL, N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (10, N'Markus', NULL, NULL, N'NN_TortoisePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (11, N'Kaz', NULL, NULL, N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (12, N'Olivia Siegel', NULL, NULL, N'nn_squirrelpurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (13, N'bunny', NULL, NULL, N'NN_HarePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (14, N'Abby', NULL, NULL, N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (15, N'jenny', NULL, NULL, N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (16, N'Jamie', NULL, NULL, N'NN_BisonOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (17, N'karen', NULL, NULL, N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (18, N'Tom Cardamone', NULL, NULL, N'NN_HarePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (24, N'court', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (25, N'udelle sticley', N'', N'                                                                                                                                ', N'NN_CaribouPurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (26, N'udelle stuckey', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (27, N'me', N'', N'                                                                                                                                ', N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (28, N'buf', N'', N'                                                                                                                                ', N'NN_BisonOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (29, N'Jake Moe', N'', N'                                                                                                                                ', N'nn_cariboupurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (30, N'Kazjon', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (31, N'Kazjon2', N'', N'                                                                                                                                ', N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (32, N'Micah Davis', N'', N'                                                                                                                                ', N'NN_GatorRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (33, N'bob p', N'', N'                                                                                                                                ', N'NN_GatorRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (34, N'amy', N'', N'                                                                                                                                ', N'NN_BearGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (35, N'amy', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (36, N'fritest', N'', N'                                                                                                                                ', N'NN_BisonOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (37, N'Stephie', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (38, N'Jill', N'', N'                                                                                                                                ', N'NN_BisonOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (39, N'eric', N'', N'                                                                                                                                ', N'NN_TortoisePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (40, N'spiffy', N'', N'                                                                                                                                ', N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (41, N'cullinan', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (42, N'Kaz3', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (43, N'gwynne', N'', N'                                                                                                                                ', N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (44, N'pc33ujnnhb', N'', N'                                                                                                                                ', N'nn_bearred.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (45, N'Enter UserName', N'', N'                                                                                                                                ', N'nn_bearorange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (46, N'phil', N'', N'                                                                                                                                ', N'NN_TortoisePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (47, N'Abby S', N'', N'                                                                                                                                ', N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (48, N'purple bunny', N'', N'                                                                                                                                ', N'NN_HarePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (49, N'moose', N'', N'                                                                                                                                ', N'NN_CaribouPurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (50, N'grsnake', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (51, N'MJ', N'', N'                                                                                                                                ', N'nn_horseorange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (52, N'carrie', N'', N'                                                                                                                                ', N'nn_bisongreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (53, N'm', N'', N'                                                                                                                                ', N'nn_bisongreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (54, N'ottile', N'', N'                                                                                                                                ', N'nn_weaselorange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (55, N'chris lane', N'', N'                                                                                                                                ', N'NN_SquirrelOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (56, N'abhijit', N'', N'                                                                                                                                ', N'NN_HarePurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (57, N'anders', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (58, N'Tom', N'', N'                                                                                                                                ', N'NN_CaribouPurple.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (59, N'clb', N'', N'                                                                                                                                ', N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (60, N'testing new ', N'', N'                                                                                                                                ', N'NN_GatorRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (61, N'turkeydinner', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (62, N'greenbear', N'', N'                                                                                                                                ', N'NN_BearGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (63, N'Corrie', N'', N'                                                                                                                                ', N'NN_BearGreen.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (64, N'elsie', N'', N'                                                                                                                                ', N'NN_HorseOrange.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (65, N'Nick Carter', N'', N'                                                                                                                                ', N'NN_FrogRed.png', NULL)
INSERT [dbo].[User] ([id], [name], [email], [password], [avatar], [technical_info]) VALUES (66, N'rebecca weiss', N'', N'                                                                                                                                ', N'NN_SnakeGreen.png', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_Action_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Action_Type] ([id])
GO
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_Action_Type]
GO
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_User]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Location] FOREIGN KEY([location_id])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Location]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_Activity]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_Collection_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_Collection_User]
GO
ALTER TABLE [dbo].[Collection_Contribution_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_CCMapping_Collection] FOREIGN KEY([collection_id])
REFERENCES [dbo].[Collection] ([id])
GO
ALTER TABLE [dbo].[Collection_Contribution_Mapping] CHECK CONSTRAINT [FK_CCMapping_Collection]
GO
ALTER TABLE [dbo].[Collection_Contribution_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_CCMapping_Contribution] FOREIGN KEY([contribution_id])
REFERENCES [dbo].[Contribution] ([id])
GO
ALTER TABLE [dbo].[Collection_Contribution_Mapping] CHECK CONSTRAINT [FK_CCMapping_Contribution]
GO
ALTER TABLE [dbo].[Contribution]  WITH CHECK ADD  CONSTRAINT [FK_Contribution_Location] FOREIGN KEY([location_id])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Contribution] CHECK CONSTRAINT [FK_Contribution_Location]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Feedback] FOREIGN KEY([parent_id])
REFERENCES [dbo].[Feedback] ([id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Feedback]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Feedback_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Feedback_Type] ([id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Feedback_Type]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_User]
GO
