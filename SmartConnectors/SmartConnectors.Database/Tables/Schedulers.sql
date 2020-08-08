CREATE TABLE [dbo].[Schedulers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(50) NULL,
	[Data] VARCHAR(MAX) NULL,
	[WorkflowId] INT NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL,
	[ModifiedDate] DateTime NULL,
	[StartDate] DateTime NULL,
	[EndDate] DateTime NULL,
	[IsRepeated] BIT NULL Default 0,
	[StartTime] VARCHAR(50) NULL,
	[EndTime] VARCHAR(50) NULL,
	[RepeatOptionId] INT NULL

	CONSTRAINT [FK_Workflows_Schedulers] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id]),
	CONSTRAINT [FK_Schedulers_Lookups] FOREIGN KEY ([RepeatOptionId]) REFERENCES [dbo].[Lookups]([Id])  
)
