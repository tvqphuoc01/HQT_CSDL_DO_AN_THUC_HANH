﻿USE [QL_DH_GH_ONLINE]
GO
/*LOST UPDATE*/
/* T2 */
CREATE OR ALTER PROCEDURE KH2_MUASP_FIX @MADH CHAR(5), @MASP CHAR(5), @SLMUA INT
AS
BEGIN TRAN
	 SET TRAN ISOLATION LEVEL REPEATABLE READ
     DECLARE @SLTON INT
     SELECT @SLTON = SLTON FROM SANPHAM WITH(UPDLOCK) WHERE MASP = @MASP
	 PRINT @SLTON
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

     UPDATE SANPHAM 
     SET SLTON = @SLTON - @SLMUA
     WHERE MASP = @MASP

COMMIT TRAN
GO
EXEC KH2_MUASP_FIX 'DH763', 'SP005', 5