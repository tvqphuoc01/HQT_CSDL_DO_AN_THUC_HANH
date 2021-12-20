USE [QL_DH_GH_ONLINE]
GO
---- CREATE 5 LOGIN ------
EXEC sp_addLogin 'LG_DoiTac','DoiTac'
EXEC sp_addLogin 'LG_KhachHang','KhachHang'
EXEC sp_addLogin 'LG_NhanVien','NhanVien'
EXEC sp_addLogin 'LG_TaiXe','TaiXe'
EXEC sp_addLogin 'LG_Admin','Admin'

---- CREATE USER ---------
CREATE USER userDT FOR LOGIN LG_DoiTac
CREATE USER userKH FOR LOGIN LG_KhachHang
CREATE USER userNV FOR LOGIN LG_NhanVien
CREATE USER userTX FOR LOGIN LG_TaiXe
CREATE USER userAd FOR LOGIN LG_Admin

------ ADD ROLE ----------
EXEC sp_addrole 'DoiTac'
EXEC sp_addrole 'KhachHang'
EXEC sp_addrole 'TaiXe' 
EXEC sp_addrole 'NhanVien'
EXEC sp_addrole 'Admin'

EXEC sp_addrolemember 'DoiTac', 'userDT'
EXEC sp_addrolemember 'KhachHang', 'userKH'
EXEC sp_addrolemember 'TaiXe', 'userTX'
EXEC sp_addrolemember 'NhanVien', 'userNV'
EXEC sp_addrolemember 'Admin', 'userAd'


------ GRANT FOR DOITAC -------
GRANT SELECT, INSERT, DELETE, UPDATE ON SANPHAM TO DoiTac
GRANT INSERT, UPDATE ON HOPDONG TO DoiTac
GRANT INSERT, DELETE, UPDATE ON CHINHANH TO DoiTac
GRANT SELECT ON DONHANG TO DoiTac
GRANT SELECT ON CTDONHANG TO DoiTac
GRANT UPDATE ON DONHANG(TTDONHANG) TO DoiTac

------ GRANT FOR KHACHHANG -------
GRANT INSERT, UPDATE ON KHACHHANG TO KhachHang
GRANT SELECT ON DOITAC TO KhachHang
GRANT SELECT ON SANPHAM TO KhachHang
GRANT SELECT ON DONHANG TO KhachHang
GRANT SELECT ON DONGIAOHANG TO KhachHang

------- GRANT FOR TAIXE ---------
GRANT INSERT, UPDATE ON TAIXE TO TaiXe
GRANT UPDATE ON DONGIAOHANG(TTGIAOHANG) TO TaiXe
GRANT SELECT ON DONHANG TO TaiXe
GRANT SELECT ON DONGIAOHANG TO TaiXe

------- GRANT FOR NHANVIEN ---------
GRANT SELECT ON HOPDONG TO NhanVien
GRANT UPDATE ON HOPDONG(TINHTRANGHD) TO NhanVien
GRANT UPDATE ON HOPDONG(PHANTRAMHOAHONG) TO NhanVien

------- GRANT FOR ADMIN --------
EXEC sp_addsrvrolemember 'Admin', 'sysadmin';