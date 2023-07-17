CREATE DATABASE db_QuanLyDiemTHPT
USE db_QuanLyDiemTHPT
GO

--HỌC KỲ
CREATE TABLE tblHocKy
					(
					sMaHocKy VARCHAR(3) NOT NULL PRIMARY KEY,
					sTenHocKy NVARCHAR(30) NOT NULL,
					CONSTRAINT CK_HOCKY CHECK(CAST(RIGHT(sMaHocKy, 1) AS INT) BETWEEN 1 AND 3)
					)

INSERT INTO tblHocKy VALUES('HK1', N'Học Kỳ 1')
INSERT INTO tblHocKy VALUES('HK2', N'Học Kỳ 2')

--NĂM HỌC
CREATE TABLE tblNamHoc
					(
					sMaNamHoc VARCHAR(6) NOT NULL PRIMARY KEY,
					sTenNamHoc NVARCHAR(30) NOT NULL
					)

INSERT INTO tblNamHoc VALUES('NH1920', '2019-2020')
INSERT INTO tblNamHoc VALUES('NH2021', '2020-2021')

--KHỐI LỚP
CREATE TABLE tblKhoiLop (
						sMaKhoiLop VARCHAR(6) PRIMARY KEY,
						sTenKhoiLop NVARCHAR(30)
						)


INSERT INTO tblKhoiLop VALUES
						('KHOI10',N'Khối 10'),
						('KHOI11',N'Khối 11'),
						('KHOI12',N'Khối 12')
--GIÁO VIÊN
CREATE TABLE tblGiaoVien (
						sMaGV VARCHAR(6) PRIMARY KEY,
						sTenGV NVARCHAR(30),
						sGioitinh NVARCHAR(10),
						sDiaChi NVARCHAR(50),
						sDienThoai NVARCHAR(12),
						)
				
INSERT INTO tblGiaoVien VALUES
						('GV01', N'Nguyễn Hoàng Trung', N'Nam',N'Long Biên', '0975058876'),
						('GV02', N'Phan Hồng Nhung', N'Nữ',N'Cầu Giấy', '0976630315'),
						('GV03', N'Huỳnh Thanh Trúc', N'Nữ',N'Bắc Từ Liêm', '0699015456'),
						('GV04', N'Lâm Trung Toàn', N'Nam',N'Hoàng Mai', '0845241566'),
						('GV05', N'Huỳnh Túc Trí',N'Nam', N'Đại Từ', '0123456789'),
						('GV06', N'Lê Thi Minh Nguyệt',N'Nữ', N'Ba Đình', '0123456789')

GO

--MÔN HỌC
CREATE TABLE tblMonHoc( 
						sMaMH VARCHAR(20) PRIMARY KEY,
						sTenMH NVARCHAR(30),
						iSoTiet INT,
						sMaGV VARCHAR(6)
						CONSTRAINT FK_tblMonHoc_tblGiaoVien FOREIGN KEY(sMaGV) REFERENCES tblGiaoVien(sMaGV)
					  )

INSERT INTO tblMonHoc VALUES
						('SINHHOC',N'Sinh học', 45,'GV01'),
						('TOAN',N'Toán',40,'GV02'),
						('NGUVAN',N'Ngữ Văn', 60,'GV03'),
						('HOAHOC',N'Hoá học', 45,'GV04'),
						('VATLY',N'Vật Lý', 45,'GV05'),
						('MYTHUAT',N'Mỹ Thuật', 30,'GV06'),
						('AMNHAC',N'Âm nhạc', 30,'GV02'),
						('GDCD',N'GDCD', 40,'GV01')
--LỚP HỌC
CREATE TABLE tblLopHoc( 
						sMaLH VARCHAR(10) PRIMARY KEY, 
						sTenLH VARCHAR(10), 
						sMaKhoiLop VARCHAR(6),
						iSiSo INT, 
						sMaGV VARCHAR(6),
						CONSTRAINT FK_tblLopHoc_tblKhoiLop FOREIGN KEY(sMaKhoiLop) REFERENCES tblKhoiLop(sMaKhoiLop),
						CONSTRAINT FK_tblLopHoc_tblGV FOREIGN KEY(sMaGV) REFERENCES tblGiaoVien(sMaGV),
						)
