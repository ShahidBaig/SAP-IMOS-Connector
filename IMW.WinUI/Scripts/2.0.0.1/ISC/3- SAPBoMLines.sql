/****** Object:  Table [dbo].[SAPBoMLines]    Script Date: 5/23/2023 3:53:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SAPBoMLines]') AND type in (N'U'))
DROP TABLE [dbo].[SAPBoMLines]
GO

/****** Object:  Table [dbo].[SAPBoMLines]    Script Date: 5/23/2023 3:53:28 PM ******/
CREATE TABLE [dbo].[SAPBoMLines](
	[OrderID] [nvarchar](80) NULL,
	[Line_No] [int] NULL,
	[ItemCode] [nvarchar](100) NULL,
	[ItemName] [nvarchar](200) NULL,
	[ID] [nvarchar](60) NULL,
	[DfltWH] [nvarchar](80) NULL,
	[VatGourpSa] [nvarchar](80) NULL,
	[SalUnitMsr] [nvarchar](80) NULL,
	[PriceList] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[Qty] [real] NULL,
	[SequenceNo] [real] NULL,
	[UgpEntry] [int] NULL,
	[Type] [int] NULL,
	[ParentID] [nvarchar](100) NULL
) ON [PRIMARY]
GO


