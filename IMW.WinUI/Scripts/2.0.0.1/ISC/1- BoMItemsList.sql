/****** Object:  Table [dbo].[BoMItemsList]    Script Date: 5/23/2023 3:19:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoMItemsList]') AND type in (N'U'))
DROP TABLE [dbo].[BoMItemsList]
GO

/****** Object:  Table [dbo].[BoMItemsList]    Script Date: 5/23/2023 3:19:50 PM ******/
CREATE TABLE [dbo].[BoMItemsList](
	[OrderID] [nvarchar](80) NULL,
	[ID] [nvarchar](60) NULL,
	[ItemCode] [nvarchar](160) NULL,
	[Type] [int] NULL,
	[ParentID] [nvarchar](160) NULL,
	[Qty] [real] NULL,
	[Depth] [real] NULL,
	[Width] [real] NULL,
	[Height] [real] NULL,
	[IDBEXT_Length] [real] NULL,
	[IDBEXT_Width] [real] NULL,
	[IDBEXT_Thickness] [real] NULL
) ON [PRIMARY]
GO


