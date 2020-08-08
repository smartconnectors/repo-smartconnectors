CREATE TABLE [dbo].[Credentials]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserName] VARCHAR(255) NULL,
	[Password] VARCHAR(255) NULL,
	[SecretToken] VARCHAR(MAX) NULL,
	[HostUrl] VARCHAR(255) NULL,
	[RememberMe] BIT NULL,
	[ConnectorId] INT NULL,
	[WorkflowId] INT NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL,
	[ModifiedDate] DateTime NULL

	CONSTRAINT [FK_Connectors_Credentials] FOREIGN KEY ([ConnectorId]) REFERENCES [dbo].[Connectors]([Id]),
	[OrgId] VARCHAR(50) NULL, 
    [EndPointName] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Workflows_Credentials] FOREIGN KEY ([WorkflowId]) REFERENCES [dbo].[Workflows]([Id])
)
