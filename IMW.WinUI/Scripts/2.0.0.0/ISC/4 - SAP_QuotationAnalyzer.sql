
ALTER PROCEDURE [dbo].[SAP_QuotationAnalyzer] 
	@p_DocNo				INT = '',
	@p_DocDateFrom			VARCHAR(100) = '',
	@p_DocDateTo			VARCHAR(100) = '',
	@p_Status				VARCHAR(100) = ''
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		@l_DocNo				INT,
		@l_DocDateFrom			VARCHAR(100),
		@l_DocDateTo			VARCHAR(100),
		@l_Status				VARCHAR(100),
		@l_SQL					VARCHAR(1000),
		@l_CriteriaSQL			VARCHAR(1000)		

	BEGIN TRY
		SET @l_DocNo			= @p_DocNo
		SET @l_DocDateFrom		= @p_DocDateFrom
		SET @l_DocDateTo		= @p_DocDateTo
		SET @l_Status			= @p_Status

		SET @l_SQL = 'SELECT DocNum [Doc Num],DocDate [Doc Date],CardCode [Customer Code],CardName [Customer Name], '
		SET @l_SQL =  @l_SQL +'CASE WHEN  Posted_IMOS = 0 AND Posted_SAP = 0 AND Completed_IMOS = 0 THEN ''PostedToISC'''
		SET @l_SQL =  @l_SQL +' WHEN  POSTED_IMOS = 1	 THEN ''PostedToIMOS'''
		SET @l_SQL =  @l_SQL +' WHEN  Completed_IMOS = 1 THEN ''ReceivedFromIMOS'''
		SET @l_SQL =  @l_SQL + ' WHEN  Posted_SAP = 1	 THEN ''PostedTOSAP'''
		SET @l_SQL =  @l_SQL + ' END  AS Status'
		SET @l_SQL = @l_SQL + ' FROM  OQUT WITH (NOLOCK)'

		IF @l_DocDateFrom <> ''
		BEGIN 
				IF	@l_CriteriaSQL <> ''
				BEGIN
						SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND  DocDate >=  ''' + CONVERT(VARCHAR,CONVERT(DATE,@l_DocDateFrom)) + ''''
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE  DocDate <=  ''' + CONVERT(VARCHAR,CONVERT(DATE,@l_DocDateTo)) + ''''
				END
		END

		IF @l_DocDateTo <> ''
		BEGIN 
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND  DocDate <=  ''' + CONVERT(VARCHAR,CONVERT(DATE,@l_DocDateTo)) + ''''
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE  DocDate <=  ''' + CONVERT(VARCHAR,CONVERT(DATE,@l_DocDateTo)) + ''''
				END
		END

		IF @l_DocNo > 0
		BEGIN 
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND DocNum =  ' + CONVERT(VARCHAR,@l_DocNo)
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE DocNum =  ' + CONVERT(VARCHAR,@l_DocNo)
				END
		END
		
		IF	@l_Status <> ''
		BEGIN
			IF @l_Status = 'PostedToISC'
			BEGIN 
				
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND Posted_IMOS = 0 AND Posted_SAP = 0 AND Completed_IMOS = 0 '
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL = ' WHERE Posted_IMOS = 0 AND Posted_SAP = 0 AND Completed_IMOS = 0 '
				END
			END 

			IF @l_Status = 'PostedToIMOS'
			BEGIN 
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND POSTED_IMOS = 1 '
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE POSTED_IMOS = 1 '
				END
			END 

			IF @l_Status = 'ReceivedFromIMOS'
			BEGIN 
				
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND Completed_IMOS = 1 '
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE Completed_IMOS = 1 '
				END

				
			END 

			IF @l_Status = 'PostedTOSAP'
			BEGIN
				IF	@l_CriteriaSQL <> ''
				BEGIN
					SET @l_CriteriaSQL = @l_CriteriaSQL + ' AND Posted_SAP = 1 '
				END
				ELSE
				BEGIN
					SET @l_CriteriaSQL =  ' WHERE Posted_SAP = 1 '
				END
			END 
		END

		SET @l_SQL = @l_SQL + @l_CriteriaSQL
		
		EXECUTE (@l_SQL)
		
	END TRY
	BEGIN CATCH
		DECLARE @l_ExceptionMessage AS NVARCHAR(MAX)

		SET @l_ExceptionMessage = ERROR_MESSAGE() + ' err src line: ' + CAST(ERROR_LINE() AS NVARCHAR(20)) + ' ' + ISNULL(ERROR_PROCEDURE(), '')

		SELECT CONVERT(BIT, 0) Success, 900 Code, @l_ExceptionMessage [Description]
	END CATCH		
END

GO
