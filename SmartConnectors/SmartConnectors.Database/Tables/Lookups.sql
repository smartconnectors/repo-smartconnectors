CREATE TABLE [dbo].[Lookups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Type] VARCHAR(50) NULL,
	[Value] VARCHAR(50) NULL,
	[Description] VARCHAR(50) NULL,
	[IsActive] BIT NOT NULL Default 1,
	[CreatedDate] DateTime NULL Default getDate(),
	[ModifiedDate] DateTime NULL Default getDate()
)
