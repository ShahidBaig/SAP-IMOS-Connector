CREATE PROCEDURE sp_ClearIMOSMappedTablesforSQ
	@p_OrderID NVARCHAR(60)
AS
BEGIN
	DECLARE @l_OrderID NVARCHAR(60)

	SET @l_OrderID = @p_OrderID

	DELETE FROM SalesQuotationItemsList
	WHERE OrderID = @l_OrderID
END