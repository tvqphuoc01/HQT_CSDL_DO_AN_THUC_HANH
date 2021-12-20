USE [QL_DH_GH_ONLINE]
GO
CREATE OR ALTER PROCEDURE DT_CAPNHATSL @MASP CHAR(5), @SL INT
AS
BEGIN TRAN
	SELECT MASP, SLTON 
	FROM SANPHAM 
	WHERE MASP = @MASP
	WAITFOR DELAY '00:00:05'

	UPDATE SANPHAM
	SET SLTON = @SL
	WHERE MASP = @MASP
COMMIT TRAN
GO
EXEC DT_CAPNHATSL 'SP001', 49