DROP TABLE IF EXISTS AppSettings
GO

CREATE TABLE AppSettings
(
	SettingID	INT PRIMARY KEY,
	Tag			VARCHAR(250) NOT NULL,
	TagValue	VARCHAR(250) NULL
)
GO

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (1, 'CompanyDB', 'TEST22')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (2, 'Server', 'hanadb.iwm.com.pk:30015')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (3, 'LicenseServer', 'sapapp.iwm.com.pk:40000')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (4, 'SLDServer', 'sapapp.iwm.com.pk:40000')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (5, 'DbUserName', 'CORVIT')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (6, 'DbPassword', '!Wm@44222')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (7, 'UserName', 'corvit')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (8, 'Password', '!Wm4422')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (9, 'DbServerType', 'dst_HANADB')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (10, 'UseTrusted', 'True')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (11, 'TSI', '60')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (12, 'Sync', '0|0|0|0|0|0|0|0')

INSERT INTO AppSettings (SettingID, Tag, TagValue)
VALUES (13, 'BoMLastAutoID', '0')
GO