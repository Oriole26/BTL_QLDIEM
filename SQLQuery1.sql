CREATE DATABASE QuanLyDiemTHCS_G09
USE QuanLyDiemTHCS_G09
GO
CREATE TABLE tblLopHoc(sMaLH VARCHAR(10) PRIMARY KEY, 
						sTenLH VARCHAR(10), 
						sMaKhoiLop VARCHAR(6),
						sMaNamHoc VARCHAR(6),
						iSiSo INT, 
						sMaGV VARCHAR(6)
						CONSTRAINT FK_tblLopHoc_tblKhoiLop FOREIGN KEY(sMaKhoiLop) REFERENCES tblKhoiLop(sMaKhoiLop),
						CONSTRAINT FK_tblLopHoc_tblNamHoc FOREIGN KEY(sMaNamHoc) REFERENCES tblNamHoc(sMaNamHoc),
						CONSTRAINT FK_tblLopHoc_tblGV FOREIGN KEY(sMaGV) REFERENCES tblGiaoVien(sMaGV)
						)
DROP TABLE tblLopHoc
INSERT INTO tblLopHoc VALUES('LOP611920', '6A1', 'KHOI6', 'NH1920', 35, 'GV06'),
							('LOP621920', '6A2', 'KHOI6', 'NH1920', 36, 'GV05'),
							('LOP631920', '6A3', 'KHOI6', 'NH1920', 34, 'GV04'),
							('LOP711920', '7A1', 'KHOI7', 'NH1920', 37, 'GV03'),
							('LOP721920', '7A2', 'KHOI7', 'NH1920', 38, 'GV02'),
							('LOP811920', '8A1', 'KHOI8', 'NH1920', 39, 'GV01'),
							('LOP612021', '6A1', 'KHOI6', 'NH2021', 39, 'GV01'),
							('LOP622021', '6A2', 'KHOI6', 'NH2021', 38, 'GV02'),
							('LOP632021', '6A3', 'KHOI6', 'NH2021', 35, 'GV03'),
							('LOP912021', '9A1', 'KHOI9', 'NH2021', 40, 'GV04'),
							('LOP922021', '9A2', 'KHOI9', 'NH2021', 38, 'GV05'),
							('LOP932021', '9A3', 'KHOI9', 'NH2021', 38, 'GV06')
GO
select *from tblLopHoc


/*Học sinh*/
CREATE TABLE tblHocSinh( sMaHS VARCHAR(10) PRIMARY KEY,
						sHoTenHS NVARCHAR(50),
						dNgaySinh DATETIME, 
						sGioiTinh NVARCHAR(10),
						sDiaChi NVARCHAR(50),
						sDanToc NVARCHAR(30),
						)
ALTER TABLE tblHocSinh
ADD sMaLH VARCHAR(10)
ALTER TABLE tblHocSinh
ADD CONSTRAINT FK_tblLopHoc_tblHocSinh FOREIGN KEY (sMaLH) REFERENCES tblLopHoc(sMaLH);

INSERT INTO tblHocSinh
VALUES
							('HS01',N'Nguyễn Thị Phương','3/29/2006',N'Nữ',N'Hoàn Kiếm',N'Kinh','LOP922021'),
							('HS02',N'Nguyễn Phương Nam','7/24/2006',N'Nam',N'Hà Đông', N'Kinh','LOP932021'),
							('HS03',N'Trần Quốc Việt','3/20/2007',N'Nam',N'Trương Định', N'Kinh','LOP811920'),
							('HS04',N'Chu Diệu Linh','7/14/2008',N'Nữ',N'Hoàng Mai', N'Kinh','LOP711920'),
							('HS05',N'Ngô Phương Anh','12/26/2008',N'Nữ',N'Tây Hồ', N'Kinh','LOP711920'),
							('HS06',N'Trần Thuý Quỳnh','10/2/2009',N'Nữ',N'Gia Lâm', N'Kinh','LOP611920')

DELETE FROM tblHocSinh
INSERT INTO tblHocSinh
VALUES
							('HS07',N'Nguyễn Thị P','3/29/2006',N'Nữ',N'Hoàn Kiếm',N'Kinh','LOP922021')

select * from tblHocSinh

/*Môn học*/
CREATE TABLE tblMonHoc( sMaMH VARCHAR(20) PRIMARY KEY,
						sTenMH NVARCHAR(30),
						iSoTiet INT
					  )
					  
