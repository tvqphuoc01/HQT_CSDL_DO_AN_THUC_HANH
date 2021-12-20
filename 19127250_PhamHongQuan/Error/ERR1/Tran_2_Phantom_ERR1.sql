﻿USE [QL_DH_GH_ONLINE]
GO
/*PHANTOM - 2*/
/* T2 */
CREATE OR ALTER PROCEDURE THEM_HD @MADT CHAR(5), @MACN CHAR(5), @NGAYKY DATE, @NGAYKT DATE, @PTHOAHONG FLOAT
AS
BEGIN TRAN
	INSERT INTO HOPDONG(MADT,MACN,NGAYKY,NGAYKT,PHANTRAMHOAHONG, TINHTRANGHD)
	VALUES (@MADT,@MACN,@NGAYKY,@NGAYKT,@PTHOAHONG, N'Đang chờ duyệt')

	IF (@NGAYKY > @NGAYKT)
	BEGIN
		PRINT N'NGÀY KÝ VÀ NGÀY KT KHÔNG HỢP LỆ'
		ROLLBACK TRAN
	END
COMMIT TRAN
GO
EXEC THEM_HD 'DT001', 'CN240', '2021-12-10', '2022-11-13', 0