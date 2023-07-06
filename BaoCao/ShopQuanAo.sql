--CREATE DATABASE website giới thiệu và bán quần áo
CREATE DATABASE ShopQuanAo 
GO
USE ShopQuanAo

---
use master 
go
alter database ShopQuanAo set single_user with rollback immediate

drop database ShopQuanAo
----------------------TẠO BẢNG----------------------

--Tạo bảng Đối tượng--
CREATE TABLE DoiTuong
(
	Id INT IDENTITY PRIMARY KEY,
	TenDoiTuong NVARCHAR(100) NOT NULL UNIQUE
)
GO

--Tạo bảng Thể loại--
CREATE TABLE TheLoai
(
	Id INT IDENTITY PRIMARY KEY,
	TenTheLoai NVARCHAR(100) NOT NULL UNIQUE
)
GO

--Tạo bảng Kích cỡ--
CREATE TABLE KichCo
(
	Id INT IDENTITY PRIMARY KEY,
	TenKichCo NVARCHAR(100) NOT NULL UNIQUE
)
GO

--Tạo bảng Chất liệu--
CREATE TABLE ChatLieu
(
	Id INT IDENTITY PRIMARY KEY,
	TenChatLieu NVARCHAR(100) NOT NULL UNIQUE
)
GO

--Tạo bảng Nhà sản xuất--
CREATE TABLE NhaSanXuat
(
	Id INT IDENTITY PRIMARY KEY,
	TenNSX NVARCHAR(100) NOT NULL UNIQUE,
	DiaChi NVARCHAR(100) NULL,
	DienThoai VARCHAR(20)  NULL
)
GO

--Tạo bảng Sản phẩm--
CREATE TABLE SanPham
(
	Id INT IDENTITY PRIMARY KEY,
	TenSP NVARCHAR(50) NOT NULL UNIQUE,
	DoiTuongId int FOREIGN KEY REFERENCES DoiTuong(Id) on update cascade on delete cascade,
	TheLoaiId int FOREIGN KEY REFERENCES TheLoai(Id) on update cascade on delete cascade,
	ChatLieuId int FOREIGN KEY REFERENCES ChatLieu(Id) on update cascade on delete cascade,
	NSXId int FOREIGN KEY REFERENCES NhaSanXuat(Id) on update cascade on delete cascade,
	DonViTinh NVARCHAR(50) NULL,
	SoLuong INT NULL,
	DonGia INT NULL ,
	MoTa NTEXT NULL,
	NgayCapNhat SMALLDATETIME NULL,
	HinhMinhHoa VARCHAR(50) NULL
)
GO

--Tạo bảng Khách hàng--
CREATE TABLE KhachHang
(
	Id INT IDENTITY PRIMARY KEY,
	HoTen NVARCHAR(50) NOT NULL ,
	DiaChi NVARCHAR(100)NULL,
	DienThoai VARCHAR(20) NULL,
	AccID int foreign key references TaiKhoan(Id) on update cascade on delete cascade,
	Avatar VARCHAR(100) NOT NULL,
	NgaySinh SMALLDATETIME NULL,
	GioiTinh BIT NOT NULL,
	Email VARCHAR(50) NULL
)
GO

--Tạo bảng Chi tiết đặt hàng--
CREATE TABLE ChiTietDatHang
(
	DonDatHangId int FOREIGN KEY REFERENCES DonDatHang(Id) on update cascade on delete cascade,
	SanPhamId int FOREIGN KEY REFERENCES SanPham(Id) on update cascade on delete cascade,
	SizeId int FOREIGN KEY REFERENCES KichCo(Id) on update cascade on delete cascade,
	SoLuong INT NULL,
	primary key(DonDatHangId,SanPhamId)
)
GO

--Tạo bảng đơn đh
Create table DonDatHang
(
	Id INT IDENTITY PRIMARY KEY,
	KhachHangId int foreign key references KhachHang(Id) on update cascade on delete cascade,
	NgayDatHang SMALLDATETIME NULL
)
Go

