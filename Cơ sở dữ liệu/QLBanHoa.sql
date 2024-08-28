use master
go
drop database QLBanHoa
go
create database QLBanHoa
go
use  QLBanHoa
go 


-- Tạo bảng Khách Hàng
CREATE TABLE KhachHang (
    MaKH  varchar(10) PRIMARY KEY,
    TenKH NVARCHAR(50),
    Email VARCHAR(50) NOT NULL,
    DiaChi NVARCHAR(255),
    MatKhau VARCHAR(50) NOT NULL,
);
-- Tạo bảng Sản Phẩm
CREATE TABLE SanPham (
  MaSP varchar(10),
  MaLoaiSP varchar(10),
  TenNCC NVARCHAR(50),
  TenSP NVARCHAR(255) NOT NULL,
  MauSP NVARCHAR(255) NOT NULL,
  Gia FLOAT NOT NULL,
  MoTa NVARCHAR(255),
  HinhAnh VARCHAR(255) NOT NULL,
  SoLuongTonKho INT NOT NULL,
  TinhTrang NVARCHAR(50),
  PRIMARY KEY (MaSP),
);
--Tạo bảng loại sản phẩm 
CREATE TABLE LoaiSanPham (
  MaLoaiSP varchar(10),
  TenLoaiSP  NVARCHAR(255) NOT NULL,
  PRIMARY KEY (MaLoaiSP)
);
-- Tạo bảng Đơn Hàng
CREATE TABLE DonHang (
  MaDH varchar(10),
  MaKH varchar(10),
  NgayDatHoa DATETIME NOT NULL,
  NgayGiaoHoa DATETIME,
  DiaChiGiaoHoa NVARCHAR(255) NOT NULL,
  TinhTrangDH NVARCHAR(50), --Đã giao, đang giao hay bị huỷ, ...
  PhuongThucTT NVARCHAR(50),
  PRIMARY KEY (MaDH),
);
-- Tạo bảng Chi Tiết Đơn Hàng
CREATE TABLE CTDonHang (
  MaDH varchar(10),
  MaSP varchar(10),
  SL INT,
  ThanhTien FLOAT NOT NULL,
  CONSTRAINT PK_MaDH_MaSP PRIMARY KEY(MaDH,MaSP)
);

--Khóa ngoại
ALTER TABLE SanPham
ADD CONSTRAINT FK_SanPham_LoaiSanPham FOREIGN KEY (MaLoaiSP) REFERENCES LoaiSanPham (MaLoaiSP)
ALTER TABLE DonHang
ADD CONSTRAINT FK_Donhang_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang (MaKH)
ALTER TABLE CTDonHang
ADD CONSTRAINT FK_CTDonhang_DonHang FOREIGN KEY (MaDH) REFERENCES DonHang (MaDH)
ALTER TABLE CTDonHang
ADD CONSTRAINT FK_CTDonhang_SanPham FOREIGN KEY (MaSP) REFERENCES SanPham (MaSP)
--Dữ liệu 
-- Thêm dữ liệu vào bảng LoaiSanPham
INSERT INTO LoaiSanPham (MaLoaiSP, TenLoaiSP) VALUES
('LSP001', N'Hoa Lan'),
('LSP002', N'Hoa Hồng'),
('LSP003', N'Hoa Cúc'),
('LSP004', N'Hoa Sen');

-- Thêm dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (MaKH, TenKH, Email, DiaChi, MatKhau) VALUES
('KH001', N'Dương Thiên Anh', 'ThienAnh@gmail.com', N'Hà Nội', 'pass123'),
('KH002', N'Lê Hoài Nam', 'HoaiNam@gmail.com', N'Hồ Chí Minh', 'pass456'),
('KH003', N'Phú', 'Phu@gmail.com', N'Đà Nẵng', 'pass789');
-- Thêm dữ liệu vào bảng SanPham
INSERT INTO SanPham (MaSP, MaLoaiSP, TenNCC, TenSP, MauSP, Gia, MoTa, HinhAnh, SoLuongTonKho, TinhTrang) VALUES
('SP001', 'LSP001', 'NCC1', N'Hoa Phong Lan Trắng', N'Trắng', 50.00, N'Mô tả cho hoa lan trắng', '\C:\Users\ngogi\Downloads\DoAnWeb\HinhAnh\HoaLanTrang.jpg', 100, N'Còn hàng'),
('SP002', 'LSP002', 'NCC2', N'Hoa Hồng Đỏ', N'Đỏ', 30.00, N'Mô tả cho hoa hồng đỏ', '\C:\Users\ngogi\Downloads\DoAnWeb\HinhAnh\HoaHongDo.jpg', 80, N'Còn hàng'),
('SP003', 'LSP003', 'NCC3', N'Hoa Cúc Vàng', N'Vàng', 25.00, N'Mô tả cho hoa cúc vàng', '\C:\Users\ngogi\Downloads\DoAnWeb\HinhAnh\HoaCucVang.jpg', 120, N'Hết hàng');

UPDATE SanPham
SET HinhAnh = 'HoaLanTrang.jpg'
WHERE MaSP = 'SP001'

UPDATE SanPham
SET HinhAnh = 'HoaHongDo.jpg'
WHERE MaSP = 'SP002'

UPDATE SanPham
SET HinhAnh = 'HoaCucVang.jpg'
WHERE MaSP = 'SP003'

-- Thêm dữ liệu vào bảng DonHang
--INSERT INTO DonHang (MaDH, MaKH, NgayDatHoa, NgayGiaoHoa, DiaChiGiaoHoa, TinhTrangDH) VALUES
--('DH001', 'KH001', '2023-11-25 10:00:00', '2023-11-26 14:00:00', N'Hà Nội', N'Đã giao'),
--('DH002', 'KH002', '2023-11-24 15:00:00', NULL, N'Hồ Chí Minh', N'Đang giao'),
--('DH003', 'KH003', '2023-11-23 09:00:00', '2023-11-24 12:00:00', N'Đà Nẵng', N'Đã giao');

---- Thêm dữ liệu vào bảng CTDonHang
--INSERT INTO CTDonHang (MaDH, MaSP,SL, ThanhTien) VALUES
--('DH001', 'SP001', 5000.00, N'Chuyển khoản', N'Đã thanh toán'),
--('DH001', 'SP002', 900.00, N'Tiền mặt', N'Chưa thanh toán'),
--('DH002', 'SP002', 1200.00, N'Chuyển khoản', N'Chưa thanh toán'),
--('DH003', 'SP003', 1250.00, N'Chuyển khoản', N'Đã thanh toán');

SELECT* FROM CTDonHang
SELECT * FROM SanPham

SELECT * FROM KhachHang

SELECT * FROM DonHang

SELECT * FROM CTDonHang