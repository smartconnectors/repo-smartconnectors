CREATE TABLE [dbo].[OperationTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NULL,
	[Type] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL Default getDate(),
	[ModifiedDate] DateTime NULL Default getDate()
)
