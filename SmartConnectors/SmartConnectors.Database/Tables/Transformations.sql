CREATE TABLE [dbo].[Transformations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[WorkflowId] INT NOT NULL,
	[Input] VARCHAR(MAX) NULL,
	[Output] VARCHAR(MAX) NULL,
	[Payload] VARCHAR(MAX) NULL,
	[Script] VARCHAR(MAX) NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL,
	[ModifiedDate] DateTime NULL

	CONSTRAINT [FK_Transformations_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id])
)
