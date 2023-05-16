DROP PROCEDURE IF EXISTS sp_GetSaleQuotationFromIMOS
GO

CREATE PROCEDURE sp_GetSaleQuotationFromIMOS
AS
BEGIN
	SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, 
		CardName, U_Type1, IMOS_PO_ID, LogDate 
	FROM OQUT 
	WHERE Posted_IMOS = 1 AND (/*(DocDate >= GETDATE() - 30 AND Completed_IMOS = 1) OR*/ Completed_IMOS = 0)
END
GO