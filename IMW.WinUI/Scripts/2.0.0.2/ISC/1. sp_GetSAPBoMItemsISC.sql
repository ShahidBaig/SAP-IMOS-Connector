
ALTER PROCEDURE [dbo].[sp_GetSAPBoMItemsISC]
	@p_OrderID NVARCHAR(80)
AS
BEGIN
	DECLARE @l_OrderID	NVARCHAR(80),
			@l_SQL		VARCHAR(MAX),
			@l_LineNo	INT = 0

	SET @l_OrderID = @p_OrderID

	DROP TABLE IF EXISTS #OITMItemGroups
	DROP TABLE IF EXISTS #OITMItems
	DROP TABLE IF EXISTS #OrderItems
	DROP TABLE IF EXISTS #SAPQuotationLines
	DROP TABLE IF EXISTS #QtyConversionDetail

	CREATE TABLE #OITMItemGroups
	(
		ItemCode NVARCHAR(100),
		ItemName NVARCHAR(200),
		Grp1Name VARCHAR(250),
		Grp2Name VARCHAR(250),
		Grp3Name VARCHAR(250),
		Grp4Name VARCHAR(250),
		DfltWH NVARCHAR(80), 
		VatGourpSa NVARCHAR(80),
		SalUnitMsr  NVARCHAR(80),
		PriceList INT,
		Price DECIMAL,
		FormulaDesc VARCHAR(MAX),
		SequenceNo REAL,
		UgpEntry INT
	)

	CREATE TABLE #OITMItemGroups_T
	(
		ItemCode NVARCHAR(100),
		ItemName NVARCHAR(200),
		Grp1Name VARCHAR(250),
		Grp2Name VARCHAR(250),
		Grp3Name VARCHAR(250),
		Grp4Name VARCHAR(250),
		DfltWH NVARCHAR(80), 
		VatGourpSa NVARCHAR(80),
		SalUnitMsr  NVARCHAR(80),
		PriceList INT,
		Price DECIMAL,
		FormulaDesc VARCHAR(MAX),
		SequenceNo REAL,
		UgpEntry INT
	)

	CREATE TABLE #OITMItems
	(
		ItemCode NVARCHAR(100),
		ItemName NVARCHAR(200),
		U_Grp1Name VARCHAR(250),
		U_Grp2Name VARCHAR(250),
		U_Grp3Name VARCHAR(250),
		U_Grp4Name VARCHAR(250),
		DfltWH NVARCHAR(80), 
		VatGourpSa NVARCHAR(80),
		SalUnitMsr  NVARCHAR(80),
		BuyUnitMsr  NVARCHAR(80),
		PriceList INT,
		Price DECIMAL,
		UgpEntry INT,
		BLength1 REAL,
		BWidth1 REAL
	)

	CREATE TABLE #OITMItemsWithParent
	(
		ItemCode NVARCHAR(100),
		ItemName NVARCHAR(200),
		U_Grp1Name VARCHAR(250),
		U_Grp2Name VARCHAR(250),
		U_Grp3Name VARCHAR(250),
		U_Grp4Name VARCHAR(250),
		DfltWH NVARCHAR(80), 
		VatGourpSa NVARCHAR(80),
		SalUnitMsr  NVARCHAR(80),
		BuyUnitMsr  NVARCHAR(80),
		PriceList INT,
		Price DECIMAL,
		UgpEntry INT,
		FrgnName NVARCHAR(100),
		BLength1 REAL,
		BWidth1 REAL
	)

	CREATE TABLE #OrderItems
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		ItemCode NVARCHAR(160),
		[Type] [int] NULL,
		[ParentID] [nvarchar](160) NULL,	
		Qty REAL,
		Depth REAL,
		Width REAL,
		Height REAL,
		IDBEXT_Length REAL,
		IDBEXT_Width REAL,
		IDBEXT_Thickness REAL
	)

	CREATE TABLE #OrderItemsWithParent
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		ItemCode NVARCHAR(160),
		[Type] [int] NULL,
		[ParentID] [nvarchar](160) NULL,	
		Qty REAL,
		Depth REAL,
		Width REAL,
		Height REAL,
		IDBEXT_Length REAL,
		IDBEXT_Width REAL,
		IDBEXT_Thickness REAL
	)

	CREATE TABLE #QtyConversionDetail
	(
		QtyFormulaNo	int,
		Grp1Name	varchar(50),
		Grp2Name	varchar(50),
		Grp3Name	varchar(50),
		Grp4Name	varchar(50),
		FormulaDesc	varchar(500),
		SequenceNo	real,
		ItemGroupData	varchar(203)
	)

	INSERT INTO #OrderItems
	SELECT *
	FROM BoMItemsList
	WHERE OrderID = @l_OrderID AND [Type] = 1

	INSERT INTO #OrderItemsWithParent
	SELECT *
	FROM BoMItemsList
	WHERE OrderID = @l_OrderID AND [Type] > 1

	INSERT INTO #QtyConversionDetail
	SELECT QtyFormulaNo, REPLACE(ISNULL(Grp1Name, ''), 'N/A', ''), REPLACE(ISNULL(Grp2Name, ''), 'N/A', ''), 
		REPLACE(ISNULL(Grp3Name, ''), 'N/A', ''), REPLACE(ISNULL(Grp4Name, ''), 'N/A', ''), FormulaDesc, ISNULL(SequenceNo, 99999), ItemGroupData
	FROM VW_QtyConversionDetail

	INSERT INTO #OITMItems
	SELECT O.ItemCode, O.ItemName, REPLACE(ISNULL(O.U_Grp1Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', ''), 
		REPLACE(ISNULL(O.U_Grp3Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp4Name, ''), 'N/A', ''), O.DfltWH, O.VatGourpSa, 
		O.SalUnitMsr, O.BuyUnitMsr, -1, 0, O.UgpEntry, O.BLength1, O.BWidth1
	FROM OITM O WITH (NOLOCK)
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)

	INSERT INTO #OITMItemsWithParent
	SELECT O.ItemCode, O.ItemName, REPLACE(ISNULL(O.U_Grp1Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', ''), 
		REPLACE(ISNULL(O.U_Grp3Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp4Name, ''), 'N/A', ''), O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.BuyUnitMsr, -1, 0, O.UgpEntry,
		O.FrgnName, O.BLength1, O.BWidth1
	FROM OITM O WITH (NOLOCK)
	WHERE O.FrgnName IN (SELECT DISTINCT ItemCode FROM #OrderItemsWithParent WHERE Type <> 10)

	INSERT INTO #OITMItemsWithParent
	SELECT O.ResCode, O.ResName, '', '','', '', O.DfltWH, NULL, O.UnitOfMsr, O.UnitOfMsr, -1, 0, NULL,
		O.FrgnName, NULL, NULL
	FROM ORSC O WITH (NOLOCK)
	WHERE O.ResCode IN (SELECT DISTINCT ItemCode FROM #OrderItemsWithParent WHERE Type = 10)

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND O.U_Grp2Name <> ''
		AND Q.Grp3Name = '' AND Q.Grp4Name = ''

	INSERT INTO #OITMItemGroups_T
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
			AND O.U_Grp3Name = Q.Grp3Name
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND Q.Grp3Name <> '' 
		AND Q.Grp4Name = ''

	UPDATE O SET FormulaDesc = T.FormulaDesc, SequenceNo = T.SequenceNo
	FROM #OITMItemGroups O
		INNER JOIN #OITMItemGroups_T T ON O.ItemCode = T.ItemCode
		
	INSERT INTO #OITMItemGroups
	SELECT *
	FROM #OITMItemGroups_T
	WHERE ItemCode NOT IN (SELECT DISTINCT ItemCode FROM #OITMItemGroups) 

	TRUNCATE TABLE #OITMItemGroups_T
		
	INSERT INTO #OITMItemGroups_T
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		INNER JOIN ITM1 IT WITH (NOLOCK) ON O.ItemCode = IT.ItemCode
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
			AND O.U_Grp3Name = Q.Grp3Name AND O.U_Grp4Name = Q.Grp4Name
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)
		AND Q.Grp1Name <> '' AND Q.Grp2Name <> '' AND Q.Grp3Name <> '' AND Q.Grp4Name <> ''

	UPDATE O SET FormulaDesc = T.FormulaDesc, SequenceNo = T.SequenceNo
	FROM #OITMItemGroups O
		INNER JOIN #OITMItemGroups_T T ON O.ItemCode = T.ItemCode
		
	INSERT INTO #OITMItemGroups
	SELECT *
	FROM #OITMItemGroups_T
	WHERE ItemCode NOT IN (SELECT DISTINCT ItemCode FROM #OITMItemGroups) 

	DELETE FROM SAPBoMLines
	WHERE OrderID = @l_OrderID

	INSERT INTO SAPBoMLines (Line_No, ItemCode, ItemName, OrderID, ID, DfltWH, VatGourpSa, SalUnitMsr, 
		PriceList, Price, Qty, SequenceNo, UgpEntry, [Type], ParentID)
	SELECT ROW_NUMBER() OVER (ORDER BY OI.ItemCode) Line_No, OI.ItemCode, MAX(ISNULL(OG.ItemName, O.ItemName)) ItemName, MAX(OrderID) OrderID, MAX(OI.ID) ID, 
		CASE WHEN MAX(ISNULL(OG.DfltWH, O.DfltWH)) = '' THEN 'M-F-UNT3' ELSE MAX(ISNULL(OG.DfltWH, O.DfltWH)) END DfltWH, 
		MAX(ISNULL(OG.VatGourpSa, O.VatGourpSa)) VatGourpSa, MAX(ISNULL(OG.SalUnitMsr, O.SalUnitMsr)) SalUnitMsr, MAX(ISNULL(OG.PriceList, O.PriceList)) PriceList, MAX(ISNULL(OG.Price, O.Price)) Price,
		1 Quantity, 
		MAX(ISNULL(OG.SequenceNo, 99999)) SequenceNo, MAX(ISNULL(OG.UgpEntry, O.UgpEntry)) UgpEntry,
		MAX(OI.[Type]), MAX(OI.ParentID)
	FROM #OrderItems OI
		LEFT OUTER JOIN #OITMItems O ON OI.ItemCode = O.ItemCode
		LEFT OUTER JOIN #OITMItemGroups OG ON OI.ItemCode = OG.ItemCode
	GROUP BY OI.ItemCode

	SET @l_LineNo = @@ROWCOUNT

	INSERT INTO SAPBoMLines (Line_No, ItemCode, ItemName, OrderID, ID, DfltWH, VatGourpSa, SalUnitMsr, 
		PriceList, Price, Qty, SequenceNo, UgpEntry, [Type], ParentID)
	SELECT @l_LineNo + (ROW_NUMBER() OVER (ORDER BY OI.ItemCode)) Line_No, MAX(CASE WHEN OI.Type IN (2, 3) THEN OI.ItemCode ELSE O.ItemCode END), MAX(O.ItemName) ItemName, MAX(OrderID) OrderID, MAX(OI.ID) ID, 
		CASE WHEN MAX(O.DfltWH) = '' THEN 'M-F-UNT3' ELSE MAX(O.DfltWH) END DfltWH, 
		MAX(O.VatGourpSa) VatGourpSa, MAX(O.SalUnitMsr) SalUnitMsr, MAX(O.PriceList) PriceList, MAX(O.Price) Price,
		CASE 
			WHEN MAX(O.BuyUnitMsr) = 'SHEET' THEN SUM(CASE WHEN O.BLength1 = 0 or O.BWidth1 = 0 THEN 0 ELSE ((OI.IDBEXT_Length * OI.IDBEXT_Width)/(O.BLength1 * O.BWidth1))*1.17 END)
			WHEN MAX(O.U_Grp2Name) = 'PVC' And MAX(O.BuyUnitMsr) = 'METER' THEN SUM((OI.IDBEXT_Length+100)/1000)
			WHEN MAX(O.U_Grp1Name) = 'POLISH' And MAX(O.BuyUnitMsr) = 'SMT' THEN SUM((OI.IDBEXT_Length * OI.IDBEXT_Width)/1000000)
			WHEN MAX(O.U_Grp2Name) = 'FOIL PAPER' And MAX(O.BuyUnitMsr) = 'CFT' THEN SUM(((OI.IDBEXT_Length+90)*(OI.IDBEXT_Width+90))/1000/1000)
			WHEN MAX(O.U_Grp1Name) = 'VENEER' And MAX(O.BuyUnitMsr) = 'SFT' THEN SUM(((OI.IDBEXT_Length * OI.IDBEXT_Width)/POWER(304.8,2)) * 1.15)
			WHEN MAX(O.U_Grp1Name) = 'WOOD' And MAX(O.BuyUnitMsr) = 'CFT' THEN SUM((((OI.IDBEXT_Length+10) * (OI.IDBEXT_Width+7) * (OI.IDBEXT_Thickness+4))/POWER(304.8,3)) * 1.42)
			WHEN MAX(O.U_Grp1Name) = 'GLASS' THEN SUM((OI.IDBEXT_Length * OI.IDBEXT_Width)/POWER(304.8,2))
			ELSE COUNT(OI.ItemCode)
		END Quantity, 99999 SequenceNo, MAX(O.UgpEntry) UgpEntry,
		OI.[Type], OI.ParentID
	FROM #OrderItemsWithParent OI
		LEFT OUTER JOIN #OITMItemsWithParent O ON OI.ItemCode = O.FrgnName
	WHERE OI.Type <> 10
	GROUP BY OI.ItemCode, OI.[Type], OI.ParentID

	INSERT INTO SAPBoMLines (Line_No, ItemCode, ItemName, OrderID, ID, DfltWH, VatGourpSa, SalUnitMsr, 
		PriceList, Price, Qty, SequenceNo, UgpEntry, [Type], ParentID)
	SELECT @l_LineNo + (ROW_NUMBER() OVER (ORDER BY OI.ItemCode)) Line_No, MAX(O.ItemCode), MAX(O.ItemName) ItemName, MAX(OrderID) OrderID, MAX(OI.ID) ID, 
		CASE WHEN MAX(O.DfltWH) = '' THEN 'M-F-UNT3' ELSE MAX(O.DfltWH) END DfltWH, 
		MAX(O.VatGourpSa) VatGourpSa, MAX(O.SalUnitMsr) SalUnitMsr, MAX(O.PriceList) PriceList, MAX(O.Price) Price,
		CASE 
			WHEN MAX(O.BuyUnitMsr) = 'SHEET' THEN SUM(CASE WHEN O.BLength1 = 0 or O.BWidth1 = 0 THEN 0 ELSE ((OI.IDBEXT_Length * OI.IDBEXT_Width)/(O.BLength1 * O.BWidth1))*1.17 END)
			WHEN MAX(O.U_Grp2Name) = 'PVC' And MAX(O.BuyUnitMsr) = 'METER' THEN SUM((OI.IDBEXT_Length+100)/1000)
			WHEN MAX(O.U_Grp1Name) = 'POLISH' And MAX(O.BuyUnitMsr) = 'SMT' THEN SUM((OI.IDBEXT_Length * OI.IDBEXT_Width)/1000000)
			WHEN MAX(O.U_Grp2Name) = 'FOIL PAPER' And MAX(O.BuyUnitMsr) = 'CFT' THEN SUM(((OI.IDBEXT_Length+90)*(OI.IDBEXT_Width+90))/1000/1000)
			WHEN MAX(O.U_Grp1Name) = 'VENEER' And MAX(O.BuyUnitMsr) = 'SFT' THEN SUM(((OI.IDBEXT_Length * OI.IDBEXT_Width)/POWER(304.8,2)) * 1.15)
			WHEN MAX(O.U_Grp1Name) = 'WOOD' And MAX(O.BuyUnitMsr) = 'CFT' THEN SUM((((OI.IDBEXT_Length+10) * (OI.IDBEXT_Width+7) * (OI.IDBEXT_Thickness+4))/POWER(304.8,3)) * 1.42)
			WHEN MAX(O.U_Grp1Name) = 'GLASS' THEN SUM((OI.IDBEXT_Length * OI.IDBEXT_Width)/POWER(304.8,2))
			ELSE COUNT(OI.ItemCode)
		END Quantity, 99999 SequenceNo, MAX(O.UgpEntry) UgpEntry,
		OI.[Type], OI.ParentID
	FROM #OrderItemsWithParent OI
		LEFT OUTER JOIN #OITMItemsWithParent O ON OI.ItemCode = O.ItemCode
	WHERE OI.Type = 10
	GROUP BY OI.ItemCode, OI.[Type], OI.ParentID

	SELECT Q.* 
	FROM SAPBoMLines Q
	WHERE OrderID = @l_OrderID
	ORDER BY Q.[Type]
END
