# Tên dự án

Xây dựng ứng dụng với Winform (C#) để hiện thực hóa các thao tác thêm, xóa, sửa với mối quan hệ 1-N trong cơ sở dữ liệu.

## Mô tả 

Hiện thực một giao diện trên phần mềm lập trình (sử dụng Python hoặc C#) để người dùng có thể THÊM MỚI, CHỈNH SỬA hoặc XÓA dữ liệu từ 2 thực thể ở phần đặc tả.<br>
Lưu ý rằng, khi xóa thực thể CHA (phía quan hệ MỘT), các phần tử bên thực thể N cũng sẽ bị xóa theo.

## Đặc tả yêu cầu

Công ty A cần lưu thông tin của PHÒNG BAN (bao gồm mã phòng ban, Tên phòng ban, số điện thoại liên hệ, địa chỉ) và các NHÂN VIÊN làm việc trong phòng ban đó (thông tin của nhân viên bao gồm mã nhân viên, Họ và Tên, Địa chỉ, Số điện thoại liên hệ, Chức vụ). Mỗi NHÂN VIÊN chỉ làm việc cho MỘT PHÒNG BAN, một PHÒNG BAN bao gồm nhiều NHÂN VIÊN.

## Chi tiết

### Công cụ sử dụng

* OS: Windows 10, 11
* IDE: Microsoft Visual Studio 2022
* Ngôn ngữ lập trình: C#
* FrameWork: .NET Framework 4.8.1

### Cài đặt

* Bước 1: Clone Project về máy.
* Bước 2: Chạy file query data.sql khởi tạo Database.
* Bước 3: Tại dòng lệnh chứa chuối connection String trong source code đổi tên Server thành tên máy cá nhân.
* Bước 4: Thực thi và thử nghiệm các chức năng.

### Thực thi chương trình

* Trên màn hình giao diện sẽ bao gồm các chức năng sau:<br>
  - Thêm, sửa, xóa phòng ban.<br>
  - Thêm, sửa, xoa nhân viên.<br>
  - Show toàn bộ nhân viên thuộc 1 phòng ban khi click vào dòng dữ liệu bảng phòng ban.<br>
  - Show phòng ban mà nhân viên bất kì thuộc về khi click vào dòng dữ liệu bảng nhân viên.<br>
  - Chức năng làm mới (Refresh) giúp thiết lập dữ liệu về trạng thái ban đầu.<br>
  
## Thành viên nhóm

* Nguyễn Trương Xuân Nghiêm - MSSV: 2151010246<br>
Github: [@NghiemXuan11](https://github.com/NghiemXuan11)<br>
* Nguyễn Hoàng Thắng - MSSV: 2151012009<br>
Github: [@hoangthang0403](https://github.com/hoangthang0403)<br>
* Tiển Chí Sâm - MSSV: 2151010319<br>
Github: [@SamCh1](https://github.com/SamCh1)

