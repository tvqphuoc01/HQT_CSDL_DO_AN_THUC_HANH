﻿USE [QL_DH_GH_ONLINE]
GO
/*LOST UPDATE*/
/* T2 */
CREATE OR ALTER PROCEDURE KH2_MUASP @MADH CHAR(5), @MASP CHAR(5), @SLMUA INT
AS
BEGIN TRAN
     DECLARE @SLTON INT
     SET @SLTON = (SELECT SLTON FROM SANPHAM WHERE MASP = @MASP)
	 IF (@SLTON = 0)
	 BEGIN
		PRINT N'Số lượng hàng trong kho đã hết'
		ROLLBACK TRAN
	 END
	 IF (@SLMUA > @SLTON)
	 BEGIN
		PRINT N'Số lượng hàng trong kho không đủ đáp ứng với số lượng mua'
		ROLLBACK TRAN
	 END
	 WAITFOR DELAY '00:00:05'
	 INSERT [dbo].[CTDONHANG] ([MASP], [MADH], [SLSANPHAM]) VALUES (@MASP, @MADH, @SLMUA)

	 SET @SLTON = @SLTON - @SLMUA
     UPDATE SANPHAM
     SET SLTON = @SLTON
     WHERE MASP = @MASP
COMMIT TRAN
GO
EXEC KH2_MUASP 'DH768', 'SP005', 5