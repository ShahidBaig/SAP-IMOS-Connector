DROP PROCEDURE IF EXISTS dbo.SAP_InsertQtyConversionDetail
GO

CREATE PROCEDURE [dbo].[SAP_InsertQtyConversionDetail] 
	@p_Group1				INT,
	@p_Group2				INT,
	@p_Group3				INT,
	@p_Group4				INT,
	@p_FormulaNo			INT,
	@p_SequenceNo			REAL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		@l_Group1				INT,
		@l_Group2				INT,
		@l_Group3				INT,
		@l_Group4				INT,
		@l_FormulaNo			INT,
		@l_SequenceNo			REAL	,
		@l_QtyFormulaNo			INT

	BEGIN TRY
		SET @l_Group1		= @p_Group1
		SET @l_Group2		= CASE WHEN @p_Group2 = -1 THEN NULL ELSE @p_Group2 END
		SET @l_Group3		= CASE WHEN @p_Group3 = -1 THEN NULL ELSE @p_Group3 END
		SET @l_Group4		= CASE WHEN @p_Group4 = -1 THEN NULL ELSE @p_Group4 END
		SET @l_FormulaNo	= CASE WHEN @p_FormulaNo = -1 THEN NULL ELSE @p_FormulaNo END
		SET @l_SequenceNo	= CASE WHEN @p_SequenceNo = 0 THEN NULL ELSE @p_SequenceNo END

		SELECT @l_QtyFormulaNo = QtyFormulaNo 
		FROM QtyConversionDetail WITH (NOLOCK) 
		WHERE Grp1No = @l_Group1 AND ISNULL(Grp2No,0) = ISNULL(@l_Group2,0) AND ISNULL(Grp3No,0) = ISNULL(@l_Group3,0) AND ISNULL(Grp4No,0) = ISNULL(@l_Group4,0)

		IF ISNULL(@l_QtyFormulaNo, 0) > 0
		BEGIN
			UPDATE QtyConversionDetail 
			SET FormulaNo = @l_FormulaNo, SequenceNo = @l_SequenceNo
			WHERE QtyFormulaNo = @l_QtyFormulaNo

			SELECT CONVERT(BIT, 1) Success, 100 Code, 'Formula update Sucessfully' [Description]
		END
		ELSE
		BEGIN
			INSERT INTO QtyConversionDetail(Grp1No,Grp2No,Grp3No,Grp4No,FormulaNo,SequenceNo)
			VALUES (@l_Group1,@l_Group2,@l_Group3,@l_Group4,@l_FormulaNo,@l_SequenceNo)

			SELECT CONVERT(BIT, 1) Success, 100 Code, 'Formula saved successfully' [Description]
		END
	END TRY
	BEGIN CATCH
		DECLARE @l_ExceptionMessage AS NVARCHAR(MAX)

		SET @l_ExceptionMessage = ERROR_MESSAGE() + ' err src line: ' + CAST(ERROR_LINE() AS NVARCHAR(20)) + ' ' + ISNULL(ERROR_PROCEDURE(), '')

		SELECT CONVERT(BIT, 0) Success, 900 Code, @l_ExceptionMessage [Description]
	END CATCH		
END

GO


