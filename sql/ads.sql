USE [Ads]
GO
/****** Object:  Table [dbo].[advertising]    Script Date: 16/04/2015 11:24:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertising](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[detail] [text] NOT NULL,
	[price] [numeric](7, 2) NULL,
	[customer_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[subtype_id] [int] NOT NULL,
 CONSTRAINT [PK_advertising] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[category]    Script Date: 16/04/2015 11:24:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[customer]    Script Date: 16/04/2015 11:24:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phone] [nvarchar](40) NOT NULL,
	[occupation] [nvarchar](80) NULL,
	[address] [nvarchar](100) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[resource]    Script Date: 16/04/2015 11:24:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resource](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[path] [nvarchar](100) NOT NULL,
	[type] [nvarchar](20) NOT NULL,
	[advertising_id] [int] NOT NULL,
 CONSTRAINT [PK_resource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[subtype]    Script Date: 16/04/2015 11:24:50 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subtype](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[category_id] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_subtype] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[advertising]  WITH CHECK ADD  CONSTRAINT [FK_advertising_customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[advertising] CHECK CONSTRAINT [FK_advertising_customer]
GO
ALTER TABLE [dbo].[resource]  WITH CHECK ADD  CONSTRAINT [FK_resource_advertising] FOREIGN KEY([advertising_id])
REFERENCES [dbo].[advertising] ([id])
GO
ALTER TABLE [dbo].[resource] CHECK CONSTRAINT [FK_resource_advertising]
GO
ALTER TABLE [dbo].[subtype]  WITH CHECK ADD  CONSTRAINT [FK_subtype_category] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[subtype] CHECK CONSTRAINT [FK_subtype_category]
GO
