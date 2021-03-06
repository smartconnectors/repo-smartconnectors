USE [master]
GO
/****** Object:  Database [SmartConnectors.Database]    Script Date: 5/28/2020 12:52:11 AM ******/
CREATE DATABASE [SmartConnectors.Database]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartConnectors.Database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartConnectors.Database_Primary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartConnectors.Database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SmartConnectors.Database_Primary.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SmartConnectors.Database] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartConnectors.Database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartConnectors.Database] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET ANSI_NULLS ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET ANSI_PADDING ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET ARITHABORT ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [SmartConnectors.Database] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [SmartConnectors.Database] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SmartConnectors.Database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartConnectors.Database] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartConnectors.Database] SET  MULTI_USER 
GO
ALTER DATABASE [SmartConnectors.Database] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [SmartConnectors.Database] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartConnectors.Database] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartConnectors.Database] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SmartConnectors.Database] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartConnectors.Database', N'ON'
GO
ALTER DATABASE [SmartConnectors.Database] SET QUERY_STORE = ON
GO
ALTER DATABASE [SmartConnectors.Database] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [SmartConnectors.Database]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SmartConnectors.Database]
GO
USE [SmartConnectors.Database]
GO
/****** Object:  Sequence [dbo].[MySeq]    Script Date: 5/28/2020 12:52:11 AM ******/
CREATE SEQUENCE [dbo].[MySeq] 
 AS [int]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
/****** Object:  Table [dbo].[Connectors]    Script Date: 5/28/2020 12:52:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[CompanyLogo] [varchar](255) NULL,
	[CompanyWebsite] [varchar](255) NULL,
	[IsPrimary] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credentials]    Script Date: 5/28/2020 12:52:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credentials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[SecretToken] [varchar](max) NULL,
	[HostUrl] [varchar](255) NULL,
	[RememberMe] [bit] NULL,
	[ConnectorId] [int] NULL,
	[WorkflowId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[OrgId] [varchar](50) NULL,
	[EndPointName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 5/28/2020 12:52:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[Type] [varchar](50) NULL,
	[FileSize] [varchar](50) NULL,
	[Content] [varbinary](max) NULL,
	[Description] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsProcessed] [bit] NULL,
	[FullPath] [varchar](500) NULL,
	[WebUrl] [varchar](max) NULL,
	[WorkflowId] [int] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookups]    Script Date: 5/28/2020 12:52:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OperationTypeId] [int] NULL,
	[Content] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[WorkflowConnectorId] [int] NULL,
	[StepCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationTypes]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedulers]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedulers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Data] [varchar](max) NULL,
	[WorkflowId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsRepeated] [bit] NULL,
	[StartTime] [varchar](50) NULL,
	[EndTime] [varchar](50) NULL,
	[RepeatOptionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transformations]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowId] [int] NOT NULL,
	[Input] [varchar](max) NULL,
	[Output] [varchar](max) NULL,
	[Payload] [varchar](max) NULL,
	[Script] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkflowConnectors]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkflowConnectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConnectorId] [int] NULL,
	[WorkflowId] [int] NULL,
	[Pos] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflows]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workflows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[TabIndex] [int] NULL,
	[IsDefault] [bit] NULL,
	[PackageName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Connectors] ADD  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[Connectors] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Credentials] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Lookups] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Lookups] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Lookups] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Operations] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Operations] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Operations] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[OperationTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[OperationTypes] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[OperationTypes] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Schedulers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Schedulers] ADD  DEFAULT ((0)) FOR [IsRepeated]
GO
ALTER TABLE [dbo].[Transformations] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Workflows] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Credentials]  WITH CHECK ADD  CONSTRAINT [FK_Connectors_Credentials] FOREIGN KEY([ConnectorId])
REFERENCES [dbo].[Connectors] ([Id])
GO
ALTER TABLE [dbo].[Credentials] CHECK CONSTRAINT [FK_Connectors_Credentials]
GO
ALTER TABLE [dbo].[Credentials]  WITH CHECK ADD  CONSTRAINT [FK_Workflows_Credentials] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[Credentials] CHECK CONSTRAINT [FK_Workflows_Credentials]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_WorkflowId] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_WorkflowId]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_OperationTypeId] FOREIGN KEY([OperationTypeId])
REFERENCES [dbo].[OperationTypes] ([Id])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_OperationTypeId]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_WorkflowConnectorId] FOREIGN KEY([WorkflowConnectorId])
REFERENCES [dbo].[WorkflowConnectors] ([Id])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_WorkflowConnectorId]
GO
ALTER TABLE [dbo].[Schedulers]  WITH CHECK ADD  CONSTRAINT [FK_Schedulers_Lookups] FOREIGN KEY([RepeatOptionId])
REFERENCES [dbo].[Lookups] ([Id])
GO
ALTER TABLE [dbo].[Schedulers] CHECK CONSTRAINT [FK_Schedulers_Lookups]
GO
ALTER TABLE [dbo].[Schedulers]  WITH CHECK ADD  CONSTRAINT [FK_Workflows_Schedulers] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[Schedulers] CHECK CONSTRAINT [FK_Workflows_Schedulers]
GO
ALTER TABLE [dbo].[Transformations]  WITH CHECK ADD  CONSTRAINT [FK_Transformations_WorkflowId] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[Transformations] CHECK CONSTRAINT [FK_Transformations_WorkflowId]
GO
ALTER TABLE [dbo].[WorkflowConnectors]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowConnectors_ConnectorId] FOREIGN KEY([ConnectorId])
REFERENCES [dbo].[Connectors] ([Id])
GO
ALTER TABLE [dbo].[WorkflowConnectors] CHECK CONSTRAINT [FK_WorkFlowConnectors_ConnectorId]
GO
ALTER TABLE [dbo].[WorkflowConnectors]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlowConnectors_WorkflowId] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[WorkflowConnectors] CHECK CONSTRAINT [FK_WorkFlowConnectors_WorkflowId]
GO
ALTER TABLE [dbo].[Workflows]  WITH CHECK ADD  CONSTRAINT [FK_WorkFlows_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Workflows] CHECK CONSTRAINT [FK_WorkFlows_ProjectId]
GO
/****** Object:  StoredProcedure [dbo].[NextID]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[NextID] 
    @pLastUsed CHAR(7) 
AS

/* Usage
EXEC NextID '0J99999'

*/
BEGIN
    SET NOCOUNT ON;
  DECLARE @T1 TABLE (Col CHAR(1))
  DECLARE @T2 TABLE (Col CHAR(1))
  DECLARE @T3 TABLE (Col CHAR(1))
  DECLARE @T4 TABLE (Col CHAR(1))
  DECLARE @T5 TABLE (Col CHAR(1))
  DECLARE @T6 TABLE (Col CHAR(1))
  DECLARE @T7 TABLE (Col CHAR(1))
  INSERT @T1 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9' UNION ALL SELECT 'A' UNION ALL SELECT 'B' UNION ALL SELECT 'C'
                                UNION ALL SELECT 'D' UNION ALL SELECT 'E' UNION ALL SELECT 'F' UNION ALL SELECT 'G'
                                UNION ALL SELECT 'H' UNION ALL SELECT 'I' UNION ALL SELECT 'J' UNION ALL SELECT 'K'
                                UNION ALL SELECT 'L' UNION ALL SELECT 'M' UNION ALL SELECT 'N' UNION ALL SELECT 'O'
                                UNION ALL SELECT 'P' UNION ALL SELECT 'Q' UNION ALL SELECT 'R' UNION ALL SELECT 'S'
                                UNION ALL SELECT 'T' UNION ALL SELECT 'U' UNION ALL SELECT 'V' UNION ALL SELECT 'W'
                                UNION ALL SELECT 'X' UNION ALL SELECT 'Y' UNION ALL SELECT 'Z'
