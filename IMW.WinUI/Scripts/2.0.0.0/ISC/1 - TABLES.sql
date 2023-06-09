CREATE TABLE SalesQuotationItemsList
(
	OrderID NVARCHAR(80),
	ID NVARCHAR(60),
	ItemCode NVARCHAR(160),
	Qty REAL,
	Depth REAL,
	Width REAL,
	Height REAL
)
GO

CREATE TABLE QtyConversionDetail
(
	QtyFormulaNo INT IDENTITY (1, 1) PRIMARY KEY,
	Grp1No INT,
	Grp2No INT,
	Grp3No INT,
	Grp4No INT,
	FormulaNo INT,
	SequenceNo INT
)
GO

CREATE TABLE Group1
(
	SerialNo INT IDENTITY (1, 1) PRIMARY KEY,
	GrpName VARCHAR(50)
)
GO

CREATE TABLE Group2
(
	SerialNo INT IDENTITY (1, 1) PRIMARY KEY,
	GrpName VARCHAR(50)
)
GO

CREATE TABLE Group3
(
	SerialNo INT IDENTITY (1, 1) PRIMARY KEY,
	GrpName VARCHAR(50)
)
GO

CREATE TABLE Group4
(
	SerialNo INT IDENTITY (1, 1) PRIMARY KEY,
	GrpName VARCHAR(50)
)
GO

CREATE TABLE Formulas
(
	FormulaNo INT IDENTITY (1, 1) PRIMARY KEY,
	FormulaDesc VARCHAR(500)
)
GO

CREATE VIEW VW_QtyConversionDetail
AS
	SELECT QtyFormulaNo, G1.GrpName Grp1Name, G2.GrpName Grp2Name, G3.GrpName Grp3Name, G4.GrpName Grp4Name, F.FormulaDesc, SequenceNo,
		ISNULL(G1.GrpName, '') + '-' + ISNULL(G2.GrpName, '') + '-' + ISNULL(G3.GrpName, '') + '-' + ISNULL(G4.GrpName, '') ItemGroupData
	FROM QtyConversionDetail Q
		LEFT OUTER JOIN Group1 G1 ON Q.Grp1No = G1.SerialNo
		LEFT OUTER  JOIN Group2 G2 ON Q.Grp2No = G2.SerialNo
		LEFT OUTER  JOIN Group3 G3 ON Q.Grp3No = G3.SerialNo
		LEFT OUTER  JOIN Group4 G4 ON Q.Grp4No = G4.SerialNo
		LEFT OUTER  JOIN Formulas F ON Q.FormulaNo = F.FormulaNo
GO


INSERT INTo Group1
SELECT 'KITCHEN'
GO

INSERT INTo Group2
SELECT 'HARDWARE'
UNION
SELECT 'KITCHEN ACC'
UNION
SELECT 'KITCHEN APP'
UNION
SELECT 'KITCHEN LIGHTS'
UNION
SELECT 'WORK TOP'
UNION
SELECT 'KITCHEN CABINETRY'
GO

INSERT INTo Group3
SELECT 'BASE CABINET'
UNION
SELECT 'WALL CABINET'
UNION
SELECT 'TALL CABINET'
UNION
SELECT 'FRONT'
UNION
SELECT 'COMPONENT'
GO

INSERT INTo Group4
SELECT 'CANTILIVER'
UNION
SELECT 'CORNICE'
UNION
SELECT 'DUCTING'
UNION
SELECT 'SHADE'
UNION
SELECT 'PEDESTAL'
UNION
SELECT 'BACK SPLASH/FILLET'
UNION
SELECT 'DUMMY SHUTTER'
UNION
SELECT 'SKIRTING'
UNION
SELECT 'PLINTH'
UNION
SELECT 'LIGHT STRIP'
GO

INSERT INTo Formulas
SELECT 'Count Line No'
UNION
SELECT 'Sum((WIDTH * DEPTH)/1000000)'
UNION
SELECT 'Sum(WIDTH/2400)'
UNION
SELECT 'Sum((HEIGHT * WIDTH)/1000000)'
UNION
SELECT 'Sum((HEIGHT * DEPTH)/1000000)'
UNION
SELECT 'Sum(WIDTH/3000)'
UNION
SELECT 'Sum(WIDTH/5000)'
GO



