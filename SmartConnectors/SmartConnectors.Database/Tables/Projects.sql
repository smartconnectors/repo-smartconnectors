CREATE TABLE [dbo].[Projects]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NOT NULL,
	[IsActive] BIT NULL,
	[CreatedDate] DATETIME NULL Default getDate(),
	[ModifiedDate] DATETIME NULL Default getDate()
)
