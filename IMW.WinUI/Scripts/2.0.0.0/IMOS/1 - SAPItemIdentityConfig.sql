DROP TABLE IF EXISTS dbo.SAPItemIdentityConfig
GO

CREATE TABLE SAPItemIdentityConfig
(
	SerialNo INT IDENTITY(1, 1) PRIMARY KEY,
	ProductCode VARCHAR(50)
)
GO

INSERT INTO SAPItemIdentityConfig
SELECT 'FG01'
GO