--Tạo bảng Nhân viên--
CREATE TABLE NhanVien
(
	Id int IDENTITY PRIMARY KEY,
	HoTen NVARCHAR(50) NOT NULL ,
	GioiTinh BIT NOT NULL,
	NgaySinh SMALLDATETIME NULL,
	DienThoai VARCHAR(20) NULL,
	DiaChi NVARCHAR(100) NULL,
	Avatar NVARCHAR(100),
	AccID int foreign key references TaiKhoan(Id) on update cascade on delete cascade
)
GO

--Tạo bảng users--
CREATE TABLE TaiKhoan
(
	Id INT IDENTITY PRIMARY KEY,
	UserName NVARCHAR(30) NOT NULL,
	[PassWord] NVARCHAR(30) NOT NULL,
	Allowed INT DEFAULT(0)
)
GO

----------------------THÊM DỮ LIỆU VÀO BẢNG----------------------

--Thêm dữ liệu vào bảng Chất liệu--
INSERT INTO ChatLieu VALUES(N'Vải Cotton')
INSERT INTO ChatLieu VALUES(N'Vải Da')
INSERT INTO ChatLieu VALUES(N'Vải Đũi')
INSERT INTO ChatLieu VALUES(N'Vải Jeans')
INSERT INTO ChatLieu VALUES(N'Vải Kaki')
INSERT INTO ChatLieu VALUES(N'Nhựa')
INSERT INTO ChatLieu VALUES(N'Vải')
INSERT INTO ChatLieu VALUES(N'Linon')
INSERT INTO ChatLieu VALUES(N'Kim loại')
GO


--Thêm dữ liệu vào bảng Đối tượng--
INSERT INTO DoiTuong VALUES(N'Bé trai')
INSERT INTO DoiTuong VALUES(N'Bé gái')
INSERT INTO DoiTuong VALUES(N'Trẻ sơ sinh')
INSERT INTO DoiTuong VALUES(N'Tất cả')
GO


--Thêm dữ liệu vào bảng Khách hàng--
INSERT INTO KhachHang VALUES(	N'Khách hàng A',	N'Hà Nội','0912345678',1,'kh1.jpg','01/01/1990',1,'A@gmail.com')
INSERT INTO KhachHang VALUES(	N'Khách hàng B',	N'Đà Nẵng','0912345678',2,'kh2.jpg','01/01/1990',1,'B@gmail.com')
INSERT INTO KhachHang VALUES(	N'Khách hàng C',	N'Hồ Chí Minh','0912345678',3,'kh3.jpg','01/01/1990',1,'C@gmail.com')
GO

--account 
Insert into TaiKhoan values('khachhanga', '123', 0)
Insert into TaiKhoan values('khachhangb', '123', 0)
Insert into TaiKhoan values('khachhangc', '123', 0)
---nhanvien
Insert into TaiKhoan values('nhanvien1', '123', 1)
Insert into TaiKhoan values('nhanvien2', '123', 1)
Insert into TaiKhoan values('nhanvien3', '123', 1)

--Thêm dữ liệu vào bảng Kích cỡ--
INSERT INTO KichCo VALUES('01')
INSERT INTO KichCo VALUES('02')
INSERT INTO KichCo VALUES('03')
INSERT INTO KichCo VALUES('04')
INSERT INTO KichCo VALUES('05')
GO

--Thêm dữ liệu vào bảng Nhân viên--
INSERT INTO NhanVien VALUES(N'Nhân viên A',1,'01/01/1995','0987654321',	N'Hà Nội', 'nv1.jpg', 4)
INSERT INTO NhanVien VALUES(N'Nhân viên B',0,'01/01/1995','0987654321',	N'Hà Nội', 'nv2.jpg', 5)
INSERT INTO NhanVien VALUES(N'Nhân viên C',0,'01/01/1995','0987654321',	N'Hà Nội', 'nv3.jpg', 6)
GO


--Thêm dữ liệu vào bảng Nhà sản xuất--
INSERT INTO NhaSanXuat VALUES(N'Nhà sản xuất A',	N'Hà Nội','0123456789')
INSERT INTO NhaSanXuat VALUES(N'Nhà sản xuất B',	N'Lạng Sơn','0123456789')
INSERT INTO NhaSanXuat VALUES(N'Nhà sản xuất C',	N'Đà Nẵng','0123456789')
INSERT INTO NhaSanXuat VALUES(N'Nhà sản xuất D',	N'Hồ Chí Minh','0123456789')
GO