INSERT INTO QtyConversionDetail
SELECT 1, 1, NULL, NULL, 1
UNION
SELECT 1, 2, NULL, NULL, 1
UNION
SELECT 1, 3, NULL, NULL, 1
UNION
SELECT 1, 5, NULL, NULL, 1
UNION
SELECT 1, 6, NULL, NULL, 4
UNION
SELECT 1, 4, 1, NULL, 1
UNION
SELECT 1, 4, 5, NULL, 1
UNION
SELECT 1, 4, 4, NULL, 1
UNION
SELECT 1, 4, 3, NULL, 3
UNION
SELECT 1, 4, 2, 2, 5
UNION
SELECT 1, 4, 2, 3, 5
UNION
SELECT 1, 4, 2, 4, 5
UNION
SELECT 1, 4, 2, 9, 5
UNION
SELECT 1, 4, 2, 7, 1
UNION
SELECT 1, 4, 2, 1, 3
UNION
SELECT 1, 4, 2, 5, 2
UNION
SELECT 1, 4, 2, 10, 5
UNION
SELECT 1, 4, 2, 8, 6
UNION
SELECT 1, 4, 2, 6, 7
GO

CREATE TABLE SAPQuotationLines
(
	OrderID NVARCHAR(80),
	Line_No INT,
	ItemCode NVARCHAR(100),
	ItemName NVARCHAR(200),
	ID NVARCHAR(60),
	DfltWH NVARCHAR(80), 
	VatGourpSa NVARCHAR(80),
	SalUnitMsr  NVARCHAR(80),
	PriceList INT,
	Price DECIMAL,
	Qty REAL,
	SequenceNo INT
)
GO

INSERT INTO Group3
SELECT 'REFRIGERATOR'
UNION
SELECT 'BASKET'
UNION
SELECT 'WASTE BIN'
UNION
SELECT 'TRAY'
UNION
SELECT 'CORIAN'
UNION
SELECT 'LIGHT'
GO


UPDATE QtyConversionDetail SET SequenceNo = 1 WHERE QtyFormulaNo = 23
UPDATE QtyConversionDetail SET SequenceNo = 2 WHERE QtyFormulaNo = 36
UPDATE QtyConversionDetail SET SequenceNo = 3 WHERE QtyFormulaNo = 35
UPDATE QtyConversionDetail SET SequenceNo = 4 WHERE QtyFormulaNo = 34
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 24
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 25
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 26
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 27
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 28
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 29
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 30
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 31
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 32
UPDATE QtyConversionDetail SET SequenceNo = 5 WHERE QtyFormulaNo = 33
GO

INSERT INTO QtyConversionDetail
SELECT 1, 3, 8, NULL, NULL, 6
UNION
SELECT 1, 2, 6, NULL, NULL, 7
UNION
SELECT 1, 2, 10, NULL, NULL, 8
UNION
SELECT 1, 2, 9, NULL, NULL, 9
UNION
SELECT 1, 5, 11, NULL, NULL, 10
UNION
SELECT 1, 6, 7, NULL, NULL, 11
GO

ALTER TABLE OQUT
ADD LogDate DATETIME
GO


ALTER TABLE QtyConversionDetail ALTER COLUMN SequenceNo REAL NULL
GO

ALTER TABLE SAPQuotationLines
ADD UgpEntry INT
GO

ALTER TABLE SAPQuotationLines 
ALTER COLUMN SequenceNo REAL NULL
GO

CREATE TABLE OpportunitySource
(
	OppoSource VARCHAR(50),
	SAPSource VARCHAR(50)
)
GO

INSERT INTO OpportunitySource VALUES ('I9','M-N/I9')
INSERT INTO OpportunitySource VALUES ('Golra','M-N/Gol')
INSERT INTO OpportunitySource VALUES ('DHA ISL','M-N/DHI')
INSERT INTO OpportunitySource VALUES ('Hayatabad','M-N/HYT')
INSERT INTO OpportunitySource VALUES ('Corporate - North','M-N/CORP')
INSERT INTO OpportunitySource VALUES ('Garden Town','M-C/GT')
INSERT INTO OpportunitySource VALUES ('Corporate - Central','M-C/CORP')
INSERT INTO OpportunitySource VALUES ('Gujranwala','M-C/GRW')
INSERT INTO OpportunitySource VALUES ('DHA Y Block','M-C/DHA')
INSERT INTO OpportunitySource VALUES ('Bukhari','M-S/BOK')
INSERT INTO OpportunitySource VALUES ('Corporate - South','M-S/CORP')
INSERT INTO OpportunitySource VALUES ('Lucky One','M-S/LO')
INSERT INTO OpportunitySource VALUES ('Online','M-S/KO')
GO



