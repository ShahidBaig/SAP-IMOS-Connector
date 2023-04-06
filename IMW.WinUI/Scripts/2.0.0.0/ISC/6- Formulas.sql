INSERT INTO Group1
SELECT GroupName
FROM (
	SELECT DISTINCT U_Grp1Name GroupName
	FROM OITM_ItemGroups 
	WHERE ISNULL(U_Grp1Name, '') NOT IN (SELECT GrpName FROM Group1)
		AND ISNULL(U_Grp1Name, '') <> ''
	) A
GO

INSERT INTO Group2
SELECT GroupName
FROM (
	SELECT DISTINCT U_Grp2Name GroupName
	FROM OITM_ItemGroups 
	WHERE ISNULL(U_Grp2Name, '') NOT IN (SELECT GrpName FROM Group2)
		AND ISNULL(U_Grp2Name, '') <> ''
	) A
GO

INSERT INTO Group3
SELECT  GroupName
FROM (
	SELECT DISTINCT U_Grp3Name GroupName
	FROM OITM_ItemGroups 
	WHERE ISNULL(U_Grp3Name, '') NOT IN (SELECT GrpName FROM Group3)
		AND ISNULL(U_Grp3Name, '') <> ''
	) A
GO

INSERT INTO Group4
SELECT GroupName
FROM (
	SELECT DISTINCT U_Grp4Name GroupName
	FROM OITM_ItemGroups 
	WHERE ISNULL(U_Grp4Name, '') NOT IN (SELECT GrpName FROM Group4)
		AND ISNULL(U_Grp4Name, '') <> ''
	) A
GO

UPDATE QtyConversionDetail SET SequenceNo = 1.0
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 1
GO

UPDATE QtyConversionDetail SET SequenceNo = 1.1
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 5
GO

UPDATE QtyConversionDetail SET SequenceNo = 1.2
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 4
GO

UPDATE QtyConversionDetail SET SequenceNo = 1.3
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 3
GO

UPDATE QtyConversionDetail SET Grp4No = 508
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 2 AND ISNULL(Grp4No, 0) IN (1)
GO

INSERT INTO QtyConversionDetail (Grp1No,Grp2No,Grp3No,Grp4No,FormulaNo,SequenceNo)
VALUES (1, 4, 2, 632, 3, 1.4)
GO

UPDATE QtyConversionDetail SET SequenceNo = 1.4
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 4 AND ISNULL(Grp3No, 0) = 2
GO


UPDATE QtyConversionDetail SET SequenceNo = 2.0
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 3 AND ISNULL(Grp3No, 0) = 8
GO

UPDATE QtyConversionDetail SET SequenceNo = 3.0
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 2 AND ISNULL(Grp3No, 0) = 6
GO

UPDATE QtyConversionDetail SET SequenceNo = 3.1
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 2 AND ISNULL(Grp3No, 0) = 10
GO

UPDATE QtyConversionDetail SET SequenceNo = 3.2
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 2 AND ISNULL(Grp3No, 0) = 9
GO

UPDATE QtyConversionDetail SET SequenceNo = 4.0
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 5 AND ISNULL(Grp3No, 0) IN (11)
GO

INSERT INTO QtyConversionDetail (Grp1No,Grp2No,Grp3No,Grp4No,FormulaNo,SequenceNo)
VALUES (1, 5, 148, NULL, NULL, 4.0)
GO

INSERT INTO QtyConversionDetail (Grp1No,Grp2No,Grp3No,Grp4No,FormulaNo,SequenceNo)
VALUES (1, 5, 274, NULL, NULL, 4.0)
GO

INSERT INTO QtyConversionDetail (Grp1No,Grp2No,Grp3No,Grp4No,FormulaNo,SequenceNo)
VALUES (1, 5, 52, NULL, NULL, 4.0)
GO

UPDATE QtyConversionDetail SET SequenceNo = 5.0
WHERE Grp1No = 1 AND ISNULL(Grp2No,0) = 6 AND ISNULL(Grp3No, 0) IN (7)
GO