--Thêm dữ liệu vào bảng Sản phẩm--
--Số liệu và hình ảnh thông tin sản phẩm được lấy từ trang babi.vn
--Bé Trai --
INSERT INTO SanPham VALUES(N'Bộ bé trai In Hình Cá Mập',1,4,7,1,		N'Bộ',10,'129000',	N'Thiết kế áo thun sát nách cổ tròn với quần short có túi thêm cách phối màu khá độc đáo, bộ đồ bé trai mặc hè này khá ấn tượng, bé có thể mặc nhà hay mặc đi chơi với chúng bạn cùng xóm, ấn tượng hơn với hình in chú cá mập vùng vẫy trong nước làm tăng thêm phong cách khỏe khoắn và năng động. Màu đỏ, cam, vàng và xanh để mẹ có nhiều lựa chọn phù hợp. Mua ngay cho con mẹ ơi. ','01/01/2019','MSP001.jpg')
INSERT INTO SanPham VALUES(N'Bộ bé trai In Số 23',		1,	4,	1,1,	N'Bộ',10,'129000',	N'Đồ bộ ngắn tay bé trai chất liệu thun cotton mềm, thoáng mát, thấm hút tốt phù hợp cho bé trai tha hồ hoạt động. Áo thiết kế đơn giản, cổ tròn, tay ráp lăng thời trang, phía trước ngực in số 23 kèm theo quần short lưng thun có túi. Hàng có đủ size cho các bé từ 11 kg - 45 kg, mẹ mua ngay cho các bé nhé.','01/01/2019','MSP002.jpg')
INSERT INTO SanPham VALUES(N'Sơ Mi Ngắn bé trai Màu Trơn Thêu Xe',1,1,1,2,	N'Chiếc',10,'129000',	N'Áo sơ mi ngắn tay cho bé trai từ 1 - 6 tuổi với chất liệu vải cotton mịn đẹp và thoáng mát cho bé sự thoải mái tối đa khi mặc. Áo có 2 màu xanh trắng dễ dàng cho mẹ phối đồ, phù hợp với cả quần tây, quần jean hay kaki dài hoặc ngắn đều rất đẹp. Kiểu dáng áo đơn giản và lịch sự, năng động có thêu xe ô tô trước ngực. Mua ngay cho con trai cưng nha mẹ ơi. ','01/01/2019','MSP003.jpg')
INSERT INTO SanPham VALUES(N'Thun Sát Nách bé trai Chữ YEAH!',	1,1,1,4,	N'Chiếc',10,'79000',	N'Áo thun sát nách chắc chắn là trang phục cần cho mọi bé trai trong mùa hè nóng nực này. Áo sát nách cổ tròn với chất liệu thun cotton thoáng mát và thấm hút tốt giúp bé tha hồ vận động mà không lo ướt đẫm mồ hôi. Ngoài ra còn có thể phối với quần short jean, kaki cho bé mặc đi học hè đầy khỏe mạnh và năng động. Các mẹ xem và chọn lựa ngay màu sắc, kích thước phù hợp cho bé nhà mình nha.','01/01/2019','MSP004.jpg')
INSERT INTO SanPham VALUES(N'Quần Jeans Lửng bé trai',			1,2,4,1,	N'Chiếc',10,'139000',	N'Thiết kế lưng thun, không có dây kéo mặc rất dễ chịu, 2 túi trước và 2 túi sau cho bé bỏ đồ nhỏ nhỏ, linh tinh. Đường wash nhẹ, viền chỉ nổi tinh tế thêm nhấn nhá cho sản phẩm. Quần kết hợp cùng áo thun hay áo sơ mi cho bé đi học, đi chơi, đi tiệc cùng bố mẹ nha. Đến Babi mua ngay nào. ','01/01/2019','MSP005.jpg')
INSERT INTO SanPham VALUES(N'Bộ Pijama bé trai Sọc Caro',		1,4,1,3,	N'Bộ',10,'139000',	N'Thời trang mặc nhà cho bé không thể không kể đến thời trang đồ pijama. Tùy theo mùa mà các nhà thiết kế cho ra kiểu đồ pijama khác nhau. Nếu như mùa lạnh những kiểu đồ pijama tay dài, quần dài được các mẹ chọn mua cho con thì mùa hè là kiểu tay ngắn, quần ngắn gọn gàng, mát mẻ. Bộ pijama dưới đây sẽ là lựa chọn ưng ý cho những mẹ thích kiểu thời trang pijama cho bé mặc nhà.','01/01/2019','MSP006.jpg')
INSERT INTO SanPham VALUES(N'Áo Thun bé trai In Heo Dễ Thương',	1,1,1,3,	N'Chiếc',10,'79000',	N'Áo thun cho bé từ 2 tuổi đến 12 tuổi in hình heo dễ thương, dễ phối quần mặc đi học, hay đi chơi cuối tuần. Mua ngay cho bé mẹ nhé!','01/01/2019','MSP007.jpg')
INSERT INTO SanPham VALUES(N'Quần Short Kaki bé trai',			1,2,5,2,	N'Chiếc',10,'119000',	N'Babi về thêm mẫu quần short mới cho bé trai cả nhà ghé xem nhé. Màu sắc hài hòa, thiết kế đơn giản, cực dễ phối cùng áo thun hoặc sơ mi. Bé mặc đi chơi, đi học, tiệc tùng cùng bố mẹ đều xinh lắm ạ ','01/01/2019','MSP008.jpg')
INSERT INTO SanPham VALUES(N'Áo Sơ Mi Caro Ngắn Tay bé trai',	1,1,1,4,	N'Chiếc',10,'138000',	N'Chất vải cotton mềm mịn, thoáng mát, bé sẽ không cảm thấy bí bách hay khó chịu khi mặc vào hè đâu ạ. Có 3 màu sắc tươi trẻ, siêu cưng, cậu nhóc mà kết hợp cùng quần jeans, hay quần short kaki thì chuẩn chỉnh luôn nè, đi chơi, đi dạo phố, hay đi tiệc cũng quá bảnh luôn. Đây chắc chắn là mẫu áo sơ mi ngắn tay cho bé trai được ưa chuộng nhất trong mùa hè này đấy mẹ. Xem ngay nhé','01/01/2019','MSP009.jpg')
INSERT INTO SanPham VALUES(N'Bộ bé trai Người Nhện',			1,4,7,3,	N'Chiếc',10,'138000',	N'Các bé trai yêu thích siêu anh hùng đâu rồi ạ? Hôm nay Babi về kiểu đồ bộ bé trai rất dễ thương, in hình siêu nhân nhện mà bé nào cũng yêu thích rồi đây. Với hình in siêu nhân bền đẹp, sinh động nhìn như thật giúp các bé tha hồ biến hóa thành các siêu anh hùng mình thích. Chất liệu vải chọn may bằng thun cotton 100% thoáng mát, hút mồ hôi nên các mẹ rất yêu tâm. Thời tiết nắng nóng, thiết kế áo sát tay, quần short rất thích hợp cho bé mặc chơi suốt cả ngày mà không bức bí hay khó chịu. Hàng đã có sẵn tại shop, đủ size bé từ 2 đến 7 tuổi ạ. ','01/01/2019','MSP010.jpg')
GO

