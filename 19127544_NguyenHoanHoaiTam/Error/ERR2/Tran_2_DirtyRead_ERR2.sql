USE [QL_DH_GH_ONLINE]
GO
/*T2*/
CREATE OR ALTER PROCEDURE XEM_DS_MAHD @MAHD CHAR(5), @MADT CHAR(5)
AS
BEGIN TRAN
	SELECT * FROM HOPDONG WITH(NOLOCK) WHERE MAHD = @MAHD AND MADT = @MADT
COMMIT TRAN
GO
EXEC XEM_DS_MAHD 'HD001', 'DT240'