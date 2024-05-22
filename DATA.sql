create database DBMS;
use DBMS;

create table PHONG_BAN(
MaPB int primary key identity,
TenPB nvarchar(50),
SoDienThoaiPB nvarchar(50),
DiaChiPB nvarchar(50)
);
create table NHAN_VIEN(
MaNV int primary key identity,
TenNV nvarchar(50),
DiaChiNV nvarchar(50),
SoDienThoaiNV nvarchar(50),
ChucVuNV nvarchar(20),
PhongBanID int foreign key references PHONG_BAN(MaPB) on delete cascade
);

INSERT INTO PHONG_BAN (TenPB, SoDienThoaiPB, DiaChiPB)
VALUES 
    (N'Phòng Kế toán', '0123456789', N'123 Đường ABC'),
    (N'Phòng Kinh doanh', '0987654321', N'456 Đường XYZ'),
    (N'Phòng Nhân sự', '0369841752', N'789 Đường LMN'),
    (N'Phòng Marketing', '0932164875', N'321 Đường PQR'),
    (N'Phòng IT', '0654879321', N'654 Đường DEF'),
    (N'Phòng Hành chính', '0975318642', N'987 Đường GHI'),
    (N'Phòng Sản xuất', '0123456789', N'147 Đường MNO'),
    (N'Phòng Dịch vụ', '0987654321', N'258 Đường STU'),
    (N'Phòng Kỹ thuật', '0369841752', N'369 Đường VWX'),
    (N'Phòng Tài chính', '0932164875', N'741 Đường YZ'),
    (N'Phòng Logistic', '0123456789', N'753 Đường ABC'),
    (N'NPhòng Hỗ trợ', '0987654321', N'159 Đường XYZ'),
    (N'Phòng Nghiên cứu', '0369841752', N'357 Đường LMN'),
    (N'Phòng Đào tạo', '0932164875', N'951 Đường PQR'),
    (N'Phòng Hậu cần', '0654879321', N'357 Đường DEF'),
    (N'Phòng Kiểm tra', '0975318642', N'159 Đường GHI'),
    (N'Phòng Sáng tạo', '0123456789', N'753 Đường MNO'),
    (N'Phòng Phát triển', '0987654321', N'357 Đường STU'),
    (N'Phòng Tiếp thị', '0369841752', N'951 Đường VWX'),
    (N'Phòng Điều hành', '0932164875', N'753 Đường YZ');

