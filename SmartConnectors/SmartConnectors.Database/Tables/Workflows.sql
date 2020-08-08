CREATE TABLE [dbo].[Workflows]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[ProjectId] INT NOT NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL,
	[ModifiedDate] DateTime NULL

	
	CONSTRAINT [FK_WorkFlows_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects]([Id]), 
    [TabIndex] INT NULL, 
    [IsDefault] BIT NULL, 
    [PackageName] VARCHAR(50) NULL
)