INSERT INTO tblLopHoc VALUES('LOP1011920', '10A1', 'KHOI10',35, 'GV06'),
							('LOP1021920', '10A2', 'KHOI10', 36, 'GV05'),
							('LOP1031920', '10A3', 'KHOI10', 34, 'GV04'),
							('LOP1111920', '11A1', 'KHOI11', 37, 'GV03'),
							('LOP1121920', '11A2', 'KHOI11', 38, 'GV02'),
							('LOP1211920', '12A1', 'KHOI12', 39, 'GV01'),
							('LOP1012021', '10A1', 'KHOI10', 39, 'GV01'),
							('LOP1022021', '10A2', 'KHOI10', 38, 'GV02'),
							('LOP1032021', '10A3', 'KHOI10', 35, 'GV03')				
GO

--HỌC SINH
CREATE TABLE tblHocSinh( 
						sMaHS VARCHAR(10) PRIMARY KEY,
						sHoTenHS NVARCHAR(50),
						dNgaySinh DATETIME, 
						sGioiTinh NVARCHAR(10),
						sDiaChi NVARCHAR(50),
						sDanToc NVARCHAR(30),
						sMaLH VARCHAR(10)
						CONSTRAINT FK_tblLopHoc_tblHocSinh FOREIGN KEY (sMaLH) REFERENCES tblLopHoc(sMaLH)
						)

INSERT INTO tblHocSinh VALUES
							('HS01',N'Nguyễn Thị Phương','3/29/2006',N'Nữ',N'Hoàn Kiếm',N'Kinh','LOP1111920'),
							('HS02',N'Nguyễn Phương Nam','7/24/2006',N'Nam',N'Hà Đông', N'Kinh','LOP1111920'),
							('HS03',N'Trần Quốc Việt','3/20/2007',N'Nam',N'Trương Định', N'Kinh','LOP1111920'),
							('HS04',N'Chu Diệu Linh','7/14/2008',N'Nữ',N'Hoàng Mai', N'Kinh','LOP1111920'),
							('HS05',N'Ngô Phương Anh','12/26/2008',N'Nữ',N'Tây Hồ', N'Kinh','LOP1111920'),
							('HS06',N'Trần Thuý Quỳnh','10/2/2009',N'Nữ',N'Gia Lâm', N'Kinh','LOP1111920')

