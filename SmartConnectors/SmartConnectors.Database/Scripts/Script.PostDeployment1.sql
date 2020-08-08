/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [SmartConnectors.Database]
GO

Delete from [dbo].[Projects];
Delete from [dbo].[Connectors];
Delete from [dbo].[OperationTypes];
Delete from [dbo].[Lookups]

INSERT INTO [dbo].[Projects]
           ([Name]
           ,[CreatedDate]
           ,[ModifiedDate])
     VALUES
           ('Project 1'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Projects]
([Name]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Project 2'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Connectors]
([Name]
,[CompanyLogo]
,[IsPrimary]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce'
           ,'salesforce.png'
           ,1
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[Connectors]
([Name],
[CompanyLogo]
,[IsPrimary]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Excel'
           ,'excel.png'
           , 1
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[Connectors]
([Name],
[CompanyLogo]
,[IsPrimary]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Http'
           ,'http.png'
           ,1
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[Connectors]
([Name],
[CompanyLogo]
,[IsPrimary]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('MySql'
           ,'mysql.png'
           ,1
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Connectors]
([Name],
[CompanyLogo]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Transformation'
           ,'transformation.png'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[Connectors]
([Name],
[CompanyLogo]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Script'
           ,'script.png'
           ,getdate()
           ,getdate())



INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Org'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Query'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Update'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Insert'
           ,'Salesforce'
           ,getdate()
           ,getdate())
           INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Upsert'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Parent Object Selection'
           ,'Salesforce'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Child Object Selection'
           ,'Salesforce'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Operation Type Selection'
           ,'Salesforce'
           ,getdate()
           ,getdate())
           
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Query Builder'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Execute Query'
           ,'Salesforce'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Salesforce Summary'
           ,'Salesforce'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Excel File Selection'
           ,'Excel'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Excel Upload'
           ,'Excel'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Excel Download'
           ,'Excel'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Transformation Input'
           ,'Transformation'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Transformation Output'
           ,'Transformation'
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[OperationTypes]
([Name],
[Type]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('Transformation Payload'
           ,'Transformation'
           ,getdate()
           ,getdate())

INSERT INTO [dbo].[OperationTypes]
           ([Name]
           ,[Type]
           ,[IsActive]
           ,[CreatedDate]
           ,[ModifiedDate])
     VALUES
           ('Salesforce Parent Query'
           ,'Salesforce'
           ,1
           ,GETDATE()
           ,GETDATE())

		   INSERT INTO [dbo].[OperationTypes]
           ([Name]
           ,[Type]
           ,[IsActive]
           ,[CreatedDate]
           ,[ModifiedDate])
     VALUES
           ('Salesforce Child Query'
           ,'Salesforce'
           ,1
           ,GETDATE()
           ,GETDATE())

INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Day'
           ,''
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Month'
           ,''
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Year'
           ,''
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Week'
           ,''
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Weekend'
           ,''
           ,getdate()
           ,getdate())
INSERT INTO [dbo].[Lookups]
(
[Type]
,[Value]
,[Description]
,[CreatedDate]
,[ModifiedDate])
     VALUES
           ('RepeatOption'
           ,'Every Weekday'
           ,''
           ,getdate()
           ,getdate())
GO