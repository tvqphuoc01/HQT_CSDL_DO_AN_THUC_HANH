﻿USE [QL_DH_GH_ONLINE]
GO
/*CYCLE DEADLOCK*/
/* T2 */
CREATE OR ALTER PROCEDURE QL2_DEADLOCK_FIX @USERID CHAR(5), @PASS_NEW CHAR(10), @MATX CHAR(5), @CMND_NEW CHAR(10)
AS
BEGIN TRAN
	UPDATE LOGIN 
	SET PASSWORD = @PASS_NEW 
	WHERE USERID = @USERID
	WAITFOR DELAY '00:00:05'

	UPDATE TAIXE
	SET CMND = @CMND_NEW 
	WHERE MATX = @MATX
COMMIT TRAN
GO
/*Demo deadlock*/
EXEC QL2_DEADLOCK_FIX 'KH001', '123', 'TX001', '2132123339'