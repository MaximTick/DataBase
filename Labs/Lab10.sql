use Trucking
go

select * from sys.schemas;

CREATE SCHEMA [Lab10];
GO

create LOGIN Maxik with password = 'Maxik2020';
create user Maxik for login Maxik 
	with default_schema = Lab10

execute sp_addrolemember 'db_ddladmin', 'Maxik'

create login Vaska with password = 'Vaska';
create user Vaska for login Vaska
go

execute sp_addrolemember 'db_datareader', 'Maxik'
execute sp_addrolemember 'db_ddladmin', 'Maxik'

create table [Lab10].City(
id int primary key identity(1,1),
[name] nvarchar(50)
);
go
insert into Lab10.City values ('Минск'), ('Гродно'), ('Краков'), ('Варшава');

deny select on Lab10.City to Vaska;
go

create procedure Lab10.SelectFromCity
with execute as 'Maxik'
as 
Select * from Lab10.City
go

alter authorization on Lab10.SelectFromCity
to Maxik;
go

grant execute on Lab10.SelectFromCity to Vaska;

setuser 'Vaska'
go
select * from Lab10.City;
execute Lab10.SelectFromCity

use master
go

CREATE SERVER AUDIT Lab10Audit
TO FILE
(	FILEPATH = 'D:\Audit\',
	MAXSIZE = 0 MB,
	MAX_ROLLOVER_FILES = 0,
	RESERVE_DISK_SPACE = OFF
)
WITH ( QUEUE_DELAY = 1000, ON_FAILURE = CONTINUE);

CREATE SERVER AUDIT ClientAudit to APPLICATION_LOG;
CREATE SERVER AUDIT UserAudit to SECURITY_LOG;

select statement from fn_get_audit_file('D:\Audit\*', null, null);


CREATE SERVER AUDIT SPECIFICATION ServerAuditSpec
FOR SERVER AUDIT Lab10Audit
ADD (DATABASE_CHANGE_GROUP)
WITH (STATE = ON)
GO

ALTER SERVER AUDIT UserAudit WITH (STATE = OFF)
GO

ALTER SERVER AUDIT SPECIFICATION ServerAuditSpec
WITH (STATE = OFF)
GO

CREATE DATABASE Lab10
GO
DROP DATABASE Lab10
GO

--11
CREATE ASYMMETRIC KEY SampleKey
WITH Algorithm = RSA_2048
ENCRYPTION BY PASSWORD = 'MaxikRsa2048';

--12
DECLARE @plaintext NVARCHAR(21);
DECLARE @ciphertext VARBINARY(256);

SET @plaintext = 'Hello World!';
PRINT @plaintext;

SET @ciphertext = ENCRYPTBYASYMKEY(ASYMKEY_ID('SampleKey'), @plaintext);
PRINT @ciphertext;

SET @plaintext =DECRYPTBYASYMKEY(ASYMKEY_ID('SampleKey'), @ciphertext, N'MaxikRsa2048');
PRINT @plaintext;
--PRINT CAST(@plaintext as NVARCHAR(MAX))
GO

--13
USE MASTER
GO

CREATE CERTIFICATE Lab10Sert
ENCRYPTION BY PASSWORD = N'Maximka2020'
WITH SUBJECT = N'Sample Certificate',
EXPIRY_DATE = N'31/10/2022';

BACKUP CERTIFICATE Lab10Sert
TO FILE = N'D:\Audit\BackupSampleCert.cer'
WITH PRIVATE KEY (
FILE = N'D:\Audit\BackupSampleCert.pvk',
Encryption by password = N'Maximka2020',
DEcryption by password = N'Maximka2020');

--14
DECLARE @plain_text NVARCHAR(100);
SET @plain_text = 'Test Ecrypt CERTIFICATE';
PRINT @plain_text;

DECLARE @chiper_text VARBINARY(127);
SET @chiper_text = ENCRYPTBYCERT(CERT_ID('Lab10Sert'), @plain_text);
PRINT @chiper_text;

DECLARE @decrypt_text nvarchar(100)
SET @decrypt_text = cast (DECRYPTBYCERT(CERT_ID('Lab10Sert'), @chiper_text, N'Maximka2020') as nvarchar(100));
PRINT @decrypt_text;
GO

--15
CREATE SYMMETRIC KEY SKey
WITH ALGORITHM = AES_256 ENCRYPTION
BY PASsWORD = 'Maxik_AES_256';

Open SYMMETRIC KEY SKey
Decryption by password = 'Maxik_AES_256'

--16
CREATE SYMMETRIC KEY SData
WITH ALGORITHM = AES_256
ENCRYPTION BY SYMMETRIC KEY SKey;

OPEN SYMMETRIC KEY SData
DECRYPTION BY SYMMETRIC KEY SKey;

DECLARE @plain_text nvarchar(512);
SET @plain_text = 'open symmetric key with wich to encrypt the data';
PRINT @plain_text;

DECLARE @chiper_text varbinary(1024);
SET @chiper_text = ENCRYPTBYKEY(KEY_GUID(N'SData'), @plain_text); 
PRINT @chiper_text;

SET @plain_text = CAST(DECRYPTBYKEY(@chiper_text) as nvarchar(512));
PRINT @plain_text;

CLOSE SYMMETRIC KEY SData;
CLOSE SYMMETRIC KEY SKey;
GO

--17
USE master;
GO

CREATE MASTER KEY
ENCRYPTION BY PASSWORD = 'P@$$wOrd';
GO

CREATE CERTIFICATE AdvWorksCert
WITH SUBJECT = 'Certifcate to ENCRYPT MY db',
expiry_date = '2022-01-01';
GO

BACKUP CERTIFICATE AdvWorksCert
TO FILE = N'D:\Audit\BackupAdvWorksCert1.cer'
WITH PRIVATE KEY (
FILE = N'D:\Audit\BackupAdvWorksCert1.pvk',
Encryption by password = N'P@$$wOrd');

USE Trucking;
GO

CREATE DATABASE ENCRYPTION KEY
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE AdvWorksCert;
GO

ALTER DATABASE Trucking
SET ENCRYPTION ON;
GO

--18
SELECT HASHBYTES('SHA1', 'Open the symmetric key with wich to encrypt the data');

--19
SELECT SignByCert(CERT_ID('Lab10Sert'),'MaximTick', N'Maximka2020')
