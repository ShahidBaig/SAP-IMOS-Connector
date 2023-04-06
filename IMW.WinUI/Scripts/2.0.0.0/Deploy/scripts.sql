SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL Serializable
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

DROP TABLE IF EXISTS dbo.SAPItemIdentityConfig
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
CREATE TABLE SAPItemIdentityConfig
(
	SerialNo INT IDENTITY(1, 1) PRIMARY KEY,
	ProductCode VARCHAR(50)
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
INSERT INTO SAPItemIdentityConfig
SELECT 'FG01'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DROP PROCEDURE IF EXISTS dbo.sp_GetSAPQuotationItems
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
CREATE PROCEDURE [dbo].[sp_GetSAPQuotationItems]
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
	SELECT OrderID, ID , MAX(Length) Length, MAX(WIDTH) WIDTH, MAX(Thickness) Thickness
	FROM IDBEXT 
	WHERE  OrderID = @l_OrderID AND CHARINDEX('_', ID) = 0
	GROUP BY  OrderID, ID 

	INSERT INTO #IDBORDEREXT
	SELECT ORDERID, ID, SUBSTRING(Name, CHARINDEX('FG', Name), LEN(Name) - CHARINDEX('FG', Name) + 1) , DEPTH, WIDTH, HEIGHT 
	FROM IDBORDEREXT WITH (NOLOCK)
	WHERE ORDERID = @l_OrderID AND Name LIKE '%FG%'

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
								)T0
							CROSS APPLY STRING_SPLIT(PVARSTRING, ''='') as f
							WHERE T0.Pvarstring Like ''%XML%'' OR ('
	WHILE @l_Index < @l_Count
	BEGIN
		SELECT TOP 1 @l_ProductCode = ProductCode
		FROM #ISAPItemIdentityConfig
		WHERE Processed = 0

		SET @l_SQL = @l_SQL + 'T0.Pvarstring Like ''%' + @l_ProductCode + '%'' '
		SET @l_SQL = @l_SQL + CASE WHEN @l_Index < @l_Count - 1 THEN ' OR ' ELSE '' END

		UPDATE #ISAPItemIdentityConfig
		SET Processed = 1
		WHERE ProductCode = @l_ProductCode

		SET @l_Index = @l_Index + 1
	END
	
		SET @l_SQL = @l_SQL + 	'))A CROSS APPLY STRING_SPLIT(PVARSTRING, '','') as f
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

	Print @l_SQL
	EXEC (@l_SQL)

	SELECT OI.OrderID, OI.ID, ItemCode, Qty, ISNULL(X.Depth, I.Depth) Depth, ISNULL(X.Width, I.Width) Width, ISNULL(X.Height, I.Height) Height
	FROM #OrderItemsArticleAndDraw OI
		LEFT OUTER JOIN #IDBORDEREXT X ON OI.OrderID = X.OrderID AND OI.ID = X.ID 
		LEFT OUTER JOIN #IDBEXT I ON OI.OrderID = I.OrderID AND OI.ID = I.ID
	UNION ALL
	SELECT OrderID, ID, Name, 1, Depth, Width, Height
	FROM #IDBORDEREXT
	WHERE ID NOT IN (SELECT DISTINCT ID FROM #OrderItemsArticleAndDraw)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
INSERT INTO ISCVersions (VersionNo, Description, VersionDate) VALUES ('2.0.0.0','ISC - 2.0.0.0',GETDATE())
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
-- This statement writes to the SQL Server Log so SQL Monitor can show this deployment.
IF HAS_PERMS_BY_NAME(N'sys.xp_logevent', N'OBJECT', N'EXECUTE') = 1
BEGIN
    DECLARE @databaseName AS nvarchar(2048), @eventMessage AS nvarchar(2048)
    SET @databaseName = REPLACE(REPLACE(DB_NAME(), N'\', N'\\'), N'"', N'\"')
    SET @eventMessage = N'ISC: { "deployment": { "description": "ISC deployed to ' + @databaseName + N'", "database": "' + @databaseName + N'" }}'
    EXECUTE sys.xp_logevent 55000, @eventMessage
END
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END