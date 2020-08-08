CREATE TABLE [dbo].[Operations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OperationTypeId] INT NULL,
	[Content] VARCHAR(MAX) NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL Default getDate(),
	[ModifiedDate] DateTime NULL Default getDate(),	
	[WorkflowConnectorId] INT NULL,

    [StepCount] INT NULL, 
    CONSTRAINT [FK_Operations_OperationTypeId] FOREIGN KEY ([OperationTypeId]) REFERENCES [dbo].[OperationTypes]([Id]),
	CONSTRAINT [FK_Operations_WorkflowConnectorId] FOREIGN KEY ([WorkflowConnectorId]) REFERENCES [dbo].[WorkflowConnectors]([Id])
)
