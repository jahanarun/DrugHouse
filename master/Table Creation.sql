USE [DrugHouse]
GO

/****** Object:  Table [dbo].[Drug]    Script Date: 22-11-2014 18:19:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Drug](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Drug] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[MedicalPractitioner]    Script Date: 22-11-2014 18:19:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MedicalPractitioner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [char](1) NULL,
	[Age] [int] NULL,
	[Address] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[State] [int] NOT NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
 CONSTRAINT [PK_MedicalPractitioner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[Patient]    Script Date: 22-11-2014 18:19:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [int] NULL,
	[Age] [int] NULL,
	[Address] [nvarchar](100) NULL,
	[Location] [nvarchar](100) NULL,
	[State] [int] NOT NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[GuardianName] [nvarchar](50) NULL,
	[GuardianType] [nvarchar](3) NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Prescription]    Script Date: 22-11-2014 18:19:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DrugId] [int] NOT NULL,
	[PrescriptionType] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[VisitId] [int] NOT NULL,
 CONSTRAINT [PK_PatientPrescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[VisitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_PatientPrescription_Drug] FOREIGN KEY([DrugId])
REFERENCES [dbo].[Drug] ([Id])
GO

ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_PatientPrescription_Drug]
GO

ALTER TABLE [dbo].[Prescription]  WITH CHECK ADD  CONSTRAINT [FK_Prescription_Visit] FOREIGN KEY([VisitId])
REFERENCES [dbo].[Visit] ([Id])
GO

ALTER TABLE [dbo].[Prescription] CHECK CONSTRAINT [FK_Prescription_Visit]
GO



/****** Object:  Table [dbo].[Visit]    Script Date: 22-11-2014 18:19:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Visit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VisitDate] [date] NOT NULL,
	[DiagnosisId] [int] NULL,
	[AssignedMedicalId] [int] NULL,
	[ReferredMedicalId] [int] NULL,
	[Status] [int] NULL,
	[FeeId] [int] NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_MedicalPractitioner] FOREIGN KEY([AssignedMedicalId])
REFERENCES [dbo].[MedicalPractitioner] ([Id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_MedicalPractitioner]
GO

ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_MedicalPractitioner1] FOREIGN KEY([ReferredMedicalId])
REFERENCES [dbo].[MedicalPractitioner] ([Id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_MedicalPractitioner1]
GO

ALTER TABLE [dbo].[Visit]  WITH CHECK ADD  CONSTRAINT [FK_Visit_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[Visit] CHECK CONSTRAINT [FK_Visit_Patient]
GO



