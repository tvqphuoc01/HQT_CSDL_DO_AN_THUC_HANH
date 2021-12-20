﻿USE [QL_DH_GH_ONLINE]
GO
/* UNREPEATABLE READ */
/* T1 */
CREATE OR ALTER PROCEDURE TIM_NV_CN_FIX @MACN CHAR(5)
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL REPEATABLE READ
	DECLARE @COUNT INT
	SET @COUNT = 0
	SET @COUNT = (SELECT COUNT(*) FROM NHANVIEN WHERE MACN = @MACN)
	WAITFOR DELAY '00:00:05'

	IF ((SELECT COUNT(*) FROM NHANVIEN WHERE MACN = @MACN) <> @COUNT)
	BEGIN
		RAISERROR(N'Số lượng dữ liệu hai lần đọc khác nhau',16,1)
	END
	SELECT *
	FROM NHANVIEN 
	WHERE MACN = @MACN
COMMIT TRAN
GO
EXEC TIM_NV_CN_FIX 'CN270'