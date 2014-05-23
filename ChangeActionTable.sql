USE [nature-net]
GO
drop table Action
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

ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_Action_Type] FOREIGN KEY([type_id]) REFERENCES [dbo].[Action_Type] ([id])
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_Action_Type]
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_User] FOREIGN KEY([user_id]) REFERENCES [dbo].[User] ([id])
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_User]