INSERT INTO tblMonHoc
VALUES
		('SINHHOC',N'Sinh học', 45),
		('TOAN',N'Toán',40),
		('NGUVAN',N'Ngữ Văn', 60),
		('HOAHOC',N'Hoá học', 45),
		('VATLY',N'Vật Lý', 45),
		('MYTHUAT',N'Mỹ Thuật', 30),
		('AMNHAC',N'Âm nhạc', 30),
		('GDCD',N'GDCD', 40)

GO
DROP TABLE tblMonHoc
select*from tblMonHoc
/*Học kì*/
CREATE TABLE tblHocKy
(
	sMaHocKy VARCHAR(3) NOT NULL PRIMARY KEY,
	sTenHocKy NVARCHAR(30) NOT NULL,
	iHeSo INT,
	CONSTRAINT CK_HOCKY CHECK(CAST(RIGHT(sMaHocKy, 1) AS INT) BETWEEN 1 AND 3)
)

INSERT INTO tblHocKy VALUES('HK1', N'Học Kỳ 1', 1)
INSERT INTO tblHocKy VALUES('HK2', N'Học Kỳ 2', 2)

/*Năm học*/
CREATE TABLE tblNamHoc
(
	sMaNamHoc VARCHAR(6) NOT NULL PRIMARY KEY,
	sTenNamHoc NVARCHAR(30) NOT NULL
)

INSERT INTO tblNamHoc VALUES('NH1920', '2019-2020')
INSERT INTO tblNamHoc VALUES('NH2021', '2020-2021')

