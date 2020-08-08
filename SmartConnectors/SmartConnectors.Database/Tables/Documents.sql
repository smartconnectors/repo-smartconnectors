/****** Object:  Table [dbo].[Documents]    Script Date: 7/24/2019 11:01:16 PM ******/

CREATE TABLE [dbo].[Documents](
	[Id] [int] NOT NULL IDENTITY(1,1) ,
	[FileName] [VARCHAR](50) NOT NULL,
	[Type] [VARCHAR](50) NULL,
	[FileSize] [VARCHAR](50) NULL,
	[Content]   VARBINARY(MAX) NULL,
	[Description] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsProcessed] [bit] NULL,
	[FullPath] [VARCHAR](500) NULL,
	[WebUrl] [VARCHAR](max) NULL,
	[WorkflowId] INT NULL 

	CONSTRAINT [FK_Documents_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id])

PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY], 
    [ModifiedDate] DATETIME NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


