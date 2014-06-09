/****** Object:  Table [dbo].[Interaction_Log]    Script Date: 6/3/2014 11:40:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interaction_Log](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[type] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[touch_id] [int] NOT NULL,
	[touch_x] [float] NOT NULL,
	[touch_y] [float] NOT NULL,
	[details] [nvarchar](max) NULL,
 CONSTRAINT [PK_Interaction_Log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Interaction_Type]    Script Date: 6/3/2014 11:40:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interaction_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Interaction_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Interaction_Type] ON 

INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (1, N'Scroll primary listbox')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (2, N'Tab changed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (3, N'Tap to open')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (4, N'Drag to open')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (5, N'Clicked on signup button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (6, N'Clicked on submit activity idea button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (7, N'Clicked on submit design idea button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (8, N'Clicked on header left button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (9, N'Clicked on header middle button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (10, N'Clicked on header right button')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (11, N'Clicked on red spot')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (12, N'Clicked on like in design idea listbox')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (13, N'Clicked on like in the design idea window')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (14, N'User collection opened')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (15, N'Activity collection opened')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (16, N'Design idea opened')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (17, N'Contribution opened')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (18, N'Window moved')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (19, N'Window closed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (20, N'Window rotated')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (21, N'Contribution manipulated')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (22, N'Expander clicked to open')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (23, N'Expander clicked to close')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (24, N'Collection scrolled')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (25, N'Comments scrolled')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (26, N'Clicked on reply comment')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (27, N'Clicked on submit comment (before authentication)')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (28, N'Clicked on submit design idea in its window')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (29, N'Avatar dropped for authentication')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (30, N'Authentication failed to submit comment or idea')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (31, N'Authentication passed to submit comment or idea')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (32, N'Signup next1 clicked')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (33, N'Signup next2 failed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (34, N'Signup next2 passed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (35, N'Signup back1')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (36, N'Signup back2')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (37, N'Signup submit failed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (38, N'Signup submit passed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (39, N'Cancel reply button clicked')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (40, N'Cancel authentication button clicked')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (41, N'Submit comment or idea failed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (42, N'Submit comment or idea passed')
INSERT [dbo].[Interaction_Type] ([id], [type]) VALUES (43, N'Item dropped on workspace')
SET IDENTITY_INSERT [dbo].[Interaction_Type] OFF

ALTER TABLE [dbo].[Interaction_Log]  WITH CHECK ADD  CONSTRAINT [FK_Interaction_Log_Interaction_Type] FOREIGN KEY([type])
REFERENCES [dbo].[Interaction_Type] ([id])
GO
ALTER TABLE [dbo].[Interaction_Log] CHECK CONSTRAINT [FK_Interaction_Log_Interaction_Type]
GO