--Bé Gái --
INSERT INTO SanPham VALUES(	N'Đầm Tole bé gái',					2,7,7,2,	N'Chiếc',10,'89000',	N'Bé sẽ không chịu những ngày hè quá nóng nếu mẹ biết chọn trang phục cho bé, chất liệu vải tole, lanh là lựa chọn không thể thiếu trong những ngày hè. Với chiếc đầm 2 dây được may bằng chất liệu vải tole mặc mát, ít nhăn, dáng xòe in hình heo đáng yêu, bé có thể mặc nhà hay tung tăng đây đó rất xinh mẹ nhé.','01/01/2019','MSP0011.jpg')
INSERT INTO SanPham VALUES(	N'Quần Legging bé gái',				2,2,7,2,	N'Chiếc',10,'39000',	N'Với chất liệu thun cotton co giãn 4 chiều, quần lửng, lưng thun co giãn mang đến sự thoải mái và thoáng mát cho bé khi mặc. Mẹ có thể phối quần legging ôm với áo váy dáng dài, áo thun hay sơ mi đều đẹp và hiện đại. Hiện tại babi về đủ size cho bé từ 6 tháng đến 9 tuổi cùng 5 màu đa dạng gồm vàng, trắng, hồng đào, hồng cam và xanh da trời nổi bật kết hợp họa tiết chấm bi tô cho bé năng động và cá tính.','01/01/2019','MSP012.jpg')
INSERT INTO SanPham VALUES(	N'Đầm Voan bé gái',					2,7,3,4,	N'Chiếc',10,'159000',	N'Kiểu đầm bé gái cổ yếm, họa tiết hình sao dễ thương rất thích cho bé mặc mùa hè, chất voan nhẹ tênh mặc rộng rãi mát mẻ. Rất thích hợp để bé đi chơi, đi biển trong dịp hè này. Mẹ mua ngay cho bé mặc rất đáng yêu ạ, có size cho bé từ 3 tuổi đến 10 tuổi mặc vừa vẹn mẹ nhé. 4 màu tươi tắn đỏ, hồng, vàng, trắng xinh tươi.','01/01/2019','MSP013.jpg')
INSERT INTO SanPham VALUES(	N'Quần Dài Alibaba bé gái',			2,2,7,3,	N'Chiếc',10,'69000',	N'Quần alibaba đang trở thành môt mặc nhà thay cho đồ bộ bởi nó gọn gàng và thời trang hơn, có thể mặc nhà, mặc đi ra ngoài đều xinh. Với chiếc quần alibaba dành cho bé gái này được may bằng chất liệu vải tole loại 1, mềm mịn, ít nhăn, mặc rất mát, họa tiết và màu sắc voi hồng thổ cẩm cổ điển phối dễ dàng với áo thun, áo kiểu rất dễ thương mẹ nhé! Mua ngay cho con nào mẹ ơi.','01/01/2019','MSP014.jpg')
INSERT INTO SanPham VALUES(	N'Đầm Bèo bé gái',					2,7,3,1,	N'Chiếc',10,'109000',	N'Đầm bé gái 3 màu đỏ, vàng và xám cho mẹ lựa chọn theo làn da của bé. Chất liệu thun cotton mặc mát dễ thấm hút mồ hôi, thiết kế đầm tay con, cổ tròn, chân váy nhúng bèo, ngực thiết kế màu sắc nổi bật. Cho bé gái nhà mình mặc thoải mái, mát mẻ và dễ thương.','01/01/2019','MSP015.jpg')
INSERT INTO SanPham VALUES(	N'Bộ Tole bé gái',					2,4,7,3,	N'Bộ',10,'89000',	N'Mùa hè đến rồi, lựa đồ mặc nhà cho bé mẹ chú ý đến chất liệu vải, những kiểu đồ bộ vải tole được các mẹ chọn lựa hàng đầu bởi chất tole mềm mại, mỏng, nhẹ mặc mát rất thích hợp cho bé mặc trong những ngày nắng nóng. Với thiết kế tay cánh tiên dễ thương, quần lửng, họa tiết hình ngựa đáng yêu, bộ đồ dưới đây đảm bảo các bé thích lắm ạ.','01/01/2019','MSP016.jpg')
INSERT INTO SanPham VALUES(	N'Quần Jeas Giả Váy bé gái',		2,2,4,3,	N'Chiếc',10,'139000',	N'Điệu đà và dễ thương khi mẹ phối chiếc quần jeans giả váy này với chiếc áo thun hay áo kiểu để đi chơi, đi tiệc rất tiện lợi và phong cách mẹ nhé. Với chiếc quần jeans giả váy cho bé gái này lưng thun, có túi, có nút giả phía trước, bé mặc thoải mái trông khỏe khoắn và năng động. Hai màu cơ bản xanh đậm và xanh nhạt mẹ nhé.','01/01/2019','MSP017.jpg')
INSERT INTO SanPham VALUES(	N'Áo Thun bé gái',					2,1,1,1,	N'Chiếc',10,'89000',	N'Đơn giản mẹ chỉ cần phối áo thun bé gái này với chiếc quần short trông bé tươi mới với phong cách thời trang khỏe khoắn và năng động. Áo được may bằng chất vải thun cotton áo cổ tròn, tay lỡ, in họa tiết hình thỏ dễ thương. Màu đỏ, đen và trắng dễ phối đồ. Mua ngay cho bé mặc phong cách nè mẹ ơi.','01/01/2019','MSP018.jpg')
INSERT INTO SanPham VALUES(	N'Đầm Yếm bé gái',					2,7,7,2,	N'Chiếc',10,'158000',	N'Không xúng xính như đầm xòe, hay đầm công chúa, đầm yếm cho bé gái xinh không kém. Set đầm yếm mà Babi giới thiệu dưới đây gồm áo crotop trong chất gân lụa mát, yếm dây bên ngoài cotton 100% giả xớ jeans mix thêm đôi giày thể thao hay đôi giày boot sẽ làm nổi bật phong cách thời trang khỏe khoắn và năng động của bé.','01/01/2019','MSP019.jpg')
INSERT INTO SanPham VALUES(	N'Đầm Lụa bé gái',					2,7,1,4,	N'Chiếc',10,'178000',	N'Hè tới rồi, bung lụa hết cỡ với mẫu đầm lụa cho bé được tặng kèm cả nón nữa mẹ nha. Thiết kế 2 dây thoải mái, nhấn nhá thêm bèo trước ngực, dáng váy xòe bồng bềnh, phía sau có kết nơ điệu đà, bé tha hồ thả dáng trên biển, hay xúng xính dạo phố cùng ba mẹ nè. Có 3 màu siêu đẹp cho mẹ chọn nhé, nhanh tay rinh ngay kẻo hết ạ','01/01/2019','MSP020.jpg')
GO

