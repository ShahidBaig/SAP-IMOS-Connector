DROP PROCEDURE IF EXISTS dbo.sp_GetSAPQuotationItemsISC
GO

CREATE PROCEDURE [dbo].[sp_GetSAPQuotationItemsISC]
	@p_OrderID NVARCHAR(80)
AS
BEGIN
	DECLARE @l_OrderID NVARCHAR(80),
			@l_SQL VARCHAR(MAX)

	SET @l_OrderID = @p_OrderID

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

	INSERT INTO #OrderItems
	SELECT *
	FROM SalesQuotationItemsList
	WHERE OrderID = @l_OrderID

	INSERT INTO #OITMItems
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, IT.PriceList, IT.Price, O.UgpEntry
	FROM OITM O WITH (NOLOCK)
		INNER JOIN ITM1 IT WITH (NOLOCK) ON O.ItemCode = IT.ItemCode
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, ISNULL(Q.SequenceNo, 99999), UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN VW_QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp2Name, ''), 'N/A', '') 
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND ISNULL(Q.Grp3Name, '') = '' AND ISNULL(Q.Grp4Name, '') = ''

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, ISNULL(Q.SequenceNo, 99999), UgpEntry
	FROM #OITMItems O
		LEFT OUTER JOIN VW_QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp2Name, ''), 'N/A', '') 
			AND REPLACE(ISNULL(O.U_Grp3Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp3Name, ''), 'N/A', '')
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems) AND ISNULL(Q.Grp3Name, '') <> '' AND ISNULL(Q.Grp4Name, '') = ''

	INSERT INTO #OITMItemGroups
	SELECT O.ItemCode, O.ItemName, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name, O.DfltWH, O.VatGourpSa, O.SalUnitMsr, O.PriceList, O.Price, Q.FormulaDesc, ISNULL(Q.SequenceNo, 99999), UgpEntry
	FROM #OITMItems O
		INNER JOIN ITM1 IT WITH (NOLOCK) ON O.ItemCode = IT.ItemCode
		LEFT OUTER JOIN VW_QtyConversionDetail Q ON O.U_Grp1Name = Q.Grp1Name AND REPLACE(ISNULL(O.U_Grp2Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp2Name, ''), 'N/A', '') 
			AND REPLACE(ISNULL(O.U_Grp3Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp3Name, ''), 'N/A', '') AND REPLACE(ISNULL(O.U_Grp4Name, ''), 'N/A', '') = REPLACE(ISNULL(Q.Grp4Name, ''), 'N/A', '')
	WHERE O.ItemCode IN (SELECT DISTINCT ItemCode FROM #OrderItems)
		AND ISNULL(Q.Grp1Name, '') <> '' AND ISNULL(Q.Grp2Name, '') <> '' AND ISNULL(Q.Grp3Name, '') <> '' AND ISNULL(Q.Grp4Name, '') <> ''

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
							ELSE COUNT(OI.ItemCode) 
					END
		END ELSE MAX(Qty) END Quantity, MAX(OG.SequenceNo), MAX(ISNULL(OG.UgpEntry, O.UgpEntry))
	FROM #OrderItems OI
		LEFT OUTER JOIN #OITMItems O ON OI.ItemCode = O.ItemCode
		LEFT OUTER JOIN #OITMItemGroups OG ON OI.ItemCode = OG.ItemCode
	GROUP BY OI.ItemCode

	SELECT * FROM SAPQuotationLines
	WHERE OrderID = @l_OrderID
	ORDER BY ISNULL(SequenceNo, 99999)
END