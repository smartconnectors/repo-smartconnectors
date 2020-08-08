CREATE TABLE [dbo].[OperationLogs]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Message]  VARCHAR(500) NULL,
	[Content] VARCHAR(MAX) NULL,
	[Status] VARCHAR(50) NULL,
	[CreatedDate] DATETIME NULL,
	[CreatedBy] VARCHAR(50) NULL,
	[WorkflowId] INT NOT NULL,

	CONSTRAINT [FK_OperationLogs_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id]),
)