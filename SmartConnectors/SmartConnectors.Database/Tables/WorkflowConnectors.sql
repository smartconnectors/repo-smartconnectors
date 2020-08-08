CREATE TABLE [dbo].[WorkflowConnectors]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[ConnectorId] INT NULL,
	[WorkflowId] INT  NULL,
	[Pos] INT NULL

	CONSTRAINT [FK_WorkFlowConnectors_ConnectorId] FOREIGN KEY ([ConnectorId]) REFERENCES [dbo].[Connectors]([Id])
	CONSTRAINT [FK_WorkFlowConnectors_WorkflowId] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id])
)