--Trẻ sơ sinh--
INSERT INTO SanPham VALUES(	N'Bộ trẻ sơ sinh Dễ Thương',		3,4,7,2,	N'Bộ',10,'78000',	N'Yêu từ cái nhìn đầu tiên rồi mẹ ơi. Ghé Babi khám phá ngay thiên đường thời trang mùa hè cho bé nào. Kiểu nào về cũng xinh và đáng yêu chất ngất hết ạ, đặc biệt với kiểu đồ bộ thun in hình thú này đây ạ. Áo ngắn tay, quần lỡ với chất vải thun cotton thì bao mát và thấm hút mồ hôi nên các mẹ không phải lo lắng nhé. Hình in xịn đẹp lại vô cùng dễ thương, đảm bảo bé nào cũng thích mê cho coi.','01/01/2019','MSP021.jpg')
INSERT INTO SanPham VALUES(	N'Bodysuit trẻ sơ sinh',			3,4,7,3,	N'Bộ',10,'42000',	N'Bodysuit sơ sinh cho bé gái - Thiết kế áo liền quần cho các bé gái cực kỳ ấm áp và tiện lợi, giúp bé ngủ ngon thoải mái vận động suốt cả ngày.','01/01/2019','MSP022.jpg')
INSERT INTO SanPham VALUES(	N'Bodysuit trẻ sơ sinh Họa Tiết',	3,4,7,1,	N'Bộ',10,'48000',	N'Không bức bí như ta tưởng khi mặc bodysuit, với thiết kế rất gọn gàng tiện lợi, bodysuit sơ sinh có 2 nút bên dưới cho mẹ dễ thay tã cho bé. Chất liệu thun cotton mềm mại, co giãn tốt tạo cảm giác rất dễ chịu cho bé, nhiều họa tiết và màu sắc khác nhau, mẹ có thể mua để thay đổi cho bé. Giá rẻ 48.000','01/01/2019','MSP023.jpg')
INSERT INTO SanPham VALUES(	N'Bodysuit trẻ sơ sinh Dễ Thương',	3,4,7,1,	N'Bộ',10,'89000',	N'Bodysuit Sơ Sinh Cho Bé Gái (3-12 Kg) - Kiểu áo liền quần mặc cực ấm cho các bé mới sinh, họa tiết xinh xắn đáng yêu.','01/01/2019','MSP024.jpg')
INSERT INTO SanPham VALUES(	N'Áo trẻ sơ sinh',					3,1,1,3,	N'Chiếc',10,'40000',	N'Áo cho bé sơ sinh không chỉ có thiết kế tay ngắn mát mẻ. Mà còn nổi bật với chất liệu thun cotton cao cấp, cực mềm mịn, thoáng mát và thấm hút mồ hôi tuyệt đối. Mẹ tranh thủ ghé Babi để "rinh" về cho bé yêu nhà mình nhé!','01/01/2019','MSP025.jpg')
INSERT INTO SanPham VALUES(	N'Quần thun dài trẻ sơ sinh',		3,2,1,2,	N'Chiếc',10,'27000',	N'Giữ ấm cho bé yêu tuyệt đối cùng quần dài sơ sinh cho bé (5-12kg) mẹ nhé! Quần có rất nhiều màu sắc xinh xắn cho mẹ tha hồ lựa chọn nha!','01/01/2019','MSP026.jpg')
INSERT INTO SanPham VALUES(	N'Áo tay dài trẻ sơ sinh',			3,1,1,2,	N'Chiếc',10,'44000',	N'Việc giữ ấm cho bé rất quan trọng trong những ngày mùa Thu - Đông giá lạnh. Hãy để áo trẻ em sơ sinh tay dài giúp mẹ thực hiện điều đó nhé! Bên cạnh đó, áo sơ sinh cho bé còn có nhiều hoạ tiết hình in vô cùng dễ thương và ngộ nghĩnh. Chỉ với 44.000đ, tại sao không!','01/01/2019','MSP027.jpg')
INSERT INTO SanPham VALUES(	N'Quần thun ngắn trẻ sơ sinh',		3,2,1,4,	N'Chiếc',10,'21000',	N'Những chiếc quần thun ngắn trơn màu cho bé sơ sinh được may từ chất liệu vải 100% thun cotton rất mềm mại, êm ái với làn da mỏng manh của trẻ. ','01/01/2019','MSP028.jpg')
INSERT INTO SanPham VALUES(	N'Bộ dài tay trẻ sơ sinh',			3,4,7,4,	N'Bộ',10,'82000',	N'Áo quần thun cotton cho bé sơ sinh cực kỳ co giãn và mềm mại cho làn da của bé, tay trơn phối thân họa tiết kẻ sọc cực đáng yêu. Với thiết kế tay dài, quần dài bo lai đảm bảo giữ ấm cho bé cưng cực tốt trong những ngày trời se lạnh hoặc nằm điều hòa.  ','01/01/2019','MSP029.jpg')
INSERT INTO SanPham VALUES(	N'Áo trẻ sơ sinh dáng bác sĩ',		3,1,7,3,	N'Bộ',10,'42000',	N'Vì làn da và sức khoẻ của các bé sơ sinh rất nhạy cảm, nên việc lựa chọn quần áo sơ sinh với chất liệu tốt cho bé là điều các mẹ rất quan tâm. Babi xin giới thiệu áo tay dài cho bé sơ sinh với chất liệu thun cotton cao cấp, mềm mịn, thấm hút mồ hôi tốt và quan trọng nhất là an toàn 100% với làn da của bé. Các mẹ có thể yên tâm khi cho bé yêu mặc áo sơ sinh của Babi nhé! ','01/01/2019','MSP030.jpg')
GO