/*Bảng điểm*/
CREATE TABLE tblDiem (  
					   iSTT INT IDENTITY PRIMARY KEY,
					   sMaLH VARCHAR(10),
					   sMaHS VARCHAR(10), 
					   sMaNamHoc VARCHAR(6),
					   sMaHocKy VARCHAR(3),
					   sMaMH VARCHAR(20),
					   fDiemMieng FLOAT,
					   fDiem15P FLOAT,
					   fDiem45P FLOAT,
					   fDiemHocKy FLOAT,
						CONSTRAINT FK_tblLopHoc_tblMH FOREIGN KEY(sMaMH) REFERENCES tblMonHoc(sMaMH),
						CONSTRAINT FK_tblLopHoc_tblHS FOREIGN KEY(sMaHS) REFERENCES tblHocSinh(sMaHS),
						CONSTRAINT FK_tblLopHoc_tblLH FOREIGN KEY(sMaLH) REFERENCES tblLopHoc(sMaLH),
						CONSTRAINT CK_DIEM_MIENG CHECK(fDiemMieng BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_15P CHECK(fDiem15P BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_45P CHECK(fDiem45P BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_HK CHECK(fDiemHocKy BETWEEN 0 AND 10),
						
						) 
DROP TABLE tblDiem
INSERT INTO tblDiem
VALUES('LOP611920','HS01','NH1920','HK1','SINHHOC', 7.5, 6.5,8,7.5),
	  ('LOP621920','HS02','NH1920','HK1','SINHHOC', 7, 8,8,8)

	  Select*from tblDiem
CREATE TABLE tblKhoiLop (
							sMaKhoiLop VARCHAR(6) PRIMARY KEY,
							sTenKhoiLop NVARCHAR(30)
						)


INSERT INTO tblKhoiLop
VALUES
('KHOI6',N'Khối 6'),
('KHOI7',N'Khối 7'),
('KHOI8',N'Khối 8'),
('KHOI9',N'Khối 9')

CREATE TABLE tblGiaoVien (
							sMaGV VARCHAR(6) PRIMARY KEY,
							sTenGV NVARCHAR(30),
							sGioitinh NVARCHAR(10),
							sDiaChi NVARCHAR(50),
							sDienThoai NVARCHAR(12),
							sMaMH VARCHAR(20),
							CONSTRAINT FK_tblGiaoVien_tblMonHoc FOREIGN KEY(sMaMH) REFERENCES tblMonHoc(sMaMH)
						)
						
INSERT INTO tblGiaoVien VALUES
('GV01', N'Nguyễn Hoàng Trung', N'Nam',N'Long Biên', '0975058876', 'SINHHOC'),
('GV02', N'Phan Hồng Nhung', N'Nữ',N'Cầu Giấy', '0976630315', 'HOAHOC'),
('GV03', N'Huỳnh Thanh Trúc', N'Nữ',N'Bắc Từ Liêm', '0699015456', 'TOAN'),
('GV04', N'Lâm Trung Toàn', N'Nam',N'Hoàng Mai', '0845241566', 'NGUVAN'),
('GV05', N'Huỳnh Túc Trí',N'Nam', N'Đại Từ', '0123456789', 'GDCD'),
('GV06', N'Lê Thi Minh Nguyệt',N'Nữ', N'Ba Đình', '0123456789', 'MYTHUAT')

CREATE TABLE tblNguoiDung (
							iIDNguoiDung INT PRIMARY KEY,
							sTenNguoiDung NVARCHAR(30) UNIQUE, 
							sMatKhau VARCHAR(100)
							)
INSERT INTO tblNguoiDung
VALUES 
(1,'hoangoanh','oanh26022002'),
(2,'vananh26','va12345'),			
(3,'tranquann','quan2112'),			
(4,'dantien','tien000'),			
(5,'hoangoanh26','oanh12345')		

select * from tblNguoidung

							/*HỌC SINH*/

GO
/*Lấy thông tin học sinh*/
CREATE PROC tblHocsinh_Select 
AS
BEGIN
	SELECT * FROM tblHocSinh
END
GO
/*Lấy mã học sinh*/
CREATE PROCEDURE tblHocSinh_CheckMa
@mahs NVARCHAR(20)
AS
	BEGIN	
			SELECT COUNT(tblHocSinh.sMaHS) FROM tblHocSinh
		    WHERE tblHocSinh.sMaHS = @mahs
	END
GO
/*Thêm thông tin học sinh*/
ALTER PROCEDURE tblHocsinh_Insert
				@Mahs VARCHAR(6),
				@Tenhs NVARCHAR(50),
				@Ngaysinh DATETIME,
				@Gioitinh NVARCHAR(10),
				@Diachi NVARCHAR(50),
				@Dantoc NVARCHAR(30),
				@Malh VARCHAR(10)
AS
BEGIN
	INSERT INTO tblHocSinh
	VALUES 
	(	@Mahs ,@Tenhs ,@Ngaysinh ,@Gioitinh ,@Diachi ,@Dantoc,@Malh )

END
GO
/* Chỉnh sửa thông tin học sinh*/
ALTER PROC tblHocSinh_Update
@Mahs VARCHAR(6),
@Tenhs NVARCHAR(50),
@Ngaysinh DATETIME,
@Gioitinh NVARCHAR(10),
@Diachi NVARCHAR(50),
@Dantoc NVARCHAR(30),
@Malh VARCHAR(10)

AS
BEGIN
    UPDATE tblHocSinh 
	SET
   sHoTenHS = @Tenhs,
   dNgaySinh = @Ngaysinh,
   sGioiTinh = @Gioitinh,
   sDiaChi = @Diachi,
   sDanToc = @Dantoc,
   sMaLH = @Malh
   WHERE sMaHS = @MaHS
END
GO
CREATE PROC tblHocSinh_Xoa
@Mahs VARCHAR(6)
AS
BEGIN
   DELETE tblHocSinh where sMaHS = @Mahs
END
GO
/*Tìm kiếm học sinh theo tên*/
CREATE PROCEDURE tblHocSinh_Search
@tenhs NVARCHAR(50)
AS
	BEGIN
			SELECT * 
			FROM tblHocSinh
			 WHERE tblHocSinh.sHoTenHS LIKE N'%' + @tenhs + '%'
	END
GO
			/*MÔN HỌC*/
/*Lấy danh sách môn học*/
CREATE PROC tblMonhoc_Select 
AS
BEGIN
	SELECT * FROM tblMonHoc
END
GO
/*Check trùng môn học*/
CREATE PROCEDURE tblMonHoc_CheckMa
@mamh VARCHAR(10)
AS
	BEGIN	
			SELECT COUNT(tblMonHoc.sMaMH) FROM tblMonHoc
		    WHERE tblMonHoc.sMaMH = @mamh
	END
GO
/* Lấy ra mã môn học */
CREATE PROC tblMonhoc_Ma
AS
BEGIN
	SELECT sMaMH
	FROM tblMonHoc
END
GO
/*Thêm thông tin môn học*/
CREATE PROC tblMonhoc_Insert
@Mamh VARCHAR(6),
@Tenmh NVARCHAR(50), 
@Sotiet INT
AS 

BEGIN
	INSERT INTO tblMonHoc
	VALUES(@Mamh,@Tenmh,@Sotiet)
END
GO
/*Chỉnh sửa thông tin môn học*/
CREATE PROC tblMonHoc_Update
@mamh VARCHAR(6),
@tenmh NVARCHAR(50),
@Sotiet INT
AS
BEGIN
    UPDATE tblMonHoc
	SET
	sMaMH = @mamh,
	sTenMH = @tenmh,
	iSoTiet = @Sotiet
   WHERE sMaMH = @mamh
END
GO
/*Xoá môn học*/
CREATE PROC tblMonHoc_Xoa
@mamh VARCHAR(10)
AS
BEGIN
   DELETE tblMonHoc where sMaMH = @mamh
END
GO
			/*KHỐI LỚP*/
/*Lấy ra danh sách khối lớp*/
CREATE PROC tblKhoiLop_Select 
AS
BEGIN
	SELECT * FROM tblKhoiLop
END
GO
/*Lấy mã khối lớp để check trùng */
CREATE PROCEDURE tblKhoiLop_CheckMa
@makl VARCHAR(10)
AS
	BEGIN	
			SELECT COUNT(tblKhoiLop.sMaKhoiLop) FROM tblKhoiLop
		    WHERE tblKhoiLop.sMaKhoiLop = @makl
	END
GO
/*Lấy ra mã khối lớp*/
CREATE PROCEDURE tblKhoiLop_Ma
AS
BEGIN
	SELECT sMaKhoiLop
	FROM tblKhoiLop
END
GO
/*Thêm thông tin khối lớp*/
CREATE PROC tblKhoiLop_Insert
@Makl VARCHAR(6),
@Tenkl NVARCHAR(30)

AS 

BEGIN
	INSERT INTO tblKhoiLop
	VALUES(@Makl,@Tenkl)
END
GO
/*Chỉnh sửa thông tin khối lớp*/
CREATE PROC tblKhoiLop_Update
@makl VARCHAR(6),
@tenkl NVARCHAR(30)
AS
BEGIN
    UPDATE tblKhoiLop
	SET
	sMaKhoiLop = @makl,
	sTenKhoiLop = @tenkl
   WHERE sMaKhoiLop = @makl
END
GO
/*Xoá khối lớp*/
CREATE PROC tblKhoiLop_Xoa
@makl VARCHAR(10)
AS
BEGIN
   DELETE tblKhoiLop where sMaKhoiLop = @makl
END
GO

		/*GIÁO VIÊN*/
/*Lấy thông tin giáo viên*/
CREATE PROC tblGV_Select 
AS
BEGIN
	SELECT * FROM tblGiaoVien
END
GO
/*Lấy mã giáo viên check trùng*/
CREATE PROCEDURE tblGiaoVien_CheckMa
@magv VARCHAR(6)
AS
	BEGIN	
			SELECT COUNT(tblGiaoVien.sMaGV) FROM tblGiaoVien
		    WHERE tblGiaoVien.sMaGV = @magv
	END
GO
/*Lấy ra mã giáo viên*/
CREATE PROCEDURE tblGiaoVien_Ma
AS
BEGIN
	SELECT sMaGV
	FROM tblGiaoVien
END
GO
/*Thêm thông tin giáo viên*/
CREATE PROCEDURE tblGV_Insert
				@Magv VARCHAR(6),
				@Tengv NVARCHAR(50),
				@Gioitinh NVARCHAR(10),
				@Diachi NVARCHAR(50),
				@Sdt VARCHAR(30),
				@mamh VARCHAR(6)
AS
BEGIN
	INSERT INTO tblGiaoVien
	VALUES 
	(	@Magv ,@Tengv ,@Gioitinh ,@Diachi ,@Sdt,@mamh )

END
GO
/* Chỉnh sửa thông tin giáo viên*/
CREATE PROC tblGV_Update
@Magv VARCHAR(6),
@Tengv NVARCHAR(50),
@Gioitinh NVARCHAR(10),
@Diachi NVARCHAR(50),
@Sdt VARCHAR(30),
@mamh VARCHAR(6)

AS
BEGIN
    UPDATE tblGiaoVien
	SET
   sTenGV = @Tengv,
   sGioiTinh = @Gioitinh,
   sDiaChi = @Diachi,
   sDienThoai = @Sdt,
   sMaMH = @mamh
   WHERE sMaGV = @Magv
END
GO
CREATE PROC tblGV_Xoa
@Magv VARCHAR(6)
AS
BEGIN
   DELETE tblGiaoVien where sMaGV = @Magv
END
GO
/*Tìm kiếm giáo viên */
CREATE PROCEDURE tblGiaoVien_Search
@tengv NVARCHAR(50)
AS
	BEGIN
			SELECT * 
			FROM tblGiaoVien
			 WHERE tblGiaoVien.sTenGV LIKE N'%' + @tengv + '%'
	END
GO

	/* LỚP HỌC */
	/*Lấy danh sách lớp học*/
CREATE PROC tblLopHoc_Select 
AS
BEGIN
	SELECT * FROM tblLopHoc
END
GO
/*Lấy mã lớp học*/
CREATE PROCEDURE tblLopHoc_CheckMa
@malh VARCHAR(10)
AS
	BEGIN	
			SELECT COUNT(tblLopHoc.sMaLH) FROM tblLopHoc
		    WHERE tblLopHoc.sMaLH = @malh
	END
GO
/*Thêm thông tin lớp học*/
ALTER PROC tblLopHoc_Insert
@Malh VARCHAR(10),
@Tenlh NVARCHAR(50), 
@Makl VARCHAR(6),
@Manh NVARCHAR(20),
@Siso INT,
@Magv VARCHAR(10)
AS 

BEGIN
	INSERT INTO tblLopHoc
	VALUES(@Malh ,@Tenlh , @Makl ,@Manh ,@Siso ,@Magv )
END
GO
/*Chỉnh sửa thông tin môn học*/
CREATE PROC tblLopHoc_Update
@malh VARCHAR(10),
@tenlh NVARCHAR(50),
@Makl VARCHAR(10),
@Manh VARCHAR(30),
@Siso INT,
@Magv VARCHAR(6)
AS
BEGIN
    UPDATE tblLopHoc
	SET
	sMaLH = @malh,
	sTenLH = @tenlh,
	sMaKhoiLop = @Makl,
	sMaNamHoc = @Manh,
	iSiSo = @Siso,
	sMaGV = @Magv
   WHERE sMaLH = @malh
END
GO
/*Xoá môn học*/
CREATE PROC tblLopHoc_Xoa
@malh VARCHAR(10)
AS
BEGIN
   DELETE tblLopHoc where sMaLH = @malh
END
GO

--Proc lấy năm học
GO
	ALTER PROC tblNamHoc_Select
	AS
	BEGIN
			SELECT * FROM tblNamHoc
	END
GO

-- Lấy ra mã năm học
GO
	CREATE PROC tblNamHoc_SelectMa
	AS
	BEGIN
		SELECT sMaNamHoc
		FROM tblNamHoc
	END
GO

-- Proc check ma
GO
	ALTER PROC tblNamHoc_CheckMa
	@smaNH VARCHAR(6)
	AS
	BEGIN
			SELECT COUNT(tblNamHoc.sMaNamHoc) FROM tblNamHoc
				WHERE tblNamHoc.sMaNamHoc = @smaNH
	END
GO

--PROC thêm năm học
GO
	ALTER PROC tblNamhoc_Insert
	@smaNH VARCHAR(6),
	@sTenNH NVARCHAR(30)
	AS
	BEGIN
		INSERT INTO tblNamHoc
		VALUES(@smaNH,@sTenNH )
	END
GO

-- Proc xoá năm học
GO
	ALTER PROC tblNamhoc_Delete
	@smaNH VARCHAR(6)
	AS
	BEGIN
		DELETE tblNamHoc
		WHERE sMaNamHoc = @smaNH
	END
GO

--Proc update năm học
GO
	ALTER PROC tblNamhoc_Update
	@smaNH VARCHAR(6),
	@sTenNH NVARCHAR(30)
	AS
	BEGIN
		UPDATE tblNamHoc
		SET sMaNamHoc = @smaNH,
			sTenNamHoc = @sTenNH
		WHERE sMaNamHoc = @smaNH
	END
GO


				/*ĐIỂM*/

/*Lấy ra danh sách điểm*/

CREATE PROC tblDiem_Select
AS
BEGIN
	SELECT * FROM tblDiem
END
GO

-- Thêm thông tin điểm
CREATE PROC tblDiem_Insert
@smaLH VARCHAR(10),
@smaHS VARCHAR(10), 
@smaNamHoc VARCHAR(6),
@smaHocKy VARCHAR(3),
@smaMH VARCHAR(20),
@fdiemMieng FLOAT,
@fdiem15P FLOAT,
@fdiem45P FLOAT,
@fdiemHocKy FLOAT
AS
BEGIN
	INSERT INTO tblDiem
	VALUES(@smaLH ,@smaHS ,@smaNamHoc ,@smaHocKy ,@smaMH ,@fdiemMieng ,@fdiem15P ,@fdiem45P ,@fdiemHocKy )
END
GO
-- Lay ra ma va ten lop
CREATE PROC tblLopHoc_SelectMa_Ten
AS
BEGIN
	SELECT sMaLH,sTenLH
	FROM tblLopHoc
END
GO
-- Lấy ra mã và tên môn học
CREATE PROC tblMonHoc_SelectMa_Ten
AS
BEGIN 
	SELECT sMaMH,sTenMH
	FROM tblMonHoc
END
GO
--Lấy ra danh sách năm học,học kì
CREATE  PROC select_diem_NHHK
	@maNH VARCHAR(6),
	@maHK VARCHAR(3),
	@maMH VARCHAR(20),
	@maHS VARCHAR(10)
	AS
	BEGIN
		SELECT *
		FROM tblDiem
		WHERE sMaHS = @maHS AND sMaNamHoc = @maNH AND sMaHocKy = @maHK AND sMaMH = @maMH
	END
GO
--Lấy thông tin học kỳ
CREATE   PROC select_all_hk
AS
BEGIN
	SELECT *
	FROM tblHocKy
END
GO

--Lấy tất cả tên học kỳ
GO
	ALTER PROC tblHocKy_SelectTen
	AS
	BEGIN
		SELECT sTenHocKy
		FROM tblHocKy
	END
GO

--Lấy thông tin năm học
CREATE PROC select_all_nh
AS
BEGIN
	SELECT *
	FROM tblNamHoc
END
GO


-- Lay ra ma va ten HS theo maLH
CREATE  PROC hs_by_maLH 
@maLH VARCHAR(10)
AS
BEGIN
	SELECT hs.sMaHS, hs.sHoTenHS, d.sMaNamHoc, d.sMaHocKy, d.sMaMH, d.fDiemMieng, d.fDiem15P, d.fDiem45p, d.fDiemHocKy
	FROM tblHocSinh AS hs
	LEFT JOIN tblDiem AS d
	ON  d.sMaHS =	hs.sMaHS 
	where hs.sMaLH = @maLH
	SELECT sMaHS,sHoTenHS
	FROM tblHocSinh
	WHERE sMaLH = @maLH
END
GO
--Sửa điểm
CREATE PROC editDiem
	@maNH VARCHAR(6),
	@maHK VARCHAR(3),
	@maMH VARCHAR(20),
	@maHS VARCHAR(10),
	@diemM FLOAT,
	@diem15 FLOAT,
	@diem45 FLOAT,
	@diemHK FLOAT
	AS
	BEGIN
		UPDATE tblDiem
		SET fDiemMieng = @diemM, fDiem15P = @diem15, fDiem45P = @diem15, fDiemHocKy = @diemHK
		WHERE sMaHS = @maHS AND sMaNamHoc = @maNH AND sMaHocKy = @maHK AND sMaMH = @maMH
	END

	SELECT * FROM tblDiem
GO

--Xoá điểm

CREATE  PROC deleteDiem
@maNH VARCHAR(6),
@maHK VARCHAR(3),
@maMH VARCHAR(20),
@maHS VARCHAR(10)
AS
BEGIN
	DELETE
	FROM tblDiem
	WHERE sMaHS = @maHS AND sMaNamHoc = @maNH AND sMaHocKy = @maHK AND sMaMH = @maMH
END
GO

			-- ĐĂNG NHẬP --
-- Check đăng nhập
CREATE PROCEDURE pr_CheckTK
@stennd NVARCHAR(100)
AS
BEGIN
    -- Kiểm tra tài khoản trong bảng tblNguoiDung
    SELECT COUNT(sTenNguoiDung) 
    FROM tblNguoiDung
    WHERE tblNguoiDung.sTenNguoiDung = @stennd 
END
GO
CREATE PROCEDURE pr_CheckMK
@smatkhau VARCHAR(200)
AS
BEGIN
    -- Kiểm tra tài khoản trong bảng tblUsers
    SELECT COUNT(sMatKhau) 
    FROM tblNguoiDung
    WHERE tblNguoiDung.sMatKhau = @smatkhau
END
 
								