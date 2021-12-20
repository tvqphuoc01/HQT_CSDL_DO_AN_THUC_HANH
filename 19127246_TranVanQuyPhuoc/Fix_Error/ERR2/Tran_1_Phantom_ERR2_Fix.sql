﻿USE [QL_DH_GH_ONLINE]
GO
/*PHANTOM - 2*/
/* T1 */
CREATE OR ALTER PROCEDURE XEM_DS_LOGIN_FIX
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL SERIALIZABLE
	DECLARE @COUNT INT
	SET @COUNT = 0
	SET @COUNT = (SELECT COUNT(*) FROM LOGIN)
	WAITFOR DELAY '00:00:05'
	
	IF ((SELECT COUNT(*) FROM LOGIN) <> @COUNT)
	BEGIN
		RAISERROR(N'Số lượng dữ liệu hai lần đọc khác nhau',16,1)
	END
	SELECT * FROM LOGIN
COMMIT TRAN
GO
EXEC XEM_DS_LOGIN_FIX