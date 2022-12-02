USE [Chat]
GO

/****** Object:  Table [dbo].[tblMessenger]    Script Date: 10/28/2020 8:35:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMessenger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [nvarchar](100) NOT NULL,
	[Receiver] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](200) NOT NULL,
	[Datum] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