INSERT @T2 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
INSERT @T3 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
INSERT @T4 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
INSERT @T5 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
INSERT @T6 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
INSERT @T7 ( Col ) SELECT '0' UNION ALL SELECT '1' UNION ALL SELECT '2' UNION ALL SELECT '3' UNION ALL SELECT '4' 
                                UNION ALL SELECT '5' UNION ALL SELECT '6' UNION ALL SELECT '7' UNION ALL SELECT '8' 
                                UNION ALL SELECT '9'
  SELECT TOP 1
    t2.Col + t1.Col + t3.Col + t4.Col + t5.Col + t6.Col + t7.Col
  FROM
    @T1 t1
    CROSS JOIN @T2 AS t2
    CROSS JOIN @T3 AS t3
	CROSS JOIN @T4 AS t4
    CROSS JOIN @T5 AS t5
	CROSS JOIN @T6 AS t6
    CROSS JOIN @T7 AS t7
  WHERE
    t2.Col + t1.Col + t3.Col + t4.Col + t5.Col + t6.Col + t7.Col  > @pLastUsed
  ORDER BY
    t2.Col + t1.Col + t3.Col + t4.Col + t5.Col + t6.Col + t7.Col
END
GO
/****** Object:  StoredProcedure [dbo].[up_GetNextSequence]    Script Date: 5/28/2020 12:52:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[up_GetNextSequence]
(
    @seq nchar(7) out
)
AS
DECLARE @i int = NEXT VALUE FOR dbo.MySeq;

IF @i > 685323
BEGIN
    RAISERROR(N'Sequence is out of range.', 16, 0);
END

IF @i < 1000000
BEGIN
    SET @seq = N'A' + FORMAT(@i, 'D4');
END
ELSE
BEGIN
    DECLARE @j int;
    SET @j = @i - 999999;

    DECLARE @k int;
    SET @k = ((@j - 1) % 99999) + 1;

    DECLARE @l int;
    SET @l = (@j - 1) / 9999;

    DECLARE @m int;
   SET @m = (@j - 1) / 999;

    DECLARE @n int;
    SET @n = (@m - 1) / 99;
	
	DECLARE @o int;
    SET @o = (@n % 26) + 65;

    DECLARE @p int;
    SET @p = (@o / 26) + 65;

    SET @seq = NCHAR(@n) + NCHAR(@m) + FORMAT(@k, 'D3');
END;
GO
USE [master]
GO
ALTER DATABASE [SmartConnectors.Database] SET  READ_WRITE 
GO
