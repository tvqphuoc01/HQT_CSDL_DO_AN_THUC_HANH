﻿USE [QL_DH_GH_ONLINE]
GO
/*PHANTOM*/
/* T1 */
CREATE OR ALTER PROCEDURE XEM_DS_HD_FIX @MACN CHAR(5)
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL SERIALIZABLE
	DECLARE @COUNT INT
	SET @COUNT = 0
	SET @COUNT = (SELECT COUNT(*) FROM HOPDONG WHERE MACN = @MACN)
	WAITFOR DELAY '00:00:05'

	IF ((SELECT COUNT(*) FROM HOPDONG WHERE MACN = @MACN) <> @COUNT)
	BEGIN
		RAISERROR(N'Số lượng dữ liệu hai lần đọc khác nhau',16,1)
	END
	SELECT * FROM HOPDONG WHERE MACN = @MACN
COMMIT TRAN
GO
EXEC XEM_DS_HD_FIX 'CN046'