--Phụ kiện --
INSERT INTO SanPham VALUES(	N'Balo Trứng Hình Thú Ngộ Nghĩnh Cho Bé',	4,8,8,	2,	N'Chiếc',10,'119000',	N'Balo cho bé kiểu hình thú ngộ nghĩnh, thiết kế nhỏ nhắn và gọn nhẹ, để bé tung tăng đến trường mẫu giáo mỗi ngày nè!','01/01/2019','MSP031.jpg')
INSERT INTO SanPham VALUES(	N'Két sắt Number Bank cho bé tiết kiệm',	4,8,9,	2,	N'Chiếc',10,'275000',	N'Két sắt Number Bank cho bé - Thiết kế thông minh là 1 sản phẩm tiết kiệm tiền khá độc đáo, an toàn và tiện dụng cho bé yêu nhà bạn.','01/01/2019','MSP032.jpg')
INSERT INTO SanPham VALUES(	N'Sữa Tắm Gội Cho Trẻ EM Suave Kids Vòi 3 In 1',4,8,6,3,	N'Chiếc',10,'135000',	N'Sữa tắm gội cho trẻ em hàng sách tay từ Mỹ hương táo và dưa hấu an toàn cho làn da bé, thích hợp cho mọi lứa tuổi.','01/01/2019','MSP033.jpg')
INSERT INTO SanPham VALUES(	N'Túi Vải Bố Canvas Cao Cấp Tiện Lợi Cho Mẹ',	4,8,2,4,	N'Chiếc',10,'79000',	N'Túi vải bố canvas thời trang cao cấp giải pháp thân thiện với môi trường, Babi đặc biệt thiết kế mẫu túi tole đeo vai này dành riêng cho mẹ với nhiều ưu điểm từ chất vải đến từng đường may.','01/01/2019','MSP034.jpg')
INSERT INTO SanPham VALUES(	N'Áo Mưa Trẻ Em Hình Chú Khủng Long',		4,8,6,	1,		N'Chiếc',10,'129000',	N'Áo mưa cho bé hình khủng long dễ thương, thiết kế xinh xắn hình chú khủng long các bé sẽ rất thích Mẹ mua ngay nhé ','01/01/2019','MSP035.jpg')
INSERT INTO SanPham VALUES(	N'Khăn tắm xuất Nhật Cho Cả Gia Đình',		4,8,7,		2,N'Chiếc',10,'45000',	N'Khăn tắm xuất Nhật với sợi khăn mềm mịn, thấm nước nhanh, là sản phẩm không thể thiếu trong mỗi gia đình.','01/01/2019','MSP036.jpg')
INSERT INTO SanPham VALUES(	N'Áo mưa trẻ em bảo vệ sức khỏe của trẻ nhỏ',4,8,6,		3,N'Chiếc',10,'125000',	N'Áo mưa trẻ em nhựa dẻo cao cấp - Sản phẩm thiết thực giúp bảo vệ sức khỏe bé yêu khi mùa mưa đến.','01/01/2019','MSP037.jpg')
INSERT INTO SanPham VALUES(	N'Nón Đi Biển Chống Nắng Cho Bé Sành Điệu',	4,8,8,	4,	N'Chiếc',10,'165000',	N'Nón chống nắng đi biển cho bé từ 5 tuổi trở lên, 4 màu cho mẹ lựa chọn.','01/01/2019','MSP038.jpg')
INSERT INTO SanPham VALUES(	N'Cài Tóc Ngọc Trai Sang Chảnh Cho Bé',		4,8,9,	3,	N'Chiếc',10,'35000',	N'Cài Tóc Ngọc Trai Bé Gái Sang Chảnh - Chút nhấn nhá mái tóc cho cô công chúa điệu đà','01/01/2019','MSP039.jpg')
INSERT INTO SanPham VALUES(	N'Kính Bơi Cao Cấp Cho Bé Có Bao Sành Điệu',4,8,6,	2,	N'Chiếc',10,'49000',	N'Kính bơi cao cấp cho bé từ 2 tuổi đến 5 tuổi giúp bảo vệ đôi mắt để bé dễ dàng tung hoành trong nước.','01/01/2019','MSP040.jpg')
GO

------------------------------------
--Thêm tài khoản

--Thêm dữ liệu vào bảng Thể loại--
INSERT INTO TheLoai VALUES(N'Áo')
INSERT INTO TheLoai VALUES(N'Quần')
INSERT INTO TheLoai VALUES(N'Váy')
INSERT INTO TheLoai VALUES(N'Bộ')
INSERT INTO TheLoai VALUES(N'Mũ')
INSERT INTO TheLoai VALUES(N'Giầy dép')
INSERT INTO TheLoai VALUES(N'Đầm')
INSERT INTO TheLoai VALUES(N'Phụ kiện')
GO




-----------Hiển thị bảng trong csdl------------
select *from ChatLieu
select *from ChiTietDatHang
select *from DoiTuong
select *from DonDatHang
select *from KhachHang
select *from KichCo
select *from NhanVien
select *from NhaSanXuat
select *from SanPham
select *from TaiKhoan
select *from TheLoai
go

------Drop bảng trong csdl--------
drop table DoiTuong
drop table TheLoai
drop table KichCo
drop table ChatLieu
drop table NhaSanXuat
drop table SanPham
drop table KhachHang
drop table ChiTietDatHang
drop table NhanVien
