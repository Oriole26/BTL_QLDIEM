alter PROC hs_by_maLH 
@maLH VARCHAR(10)
AS
BEGIN
	SELECT hs.sMaHS, hs.sHoTenHS, d.sMaNamHoc, d.sMaHocKy, d.sMaMH, d.fDiemMieng, d.fDiem15P, d.fDiem45p, d.fDiemHocKy
	FROM tblHocSinh AS hs
	LEFT JOIN tblDiem AS d
	ON  d.sMaHS =	hs.sMaHS 
	where hs.sMaLH = @maLH
END