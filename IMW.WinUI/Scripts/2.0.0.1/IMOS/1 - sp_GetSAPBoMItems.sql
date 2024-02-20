DROP PROCEDURE IF EXISTS dbo.sp_GetSAPBoMItems
GO

CREATE PROCEDURE [dbo].[sp_GetSAPBoMItems]
	@p_OrderID NVARCHAR(80)
AS
BEGIN
	DECLARE @l_OrderID NVARCHAR(80),
			@l_SQL VARCHAR(MAX),
			@l_Count INT,
			@l_Index INT,
			@l_ProductCode VARCHAR(50)

	SET @l_OrderID = @p_OrderID

	CREATE TABLE #IDBORDEREXT
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		Name NVARCHAR(160),
		FullName NVARCHAR(160),
		Depth REAL,
		Width REAL,
		Height REAL
	)

	CREATE TABLE #IDBEXT
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		Depth REAL,
		Width REAL,
		Height REAL
	)

	CREATE TABLE #IDBEXTWithParent
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		ARTICLE_ID NVARCHAR(60),
		TYP INT,
		PARENTID NVARCHAR(60),
		Qty REAL,
		Depth REAL,
		Width REAL,
		Height REAL
	)

	CREATE TABLE #ISAPItemIdentityConfig
	(
		ProductCode VARCHAR(50),
		Processed INT
	)

	CREATE TABLE #IDBVerso
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		PVARSTRING NVARCHAR(MAX)
	)

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
		FormulaDesc VARCHAR(MAX)
	)

	CREATE TABLE #OrderItemsArticleAndDraw
	(
		OrderID NVARCHAR(80),
		ID NVARCHAR(60),
		ItemCode NVARCHAR(100),
		Qty REAL
	)

	INSERT INTO #ISAPItemIdentityConfig
	SELECT ProductCode, 0
	FROM SAPItemIdentityConfig

	SET @l_Count = @@ROWCOUNT
	SET @l_Index = 0

	INSERT INTO #IDBEXT
	SELECT OrderID, ID, MAX(Length) Length, MAX(WIDTH) WIDTH, MAX(Thickness) Thickness
	FROM IDBEXT 
	WHERE  OrderID = @l_OrderID AND CHARINDEX('_', ID) = 0 AND TYP = 1
	GROUP BY  OrderID, ID 

	INSERT INTO #IDBEXTWithParent
	SELECT O.OrderID, MAX(O.ID), 
		CASE WHEN MAX(O.TYP) = 7 THEN P.BESTELLUNG ELSE O.ARTICLE_ID END, 
		MAX(O.TYP), MAX(O.PARENTID), COUNT(O.ARTICLE_ID), MAX(O.Length) Length, MAX(O.WIDTH) WIDTH, MAX(O.Thickness) Thickness
	FROM IDBEXT O
		LEFT OUTER JOIN [PROFIL] P ON O.NAME = P.NAME AND O.THICKNESS = P.PRFTHK
	WHERE  O.OrderID = @l_OrderID AND O.TYP > 1
	GROUP BY O.OrderID, O.ARTICLE_ID, P.BESTELLUNG

	INSERT INTO #IDBORDEREXT
	SELECT ORDERID, ID, SUBSTRING(Name, CHARINDEX(N'FG', Name), LEN(Name) - CHARINDEX(N'FG', Name) + 1), Name , DEPTH, WIDTH, HEIGHT 
	FROM IDBORDEREXT WITH (NOLOCK)
	WHERE ORDERID = @l_OrderID
		AND ID COLLATE DATABASE_DEFAULT IN (SELECT ID COLLATE DATABASE_DEFAULT FROM #IDBEXT)

	INSERT INTO #IDBVerso
	SELECT OrderID, ID, PVARSTRING
	FROM IDBVERSO WITH (NOLOCK)
	WHERE ORDERID = @l_OrderID

	SET @l_SQL =   'INSERT INTO #OrderItemsArticleAndDraw
					SELECT ORDERID,ID,RIGHT(F.VALUE,11) ITEMCODE, CASE WHEN PATINDEX(''%[A-Z]%'', F.VALUE) <> 1 THEN LEFT(F.VALUE,1) ELSE 0 END Qty --Drawer
					FROM (
							SELECT orderid,id,f.VALUE PvarString
							FROM(
									SELECT orderid,id,f.VALUE As Pvarstring
									FROM #IDBVerso AS s
									CROSS APPLY STRING_SPLIT(s.PVARSTRING, ''|'') as f
									WHERE id NOT IN (SELECT ID FROM #IDBORDEREXT WHERE Name LIKE ''%FG%'')
								)T0
							CROSS APPLY STRING_SPLIT(PVARSTRING, ''='') as f
							WHERE T0.Pvarstring Like ''%_XML%'''
	SET @l_SQL = @l_SQL + 	')A CROSS APPLY STRING_SPLIT(PVARSTRING, '','') as f
					WHERE ('

	SET @l_Index = 0

	UPDATE #ISAPItemIdentityConfig
	SET Processed = 0

	WHILE @l_Index < @l_Count
	BEGIN
		SELECT TOP 1 @l_ProductCode = ProductCode
		FROM #ISAPItemIdentityConfig
		WHERE Processed = 0

		SET @l_SQL = @l_SQL + 'pvarstring Like ''%' + @l_ProductCode + '%'' '
		SET @l_SQL = @l_SQL + CASE WHEN @l_Index < @l_Count - 1 THEN ' OR ' ELSE '' END

		UPDATE #ISAPItemIdentityConfig
		SET Processed = 1
		WHERE ProductCode = @l_ProductCode

		SET @l_Index = @l_Index + 1
	END

	SET @l_SQL = @l_SQL + ')'

	EXEC (@l_SQL)

	SELECT OI.OrderID, OI.ID, ItemCode, 1 [Type], NULL ParentID, Qty, ISNULL(X.Depth, I.Depth) Depth, ISNULL(X.Width, I.Width) Width, ISNULL(X.Height, I.Height) Height,
		I.Depth IDBEXT_Length, I.Width IDBEXT_Width, I.Height IDBEXT_Thickness
	FROM #OrderItemsArticleAndDraw OI
		LEFT OUTER JOIN #IDBORDEREXT X ON OI.OrderID = X.OrderID AND OI.ID = X.ID 
		LEFT OUTER JOIN #IDBEXT I ON OI.OrderID = I.OrderID AND OI.ID = I.ID
	UNION ALL
	SELECT X.OrderID, X.ID, X.Name, 1 [Type], NULL ParentID, 1, X.Depth, X.Width, X.Height, I.Depth, I.Width, I.Height
	FROM #IDBORDEREXT X
		LEFT OUTER JOIN #IDBEXT I ON X.OrderID = I.OrderID AND X.ID = I.ID
	WHERE Name LIKE '%FG%'
	UNION ALL
	SELECT OrderID, ID, ARTICLE_ID, TYP, ParentID, Qty, Depth, Width, Height, Depth, Width, Height
	FROM #IDBEXTWithParent
END
GO
