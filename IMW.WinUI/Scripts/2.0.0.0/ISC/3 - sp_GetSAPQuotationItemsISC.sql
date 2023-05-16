DROP PROCEDURE IF EXISTS dbo.sp_GetSAPQuotationItemsISC
GO

CREATE PROCEDURE [dbo].[sp_GetSAPQuotationItemsISC]
	@p_OrderID NVARCHAR(80)
AS
BEGIN
	DECLARE @l_OrderID NVARCHAR(80),
			@l_SQL VARCHAR(MAX)

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
		PriceList INT,
		Price DECIMAL,
		UgpEntry INT
	)

	CREATE TABLE #OrderItems
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		ItemCode NVARCHAR(160),
		Qty REAL,
		Depth REAL,
		Width REAL,
		Height REAL
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
	FROM SalesQuotationItemsList
	WHERE OrderID = @l_OrderID

	INSERT INTO #QtyConversionDetail
	SELECT QtyFormulaNo, REPLACE(ISNULL(Grp1Name, ''), 'N/A', ''), REPLACE(ISNULL(Grp2Name, ''), 'N/A', ''), 
		REPLACE(ISNULL(Grp3Name, ''), 'N/A', ''), REPLACE(ISNULL(Grp4Name, ''), 'N/A', ''), FormulaDesc, ISNULL(SequenceNo, 99999), ItemGroupData
	FROM VW_QtyConversionDetail

	INSERT INTO #OITMItems
	SELECT O.ItemCode, O.ItemName, REPLACE(ISNULL(O.U_Grp1Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', ''), 
		REPLACE(ISNULL(O.U_Grp3Name, ''), 'N/A', ''), REPLACE(ISNULL(O.U_Grp4Name, ''), 'N/A', ''), O.DfltWH, O.VatGourpSa, O.SalUnitMsr, -1, 0, O.UgpEntry
	FROM OITM O WITH (NOLOCK)
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND O.U_Grp2Name <> ''
		AND Q.Grp3Name = '' AND Q.Grp4Name = '' AND Q.SequenceNo <> 99999

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
			AND O.U_Grp3Name = Q.Grp3Name
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND Q.Grp3Name <> '' 
		AND Q.Grp4Name = '' AND Q.SequenceNo <> 99999
		
	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, Q.SequenceNo, UgpEntry
	FROM #OITMItems O
		INNER JOIN ITM1 IT WITH (NOLOCK) ON O.ItemCode = IT.ItemCode
		LEFT OUTER JOIN #QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND O.U_Grp2Name = Q.Grp2Name 
			AND O.U_Grp3Name = Q.Grp3Name AND O.U_Grp4Name = Q.Grp4Name
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)
		AND Q.Grp1Name <> '' AND Q.Grp2Name <> '' AND Q.Grp3Name <> '' AND Q.Grp4Name <> ''
		 AND Q.SequenceNo <> 99999

	DELETE FROM SAPQuotationLines
	WHERE OrderID = @l_OrderID

	INSERT INTO SAPQuotationLines (Line_No, ItemCode, ItemName, OrderID, ID, DfltWH, VatGourpSa, SalUnitMsr, PriceList, Price, Qty, SequenceNo, UgpEntry)
	SELECT ROW_NUMBER() OVER (ORDER BY OI.ItemCode) Line_No, OI.ItemCode, MAX(ISNULL(OG.ItemName, O.ItemName)) ItemName, MAX(OrderID) OrderID, MAX(OI.ID) ID, 
		CASE WHEN MAX(ISNULL(OG.DfltWH, O.DfltWH)) = '' THEN 'M-F-UNT3' ELSE MAX(ISNULL(OG.DfltWH, O.DfltWH)) END DfltWH, 
		MAX(ISNULL(OG.VatGourpSa, O.VatGourpSa)) VatGourpSa, MAX(ISNULL(OG.SalUnitMsr, O.SalUnitMsr)) SalUnitMsr, MAX(ISNULL(OG.PriceList, O.PriceList)) PriceList, MAX(ISNULL(OG.Price, O.Price)) Price,
		CASE WHEN MAX(Qty) = 0 THEN
			CASE WHEN MAX(ISNULL(OG.ItemCode, '')) = '' AND MAX(ISNULL(OG.FormulaDesc, '')) = ''  THEN COUNT(OI.ItemCode) 
					ELSE CASE WHEN MAX(FormulaDesc) = 'Count Line No' THEN COUNT(OI.ItemCode) 
							  WHEN MAX(FormulaDesc) = 'SUM((WIDTH * DEPTH)/1000000)' THEN SUM((ISNULL(WIDTH, 0) * ISNULL(DEPTH, 0))/1000000) 
							  WHEN MAX(FormulaDesc) = 'SUM(WIDTH/2400)' THEN SUM(ISNULL(WIDTH, 0)/2400) 
							  WHEN MAX(FormulaDesc) = 'SUM((HEIGHT * WIDTH)/1000000)' THEN SUM((ISNULL(HEIGHT, 0) * ISNULL(WIDTH, 0))/1000000) 
							  WHEN MAX(FormulaDesc) = 'SUM((HEIGHT * DEPTH)/1000000)' THEN SUM((ISNULL(HEIGHT, 0) * ISNULL(DEPTH, 0))/1000000) 
							  WHEN MAX(FormulaDesc) = 'SUM(WIDTH/3000)' THEN SUM(ISNULL(WIDTH, 0)/3000) 
							  WHEN MAX(FormulaDesc) = 'SUM(WIDTH/5000)' THEN SUM(ISNULL(WIDTH, 0)/5000)
							  WHEN MAX(FormulaDesc) = 'SUM(WIDTH/1000)' THEN SUM(ISNULL(WIDTH, 0)/1000)
							  WHEN MAX(FormulaDesc) = 'SUM(HEIGHT/1000)' THEN SUM(ISNULL(Height, 0)/1000)
							ELSE COUNT(OI.ItemCode) 
					END
		END ELSE MAX(Qty) END Quantity, MAX(ISNULL(OG.SequenceNo, 99999)) SequenceNo, MAX(ISNULL(OG.UgpEntry, O.UgpEntry)) UgpEntry
	FROM #OrderItems OI
		LEFT OUTER JOIN #OITMItems O ON OI.ItemCode = O.ItemCode
		LEFT OUTER JOIN #OITMItemGroups OG ON OI.ItemCode = OG.ItemCode
	GROUP BY OI.ItemCode

	SELECT Q.* 
	FROM SAPQuotationLines Q
		LEFT OUTER JOIN #OITMItemGroups OG ON Q.ItemCode = OG.ItemCode
	WHERE OrderID = @l_OrderID
	ORDER BY ISNULL(Q.SequenceNo, 99999), OG.Grp1Name, OG.Grp2Name, OG.Grp3Name, OG.Grp4Name
END
GO