INSERT INTO NHAN_VIEN (TenNV, DiaChiNV, SoDienThoaiNV, ChucVuNV, PhongBanID)
VALUES 
    (N'Nguyễn Văn A', N'1A Nguyễn Du, Q1, TP.HCM', '0123456789', N'Nhân viên kế toán', 1),
    (N'Trần Thị B', N'2B Lê Lợi, Q2, TP.HCM', '0987654321', N'Nhân viên kinh doanh', 2),
    (N'Lê Văn C', N'3C Nguyễn Thị Minh Khai, Q3, TP.HCM', '0369841752', N'Nhân viên nhân sự', 3),
    (N'Hoàng Thị D', N'4D Bà Huyện Thanh Quan, Q4, TP.HCM', '0932164875', N'Nhân viên marketing', 4),
    (N'Phạm Văn E', N'5E Nguyễn Văn Linh, Q5, TP.HCM', '0654879321', N'Nhân viên IT', 5),
    (N'Nguyễn Thị F', N'6F Nguyễn Hữu Thọ, Q7, TP.HCM', '0975318642', N'Nhân viên hành chính', 6),
    (N'Trần Văn G', N'7G Lý Thường Kiệt, Q8, TP.HCM', '0123456789', N'Nhân viên sản xuất', 7),
    (N'Lê Thị H', N'8H Nguyễn Trãi, Q9, TP.HCM', '0987654321', N'Nhân viên dịch vụ', 8),
    (N'Hoàng Văn I', N'9I Nguyễn Văn Cừ, Q10, TP.HCM', '0369841752', N'Nhân viên kỹ thuật', 9),
    (N'Trần Văn K', N'10K Lê Lai, Q11, TP.HCM', '0932164875', N'Nhân viên tài chính', 10),
    (N'Lê Văn L', N'11L Nguyễn Đình Chiểu, Q12, TP.HCM', '0654879321', N'Nhân viên logistic', 11),
    (N'Phạm Thị M', N'12M Lý Tự Trọng, Q1, TP.HCM', '0975318642', N'Nhân viên hỗ trợ', 12),
    (N'Nguyễn Văn N', N'13N Trần Hưng Đạo, Q2, TP.HCM', '0123456789', N'Nhân viên nghiên cứu', 13),
    (N'Trần Thị O', N'14O Lê Quang Định, Q3, TP.HCM', '0987654321', N'Nhân viên đào tạo', 14),
    (N'Lê Văn P', N'15P Nguyễn Thị Minh Khai, Q4, TP.HCM', '0369841752', N'Nhân viên hậu cần', 15),
    (N'Nguyễn Thị Q', N'16Q Bà Huyện Thanh Quan, Q5, TP.HCM', '0932164875', N'Nhân viên kiểm tra', 16),
    (N'Trần Văn R', N'17R Nguyễn Văn Linh, Q6, TP.HCM', '0654879321', N'Nhân viên sáng tạo', 17),
    (N'Phạm Thị S', N'18S Lê Lợi, Q7, TP.HCM', '0975318642', N'Nhân viên phát triển', 18),
    (N'Lê Văn T', N'19T Nguyễn Huệ, Q8, TP.HCM', '0123456789', N'Nhân viên tiếp thị', 19),
    (N'Nguyễn Thị U', N'20U Nguyễn Thị Minh Khai, Q9, TP.HCM', '0987654321', N'Nhân viên điều hành', 20), 
    (N'Trần Thị V', N'21V Lý Chính Thắng, Q10, TP.HCM', '0369841752', N'Nhân viên logistic', 11),
    (N'Nguyễn Văn W', N'22W Phan Văn Trị, Q11, TP.HCM', '0932164875', N'Nhân viên hỗ trợ', 12),
    (N'Lê Thị X', N'23X Nguyễn Đình Chiểu, Q12, TP.HCM', '0123456789', N'Nhân viên nghiên cứu', 13),
    (N'Hoàng Văn Y', N'24Y Lý Thường Kiệt, Q1, TP.HCM', '0987654321', N'Nhân viên đào tạo', 14),
    (N'Trần Văn Z', N'25Z Trần Hưng Đạo, Q2, TP.HCM', '0369841752', N'Nhân viên hậu cần', 15),
    (N'Nguyễn Thị M', N'26M Bà Huyện Thanh Quan, Q3, TP.HCM', '0932164875', N'Nhân viên kiểm tra', 16),
    (N'Phạm Văn N', N'27N Nguyễn Văn Linh, Q4, TP.HCM', '0654879321', N'Nhân viên sáng tạo', 17),
    (N'Lê Thị O', N'28O Lê Lợi, Q5, TP.HCM', '0975318642', N'Nhân viên phát triển', 18),
    (N'Hoàng Văn P', N'29P Nguyễn Huệ, Q6, TP.HCM', '0123456789', N'Nhân viên tiếp thị', 19),
    (N'Trần Thị Q', N'30Q Nguyễn Thị Minh Khai, Q7, TP.HCM', '0987654321', N'Nhân viên điều hành', 20),
	(N'Nguyễn Văn R', N'31R Nguyễn Thị Minh Khai, Q8, TP.HCM', '0369841752', N'Nhân viên logistic', 11),
    (N'Trần Thị S', N'32S Lê Lợi, Q9, TP.HCM', '0932164875', N'Nhân viên hỗ trợ', 12),
    (N'Lê Văn T', N'33T Nguyễn Huệ, Q10, TP.HCM', '0123456789', N'Nhân viên nghiên cứu', 13),
    (N'Hoàng Thị U', N'34U Nguyễn Đình Chiểu, Q11, TP.HCM', '0987654321', N'Nhân viên đào tạo', 14),
    (N'Phạm Văn V', N'35V Phan Văn Trị, Q12, TP.HCM', '0369841752', N'Nhân viên hậu cần', 15),
    (N'Trần Thị X', N'36X Lý Chính Thắng, Q1, TP.HCM', '0932164875', N'Nhân viên kiểm tra', 16),
    (N'Lê Văn Y', N'37Y Trần Hưng Đạo, Q2, TP.HCM', '0654879321', N'Nhân viên sáng tạo', 17),
    (N'Hoàng Thị Z', N'38Z Lý Thường Kiệt, Q3, TP.HCM', '0975318642', N'Nhân viên phát triển', 18),
    (N'Phạm Thị A1', N'39A Bà Huyện Thanh Quan, Q4, TP.HCM', '0123456789', N'Nhân viên tiếp thị', 19),
    (N'Nguyễn Văn B1', N'40B Nguyễn Văn Linh, Q5, TP.HCM', '0987654321', N'Nhân viên điều hành', 20);