--BẢNG ĐIỂM
CREATE TABLE tblDiem (  
						sMaLH VARCHAR(10),
						sMaHS VARCHAR(10), 
						sMaNamHoc VARCHAR(6),
						sMaHocKy VARCHAR(3),
						sMaMH VARCHAR(20),
						fDiemMieng FLOAT,
						fDiem15P FLOAT,
						fDiem45P FLOAT,
						fDiemHocKy FLOAT,
						fDiemTB FLOAT,
						CONSTRAINT FK_tblDiem_tblLH FOREIGN KEY(sMaLH) REFERENCES tblLopHoc(sMaLH),
						CONSTRAINT FK_tblDiem_tblMH FOREIGN KEY(sMaMH) REFERENCES tblMonHoc(sMaMH),
						CONSTRAINT FK_tblDiem_tblHS FOREIGN KEY(sMaHS) REFERENCES tblHocSinh(sMaHS),
						CONSTRAINT FK_tblDiem_tblNH FOREIGN KEY(sMaNamHoc) REFERENCES tblNamHoc(sMaNamHoc),
						CONSTRAINT FK_tblDiem_tblHK FOREIGN KEY(sMaHocKy) REFERENCES tblHocKy(sMaHocKy),
						CONSTRAINT CK_DIEM_MIENG CHECK(fDiemMieng BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_15P CHECK(fDiem15P BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_45P CHECK(fDiem45P BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_HK CHECK(fDiemHocKy BETWEEN 0 AND 10),
						CONSTRAINT CK_DIEM_TB CHECK(fDiemHocKy BETWEEN 0 AND 10)
						) 
INSERT INTO tblDiem VALUES
					('LOP1011920','HS01','NH1920','HK1','SINHHOC', 7.5, 6.5,8,7.5),
					('LOP1011920','HS02','NH1920','HK1','SINHHOC', 7, 8,8,8)

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
GO	
						--HỌC SINH--

-- Lấy ra thông tin học sinh
CREATE PROC prSelect_HS
AS
BEGIN
	SELECT * FROM tblHocSinh
END
GO

-- Lấy ra thông tin học sinh theo lớp học
CREATE PROC prSelectHSByMaLop
	@malh VARCHAR(10)
AS
BEGIN
	SELECT * 
	FROM tblHocSinh
	WHERE sMaLH = @malh
END
GO

-- Check trùng thông qua mã học sinh
CREATE PROCEDURE prChecktrung_MaHS
@smahs VARCHAR(10)
AS
BEGIN	
		SELECT COUNT(tblHocSinh.sMaHS) FROM tblHocSinh
		WHERE tblHocSinh.sMaHS = @smahs
END
GO
-- Thêm thông tin học sinh mới

CREATE PROCEDURE prInsert_HS
@smahs VARCHAR(10),
@stenhs NVARCHAR(50),
@dns DATETIME,
@sgioitinh NVARCHAR(10),
@sdiachi NVARCHAR(50),
@sdantoc NVARCHAR(30),
@smalh VARCHAR(10)
AS
BEGIN
	INSERT INTO tblHocSinh
	VALUES(@smahs,@stenhs,@dns,@sgioitinh,@sdiachi,@sdantoc,@smalh)

END
GO
-- Chỉnh sửa thông tin học sinh

CREATE PROC prUpdate_HS
@smahs VARCHAR(10),
@stenhs NVARCHAR(50),
@dns DATETIME,
@sgioitinh NVARCHAR(10),
@sdiachi NVARCHAR(50),
@sdantoc NVARCHAR(30),
@smalh VARCHAR(10)

AS
BEGIN
    UPDATE tblHocSinh 
	SET
   sHoTenHS = @stenhs,
   dNgaySinh = @dns,
   sGioiTinh = @sgioitinh,
   sDiaChi = @sdiachi,
   sDanToc = @sdantoc,
   sMaLH = @smalh
   WHERE sMaHS = @smahs
END
GO

-- Xoá học sinh

CREATE PROC prDelete_HS
@smahs VARCHAR(10)
AS
BEGIN
   DELETE tblHocSinh 
   WHERE sMaHS = @smahs
END
GO
--Tìm kiếm học sinh 
CREATE PROCEDURE prSearch_HS
@stenhs NVARCHAR(50),
@sdiachi NVARCHAR(50)
AS
BEGIN
	SELECT * 
	FROM tblHocSinh
	WHERE tblHocSinh.sHoTenHS LIKE N'%' + @stenhs + '%' OR 
		  tblHocSinh.sDiaChi LIKE N'%' + @sdiachi + '%'
END
GO
-- Lấy thông tin mã lớp học

CREATE PROC prSelect_MaLH
AS
BEGIN
	SELECT *
	FROM tblLopHoc
END
GO

--Điểm của từng học sinh
CREATE PROCEDURE prSelect_DiemTheoHS
@smahs VARCHAR(10)
AS
	BEGIN
			SELECT diem.*
			FROM tblDiem diem INNER JOIN tblHocSinh
			ON diem.sMaHS = tblHocSinh.sMaHS
			WHERE @smahs = tblHocSinh.sMaHS
			
END
GO
		--MÔN HỌC
--Lấy ra danh sách môn học
CREATE PROC prSelect_MH
AS
BEGIN
	SELECT * FROM tblMonHoc
END
GO

--Check trùng môn học (mã)
CREATE PROCEDURE prChecktrung_MH
@smamh VARCHAR(20)
AS
BEGIN	
	SELECT COUNT(tblMonHoc.sMaMH) FROM tblMonHoc
	WHERE tblMonHoc.sMaMH = @smamh
END
GO

-- Lấy ra mã môn học
CREATE PROC prSelectMa_MH
AS
BEGIN
	SELECT sMaMH
	FROM tblMonHoc
END
GO

--Thêm thông tim môn học
CREATE PROC prInsert_MH
@smamh VARCHAR(20),
@stenmh NVARCHAR(30), 
@isotiet INT,
@smagv VARCHAR(6)
AS 
BEGIN
	INSERT INTO tblMonHoc
	VALUES(@smamh,@stenmh,@isotiet,@smagv)
END
GO

--Chỉnh sửa thông tin môn học
CREATE PROC prUpdate_MH
@smamh VARCHAR(20),
@stenmh NVARCHAR(50),
@isotiet INT,
@smagv VARCHAR(6)
AS
BEGIN
    UPDATE tblMonHoc
	SET
	sMaMH = @smamh,
	sTenMH = @stenmh,
	iSoTiet = @isotiet,
	sMaGV = @smagv
   WHERE sMaMH = @smamh
END
GO

--Xoá thông tin môn học
CREATE PROC prDelete_MH
@smamh VARCHAR(20)
AS
BEGIN
   DELETE tblMonHoc 
   WHERE sMaMH = @smamh
END
GO

			
									-- KHỐI LỚP --


--Lấy ra danh sách khối lớp
CREATE PROC prSelect_KL
AS
BEGIN
	SELECT * FROM tblKhoiLop
END
GO

--Check trùng mã khối lớp
CREATE PROCEDURE prChecktrungg_MaKL
@smakl VARCHAR(10)
AS
BEGIN	
	SELECT COUNT(tblKhoiLop.sMaKhoiLop) FROM tblKhoiLop
	WHERE tblKhoiLop.sMaKhoiLop = @smakl
END
GO

--Lấy ra thông tin mã và tên khối lớp
CREATE PROCEDURE prSelect_MaTenKL
AS
BEGIN
	SELECT sMaKhoiLop,sTenKhoiLop
	FROM tblKhoiLop
END
GO

--Thêm thông tin khối lớp
CREATE PROC prInsert_KL
@smakl VARCHAR(6),
@stenkl NVARCHAR(30)
AS 
BEGIN
	INSERT INTO tblKhoiLop
	VALUES(@smakl,@stenkl)
END
GO

--Chỉnh sửa thông tin khối lớp
CREATE PROC prUpdate_KL
@smakl VARCHAR(6),
@stenkl NVARCHAR(30)
AS
BEGIN
    UPDATE tblKhoiLop
	SET
		sMaKhoiLop = @smakl,
		sTenKhoiLop = @stenkl
   WHERE sMaKhoiLop = @smakl
END
GO

--Xoá thông tin khối lớp
CREATE PROC prDelete_KL
@smakl VARCHAR(6)
AS
BEGIN
   DELETE tblKhoiLop 
   WHERE sMaKhoiLop = @smakl
END
GO

										--GIÁO VIÊN--

--Lấy ra thông tin giáo viên
CREATE PROC prSelect_GV
AS
BEGIN
	SELECT * FROM tblGiaoVien
END
GO

--Check trùng mã giáo viên
CREATE PROCEDURE prChecktrung_MaGV
@smagv VARCHAR(6)
AS
BEGIN	
	SELECT COUNT(tblGiaoVien.sMaGV) 
	FROM tblGiaoVien
	WHERE tblGiaoVien.sMaGV = @smagv
END
GO

--Lấy ra mã giáo viên

CREATE PROC prSelect_MaGV 
AS
BEGIN 
	SELECT sMaGV
	FROM tblGiaoVien
END
GO

CREATE PROCEDURE prGiaovientheoLH
@smagv VARCHAR(6)
AS
BEGIN
		SELECT a.sTenGV,b.*
		FROM tblGiaoVien a INNER JOIN tblLopHoc b
		ON a.sMaGV = b.sMaGV
		WHERE a.sMaGV = @smagv
		ORDER BY a.sMaGV
END
GO

ALTER PROCEDURE prSelectAllGiaovien
AS
BEGIN
	SELECT *
	FROM tblGiaoVien
END
GO

-- Thêm thông tin giáo viên
CREATE PROCEDURE prInsert_GV
@smagv VARCHAR(6),
@stengv NVARCHAR(50),
@sgioitinh NVARCHAR(10),
@sdiachi NVARCHAR(50),
@ssdt VARCHAR(12)
AS
BEGIN
	INSERT INTO tblGiaoVien
	VALUES (@smagv ,@stengv ,@sgioitinh ,@sdiachi ,@ssdt)
END
GO

--Chỉnh sửa thông tin giáo viên
CREATE PROC prUpdate_GV
@smagv VARCHAR(6),
@stengv NVARCHAR(50),
@sgioitinh NVARCHAR(10),
@sdiachi NVARCHAR(50),
@ssdt VARCHAR(12)
AS
BEGIN
    UPDATE tblGiaoVien
	SET
	   sTenGV = @stengv,
	   sGioiTinh = @sgioitinh,
	   sDiaChi = @sdiachi,
	   sDienThoai = @ssdt
   WHERE sMaGV = @smagv
END
GO

--Xoá thông tin giáo viên
CREATE PROC prDelete_GV
@smagv VARCHAR(6)
AS
BEGIN
   DELETE tblGiaoVien 
   WHERE sMaGV = @smagv
END
GO

--Tìm kiếm thông tin giáo viên
CREATE PROCEDURE prSearch_GV
@stengv NVARCHAR(50),
@sdiachi NVARCHAR(50),
@ssdt VARCHAR(12)
AS
BEGIN
		SELECT * 
		FROM tblGiaoVien
			WHERE tblGiaoVien.sTenGV LIKE N'%' + @stengv + '%' 
				OR tblGiaoVien.sDiaChi LIKE N'%' + @sdiachi 
				OR tblGiaoVien.sDienThoai LIKE N'%' + @ssdt
END
GO

											-- LỚP HỌC -- 

-- Lấy danh sách lớp học
CREATE PROC prSelect_LH
AS
BEGIN
	SELECT * FROM tblLopHoc
END
GO

--Check trùng mã môn học
CREATE PROCEDURE prChecktrung_MaLH
@smalh VARCHAR(10)
AS
BEGIN	
		SELECT COUNT(tblLopHoc.sMaLH) FROM tblLopHoc
		WHERE tblLopHoc.sMaLH = @smalh
END
GO

--Thêm thông tin lớp học
CREATE PROC prInsert_LH
@smalh VARCHAR(10),
@stenlh NVARCHAR(50), 
@smakl VARCHAR(6),
@isiso INT,
@smagv VARCHAR(10)
AS 
BEGIN
	INSERT INTO tblLopHoc
	VALUES(@smalh ,@stenlh , @smakl, @isiso ,@smagv )
END
GO

--Chỉnh sửa thông tin lớp học
CREATE PROC prUpdate_LH
@smalh VARCHAR(10),
@stenlh NVARCHAR(50), 
@smakl VARCHAR(6),
@isiso INT,
@smagv VARCHAR(10)
AS
BEGIN
    UPDATE tblLopHoc
	SET
		sMaLH = @smalh,
		sTenLH = @stenlh,
		sMaKhoiLop = @smakl,
		iSiSo = @isiso,
		sMaGV = @smagv
	 WHERE sMaLH = @smalh
END
GO

--Xoá thông tin lớp học
CREATE PROC prDelete_LH
@smalh VARCHAR(10)
AS
BEGIN
   DELETE tblLopHoc 
   WHERE sMaLH = @smalh
END
GO

									--NĂM HỌC--
--Lấy ra thông tin năm học
CREATE PROC prSelect_NH
AS
BEGIN
	SELECT * FROM tblNamHoc
END
GO

-- Lấy ra thông tin mã và tên năm học
CREATE PROC prSelect_MaTenNH
AS
BEGIN
	SELECT sMaNamHoc, sTenNamHoc
	FROM tblNamHoc
END
GO

-- Check trùng mã năm học
CREATE PROC prChecktrung_MaNH
@smanh VARCHAR(6)
AS
BEGIN
	SELECT COUNT(tblNamHoc.sMaNamHoc) 
	FROM tblNamHoc
	WHERE tblNamHoc.sMaNamHoc = @smanh
END
GO

--Thêm thông tin môn học
CREATE PROC prInsert_NH
@smanh VARCHAR(6),
@stennh NVARCHAR(30)
AS
BEGIN
	INSERT INTO tblNamHoc
	VALUES(@smanh,@stennh)
END
GO

-- Xoá thông tin năm học
CREATE PROC prDelete_NH
@smanh VARCHAR(6)
AS
BEGIN
	DELETE tblNamHoc
	WHERE sMaNamHoc = @smanh
END
GO

--Cập nhật thông tin năm học
CREATE PROC prUpdate_NH
@smanh VARCHAR(6),
@stennh NVARCHAR(30)
AS
BEGIN
	UPDATE tblNamHoc
	SET sMaNamHoc = @smanh,
		sTenNamHoc = @stennh
	WHERE sMaNamHoc = @smanh
END
GO

									--BẢNG ĐIỂM--

--Lấy ra thông tin danh sách điểm
CREATE PROC prSelect_Diem
AS
BEGIN
	SELECT * FROM tblDiem
END
GO

-- Thêm thông tin điểm
ALTER PROC prInsert_Diem
@smalh VARCHAR(10),
@smahs VARCHAR(10), 
@smanh VARCHAR(6),
@smahk VARCHAR(3),
@smamh VARCHAR(20),
@fdiemMieng FLOAT,
@fdiem15P FLOAT,
@fdiem45P FLOAT,
@fdiemHocKy FLOAT,
@fdiemTBHK FLOAT
AS
BEGIN
	INSERT INTO tblDiem
	VALUES(@smalh ,@smahs ,@smanh ,@smahk ,@smamh,@fdiemMieng ,@fdiem15P ,@fdiem45P ,@fdiemHocKy ,@fdiemTBHK )
END
GO

-- Lấy ra mã và tên lớp
CREATE PROC prSelect_MaLHTenLH
AS
BEGIN
	SELECT sMaLH,sTenLH
	FROM tblLopHoc
END
GO

-- Lấy ra mã và tên môn học
CREATE PROC prSelect_MaMHTenMH
AS
BEGIN 
	SELECT sMaMH,sTenMH
	FROM tblMonHoc
END
GO

--Lấy ra danh sách năm học,học kì
CREATE PROC prSelect_diemNH_HK
@smanh VARCHAR(6),
@smahk VARCHAR(3),
@smamh VARCHAR(20),
@smahs VARCHAR(10)
AS
BEGIN
	SELECT *
	FROM tblDiem
	WHERE sMaHS = @smahs AND sMaNamHoc = @smanh AND sMaHocKy = @smahk AND sMaMH = @smamh
END
GO

--Lấy thông tin học kỳ
CREATE   PROC prSelect_AllHK
AS
BEGIN
	SELECT *
	FROM tblHocKy
END
GO

--Lấy tất cả tên học kỳ

CREATE PROC prSelect_TenHK
AS
BEGIN
	SELECT sTenHocKy
	FROM tblHocKy
END
GO

--Lấy thông tin năm học
CREATE PROC prSelect_AllNH
AS
BEGIN
	SELECT *
	FROM tblNamHoc
END
GO


-- Lấy mã và tên học sinh theo mã lớp học
CREATE  PROC prSelect_MaTenHS_byLH
@smalh VARCHAR(10)
AS
BEGIN
	SELECT hs.sMaHS, hs.sHoTenHS, d.sMaNamHoc, d.sMaHocKy, d.sMaMH, d.fDiemMieng, d.fDiem15P, d.fDiem45p, d.fDiemHocKy
	FROM tblHocSinh AS hs
		LEFT JOIN tblDiem AS d
	ON  d.sMaHS =	hs.sMaHS 
	WHERE hs.sMaLH = @smalh
	SELECT sMaHS,sHoTenHS
	FROM tblHocSinh
	WHERE sMaLH = @smalh
END
GO
-- Lấy mã và tên học sinh theo mã lớp học
CREATE  PROC prSelect_MaTenLH_byNH
@smanh VARCHAR(6)
AS
BEGIN
	SELECT lh.sMaLH, lh.sTenLH
	FROM tblLopHoc AS lh
		LEFT JOIN tblDiem AS d
	ON  d.sMaLH =	lh.sMaLH 
	WHERE lh.sMaNamHoc = @smanh
	SELECT sMaNamHoc,sTenNamHoc
	FROM tblNamHoc
	WHERE sMaNamHoc = @smanh
END
GO



--Sửa điểm
CREATE PROC prUpdate_Diem
@smalh VARCHAR(10),
@smahs VARCHAR(10), 
@smanh VARCHAR(6),
@smahk VARCHAR(3),
@smamh VARCHAR(20),
@fdiemMieng FLOAT,
@fdiem15P FLOAT,
@fdiem45P FLOAT,
@fdiemHocKy FLOAT
	AS
	BEGIN
		UPDATE tblDiem
		SET fDiemMieng = @fdiemMieng, fDiem15P = @fdiem15P, fDiem45P =@fdiem45P , fDiemHocKy = @fdiemHocKy
		WHERE sMaHS = @smahs AND sMaNamHoc = @smanh AND sMaHocKy = @smahk AND sMaMH = @smamh
	END

	SELECT * FROM tblDiem
GO

--Xoá điểm
CREATE  PROC prDelete_Diem
@smahs VARCHAR(10), 
@smanh VARCHAR(6),
@smahk VARCHAR(3),
@smamh VARCHAR(20)
AS
BEGIN
	DELETE
	FROM tblDiem
	WHERE sMaHS = @smahs AND sMaNamHoc = @smanh AND sMaHocKy = @smahk AND sMaMH = @smamh
END
GO

-- In báo cáo danh sách điểm theo lớp
CREATE PROCEDURE prDiemtheoLop
@smalh NVARCHAR(10)
AS
BEGIN
	SELECT a.sMaLH,b.*
	FROM tblLopHoc a INNER JOIN tblDiem b
	ON a.sMaLH = b.sMaLH
	WHERE a.sMaLH = @smalh
	ORDER BY a.sMaLH
END
GO

-- In báo cáo danh sách điểm theo học sinh
CREATE PROCEDURE prDiemtheoHS
@smahs NVARCHAR(10)
AS
BEGIN
	SELECT *
	FROM tblDiem
	WHERE sMaHS = @smahs
END
GO


--Lấy ra học sinh theo từng lớp
CREATE PROC prSelect_HSbyLH
@smalh NVARCHAR(10)
AS
BEGIN
	SELECT a.sMaLH,a.sTenLH, b.*
		FROM tblLopHoc a INNER JOIN tblHocSinh b
		ON a.sMaLH = b.sMaLH
		WHERE a.sMaLH = @smalh
		ORDER BY a.sMaLH
END
/*
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
 
 
*/
			
 GO
--Hiển thị số lượng học sinh
CREATE PROC pr_CountHS 
@mahs VARCHAR(10)
AS
BEGIN
   SELECT tblLopHoc.sMaHS COUNT (tblLopHoc.sMaHS) AS [soluong]
   FROM tblLopHoc,tblHocSinh
   WHERE tblLopHoc.sMaHS =tblHocSinh.sMaHS 
   and tblLopHoc.sMaHS = @mahs
   GROUP BY tblLopHoc.sMaHS, tblHocSinh.sHoTenHS
END
GO

  --lấy ra số lượng
CREATE PROC pr_SelectSL
AS 
BEGIN 
	SELECT sMaLH,iSiSo
	FROM tblLopHoc
END
GO

ALTER TABLE tblDiem
ADD sMaKhoiLop VARCHAR(6),
CONSTRAINT FK_tblDiem_tblKhoiLop FOREIGN KEY (sMaKhoiLop) REFERENCES tblKhoiLop(sMaKhoiLop)

UPDATE tblDiem
SET sMaKhoiLop = 'KHOI10'

SELECT * FROM tblDiem
SELECT * FROM tblKhoiLop
SELECT * FROM tblLopHoc
SELECT * FROM tblHocSinh


