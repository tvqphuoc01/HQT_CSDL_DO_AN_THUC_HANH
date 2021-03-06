USE [QL_DH_GH_ONLINE]
GO
/* DIRTY READ */
/* T1 */
CREATE OR ALTER PROCEDURE DT_CAPNHAT_GIASP_FIX @MASP CHAR(5), @GIA_UPDATE MONEY
AS
BEGIN TRAN
	DECLARE @GIASP MONEY
	SET @GIASP = (SELECT DONGIA FROM SANPHAM WHERE MASP = @MASP)
	UPDATE SANPHAM
    SET DONGIA = @GIA_UPDATE  
	WHERE MASP = @MASP
	WAITFOR DELAY '00:00:05'

	IF (@GIA_UPDATE >= 3 * @GIASP)
	BEGIN
		ROLLBACK TRAN
		RETURN
	END
COMMIT TRAN
GO
EXEC DT_CAPNHAT_GIASP_FIX 'SP006', 3000000000