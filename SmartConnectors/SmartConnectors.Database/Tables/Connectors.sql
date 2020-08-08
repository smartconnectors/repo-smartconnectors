CREATE TABLE [dbo].[Connectors]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NULL,
	[CompanyLogo] VARCHAR(255) NULL,
	[CompanyWebsite] VARCHAR(255) NULL,
	[IsPrimary] BIT NULL Default 0,
	[IsActive] BIT NULL Default 1,
	[CreatedDate] DateTime NULL,
	[ModifiedDate] DateTime NULL
)
