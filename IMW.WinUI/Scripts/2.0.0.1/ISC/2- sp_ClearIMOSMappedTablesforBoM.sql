DROP PROCEDURE IF EXISTS [dbo].[sp_ClearIMOSMappedTablesforBoM]
GO

CREATE PROCEDURE [dbo].[sp_ClearIMOSMappedTablesforBoM]
	@p_OrderID NVARCHAR(60)
AS
BEGIN
	DECLARE @l_OrderID NVARCHAR(60)

	SET @l_OrderID = @p_OrderID

	DELETE FROM BoMItemsList
	WHERE OrderID = @l_OrderID
END
GO