USE [companyworkersdb]
GO

/****** Object:  Table [dbo].[worker]    Script Date: 2/10/2021 6:51:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[worker](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[surname] [nvarchar](100) NOT NULL,
	[middle_name] [nvarchar](100) NULL,
	[employment_date] [date] NULL,
	[job] [nchar](50) NULL,
	[company_id] [int] NOT NULL,
 CONSTRAINT [PK_worker] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[worker]  WITH CHECK ADD  CONSTRAINT [FK_worker_company] FOREIGN KEY([company_id])
REFERENCES [dbo].[company] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[worker] CHECK CONSTRAINT [FK_worker_company]
GO

ALTER TABLE [dbo].[worker]  WITH CHECK ADD  CONSTRAINT [CK__worker__job__2C3393D0] CHECK  (([job]='Manager' OR [job]='Business Analyst' OR [job]='Tester' OR [job]='Developer'))
GO

ALTER TABLE [dbo].[worker] CHECK CONSTRAINT [CK__worker__job__2C3393D0]
GO


