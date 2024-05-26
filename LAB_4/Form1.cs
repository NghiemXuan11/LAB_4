using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_4
{
    public partial class Form1 : Form
    {
        //Nghiêm
        //Khởi tạo kết nối với Database
        //Load form
        //Show danh sách nhân viên thuộc 1 phòng ban
        //Thêm chức năng cho button làm mới.(hàm clear)
        //Xử lý sự kiện khi click 1 dòng dữ liệu trên datagridview
        private string connectionString = "Data Source=11112001NGHIEM\\NGHIEMXUAN;Initial Catalog=DBMS;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private DataSet dataSet;
        private SqlDataAdapter phongBanAdapter;
        private SqlDataAdapter nhanVienAdapter;
        string idPB = "";
        string idNV = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Khoi tao ket noi va tao cac Adapter
            connection = new SqlConnection(connectionString);
            phongBanAdapter = new SqlDataAdapter("Select MaPB as [Mã PB], TenPB as [Tên PB], SoDienThoaiPB as [SĐT], DiaChiPB as [Địa Chỉ] from PHONG_BAN", connection);
            nhanVienAdapter = new SqlDataAdapter("Select MaNV as [Mã NV], TenNV as [Tên NV], DiaChiNV as [Địa chỉ],  SoDienThoaiNV as [SĐT], ChucVuNV as [Chức Vụ], PhongBanID as [PB_ID] from NHAN_VIEN", connection);

            //Tạo DataSet và điền dữ liệu từ cơ sở dữ liệu vào DataSet
            dataSet = new DataSet();
            phongBanAdapter.Fill(dataSet, "PHONG_BAN");
            nhanVienAdapter.Fill(dataSet, "NHAN_VIEN");

            //Tạo DataRelation giữa PHONG_BAN và NHAN_VIEN
            DataRelation relation = new DataRelation("PhongBan_NhanVien", dataSet.Tables["PHONG_BAN"].Columns["Mã PB"], dataSet.Tables["NHAN_VIEN"].Columns["PB_ID"]);
            dataSet.Relations.Add(relation);

            //Bind du lieu vao DataGridView
            dgvPB.DataSource = dataSet.Tables["PHONG_BAN"];
            dgvNV.DataSource = dataSet.Tables["NHAN_VIEN"];

            fill_combobox_PB();
            fill_combobox_SearchPB();
            fill_combobox_FilterPB();
            clear();
        }
        private void clear()
        {
            txtTenPB.Text = "";
            txtDienThoaiPB.Text = "";
            txtDiaChiPB.Text = "";

            txtTenNV.Text = "";
            txtDiaChiNV.Text = "";
            txtDienThoaiNV.Text = "";
            txtChucVu.Text = "";

            idPB = "";
            idNV = "";
            int totalEmployees = dataSet.Tables["NHAN_VIEN"].Rows.Count;
            lblEmployeeCount.Text = totalEmployees.ToString();
        }

        //Show danh sách nhân viên thuộc 1 phòng ban
        private void loadChildData(int rowIndex)
        {
            var parentRow = dataSet.Tables["PHONG_BAN"].Rows[rowIndex];
            var childRows = parentRow.GetChildRows("PhongBan_NhanVien");
            DataTable childTable = dataSet.Tables["NHAN_VIEN"].Clone();
            foreach (var row in childRows)
            {
                childTable.ImportRow(row);
            }
            dgvNV.DataSource = childTable;

            //Hiển thị số lượng nhân viên thuộc phòng ban đó
            lblEmployeeCount.Text = childRows.Length.ToString();
        }
        //Xử lý sự kiện khi click 1 dòng dữ liệu trên bảng Phòng ban
        private void dgvPB_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                idPB = dgvPB.CurrentRow.Cells[0].Value.ToString();
                txtTenPB.Text = dgvPB.CurrentRow.Cells[1].Value.ToString();
                txtDienThoaiPB.Text = dgvPB.CurrentRow.Cells[2].Value.ToString();
                txtDiaChiPB.Text = dgvPB.CurrentRow.Cells[3].Value.ToString();
                //Hàm thực hiện chức năng Show danh sách nhân viên thuộc 1 phòng ban
                loadChildData(dgvPB.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chọn dòng không hợp lệ!");
            }
        }
        //Xử lý sự kiện khi click 1 dòng dữ liệu trên bảng Nhân viên
        private void dgvNV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                idNV = dgvNV.CurrentRow.Cells[0].Value.ToString();
                txtTenNV.Text = dgvNV.CurrentRow.Cells[1].Value.ToString();
                txtDiaChiNV.Text = dgvNV.CurrentRow.Cells[2].Value.ToString();
                txtDienThoaiNV.Text = dgvNV.CurrentRow.Cells[3].Value.ToString();
                txtChucVu.Text = dgvNV.CurrentRow.Cells[4].Value.ToString();
                cbPhongBan.SelectedValue = dgvNV.CurrentRow.Cells[5].Value.ToString();

                // Show the corresponding record in PHONG_BAN
                DataRow[] parentRows = dataSet.Tables["PHONG_BAN"].Select($"[Mã PB] = '{cbPhongBan.SelectedValue}'");
                if (parentRows.Length > 0)
                {
                    idPB = parentRows[0]["Mã PB"].ToString();
                    txtTenPB.Text = parentRows[0]["Tên PB"].ToString();
                    txtDienThoaiPB.Text = parentRows[0]["SĐT"].ToString();
                    txtDiaChiPB.Text = parentRows[0]["Địa Chỉ"].ToString();
                }
                if (parentRows.Length > 0)
                {
                    DataTable tempTable = new DataTable();
                    tempTable.Columns.Add("Mã PB", typeof(string));
                    tempTable.Columns.Add("Tên PB", typeof(string));
                    tempTable.Columns.Add("SĐT", typeof(string));
                    tempTable.Columns.Add("Địa Chỉ", typeof(string));

                    foreach (DataRow row in parentRows)
                    {
                        tempTable.Rows.Add(row["Mã PB"], row["Tên PB"], row["SĐT"], row["Địa Chỉ"]);
                    }

                    dgvPB.DataSource = tempTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chọn dòng không hợp lệ!");
            }
        }
        //Chức năng làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        //=======================================================================================//
        //THẮNG
        //Thêm, Sửa, Xóa Nhân Viên
        //Xử lý combo box

        //Xý lý combo box
        private void fill_combobox_PB()
        {
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from PHONG_BAN", connection);
                DataTable table = new DataTable();
                table.Clear();
                adapter.Fill(table);
                cbPhongBan.DisplayMember = "TenPB";
                cbPhongBan.ValueMember = "MaPB";
                cbPhongBan.DataSource = table;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        //Thêm Nhân Viên
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlCommand command = new SqlCommand("Insert into NHAN_VIEN values (@TenNV, @DiaChiNV, @SoDienThoaiNV, @ChucVuNV, @PhongBanID)", connection);
            //    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
            //    command.Parameters.AddWithValue("@DiaChiNV", txtDiaChiNV.Text);
            //    command.Parameters.AddWithValue("@SoDienThoaiNV", txtDienThoaiNV.Text);
            //    command.Parameters.AddWithValue("@ChucVuNV", txtChucVu.Text);
            //    command.Parameters.AddWithValue("@PhongBanID", cbPhongBan.SelectedValue);
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    MessageBox.Show("Them thanh cong");
            //    Form1_Load(sender, e);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //}
            try
            {
                // Check for duplicate phone number
                SqlCommand checkDuplicateCommand = new SqlCommand("Select Count(*) from NHAN_VIEN where SoDienThoaiNV = @SoDienThoaiNV", connection);
                checkDuplicateCommand.Parameters.AddWithValue("@SoDienThoaiNV", txtDienThoaiNV.Text);
                connection.Open();
                int count = (int)checkDuplicateCommand.ExecuteScalar();
                connection.Close();

                if (count > 0)
                {
                    MessageBox.Show("Nhân viên đã tồn tại. Vui lòng nhập lại thông tin!");
                }
                else
                {
                    SqlCommand command = new SqlCommand("Insert into NHAN_VIEN values (@TenNV, @DiaChiNV, @SoDienThoaiNV, @ChucVuNV, @PhongBanID)", connection);
                    command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                    command.Parameters.AddWithValue("@DiaChiNV", txtDiaChiNV.Text);
                    command.Parameters.AddWithValue("@SoDienThoaiNV", txtDienThoaiNV.Text);
                    command.Parameters.AddWithValue("@ChucVuNV", txtChucVu.Text);
                    command.Parameters.AddWithValue("@PhongBanID", cbPhongBan.SelectedValue);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Thêm thành công");
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        // Sửa Nhân Viên
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("Update NHAN_VIEN set TenNV = @TenNV, DiaChiNV = @DiaChiNV, SoDienThoaiNV = @SoDienThoaiNV, ChucVuNV = @ChucVuNV, PhongBanID = @PhongBanID where MaNV = @MaNV ", connection);
                //id missing
                command.Parameters.AddWithValue("@MaNV", idNV);
                command.Parameters.AddWithValue("@TenNV", txtTenNV.Text);
                command.Parameters.AddWithValue("@DiaChiNV", txtDiaChiNV.Text);
                command.Parameters.AddWithValue("@SoDienThoaiNV", txtDienThoaiNV.Text);
                command.Parameters.AddWithValue("@ChucVuNV", txtChucVu.Text);
                command.Parameters.AddWithValue("@PhongBanID", cbPhongBan.SelectedValue);
                //Phong Ban
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Đã cập nhập");
                Form1_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Xóa Nhân Viên
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("Delete from NHAN_VIEN where MaNV = @MaNV ", connection);
                command.Parameters.AddWithValue("@MaNV", idNV);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Đã xóa");
                Form1_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //=======================================================================================//
        //Sâm
        //Thiết kế giao diện form
        //Thêm, Sửa, Xóa Phòng Ban


        //Thêm thông tin phòng ban
        private void btnThemPB_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    command = new SqlCommand("Insert into PHONG_BAN values (@TenPB, @SoDienThoaiPB,@DiaChiPB)", connection);
            //    command.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
            //    command.Parameters.AddWithValue("@SoDienThoaiPB", txtDienThoaiPB.Text);
            //    command.Parameters.AddWithValue("@DiaChiPB", txtDiaChiPB.Text);
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //    MessageBox.Show("Thêm thành công");
            //    Form1_Load(sender, e);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    connection.Close();
            //}
            try
            {
                // Check for duplicate department name
                SqlCommand checkDuplicateCommand = new SqlCommand("Select Count(*) from PHONG_BAN where TenPB = @TenPB", connection);
                checkDuplicateCommand.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
                connection.Open();
                int count = (int)checkDuplicateCommand.ExecuteScalar();
                connection.Close();

                if (count > 0)
                {
                    MessageBox.Show("Tên phòng ban đã tồn tại. Vui lòng chọn tên khác.");
                }
                else
                {
                    command = new SqlCommand("Insert into PHONG_BAN values (@TenPB, @SoDienThoaiPB, @DiaChiPB)", connection);
                    command.Parameters.AddWithValue("@TenPB", txtTenPB.Text);
                    command.Parameters.AddWithValue("@SoDienThoaiPB", txtDienThoaiPB.Text);
                    command.Parameters.AddWithValue("@DiaChiPB", txtDiaChiPB.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Thêm thành công");
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Sửa thông tin phòng ban





        //Xóa thông tin phòng ban


        
        
        
        //Lọc nhân viên theo Phòng ban
        private void fill_combobox_SearchPB()
        {
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from PHONG_BAN", connection);
                DataTable table = new DataTable();
                table.Clear();
                adapter.Fill(table);

                // Thêm item "Tất cả" vào combobox
                DataRow allRow = table.NewRow();
                allRow["MaPB"] = DBNull.Value;
                allRow["TenPB"] = "Tất cả";
                table.Rows.InsertAt(allRow, 0);

                cbSearchPB.DisplayMember = "TenPB";
                cbSearchPB.ValueMember = "MaPB";
                cbSearchPB.DataSource = table;
                cbSearchPB.SelectedIndexChanged += new EventHandler(cbSearchPB_SelectedIndexChanged);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }





        private void cbSearchPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchPB.SelectedValue != null)
            {
                if (cbSearchPB.SelectedValue is DBNull)
                {
                    // Hiển thị lại toàn bộ danh sách nhân viên
                    dgvNV.DataSource = dataSet.Tables["NHAN_VIEN"];
                    int totalEmployees = dataSet.Tables["NHAN_VIEN"].Rows.Count;
                    lblEmployeeCount.Text = totalEmployees.ToString();
                }
                else
                {
                    string selectedPBID = cbSearchPB.SelectedValue.ToString();
                    DataRow[] rows = dataSet.Tables["NHAN_VIEN"].Select($"[PB_ID] = '{selectedPBID}'");
                    DataTable tempTable = dataSet.Tables["NHAN_VIEN"].Clone();
                    int CountE = 0;
                    foreach (DataRow row in rows)
                    {
                        tempTable.ImportRow(row);
                        CountE += 1;
                    }
                    lblEmployeeCount.Text = CountE.ToString();
                    dgvNV.DataSource = tempTable;
                }
            }
        }

        //Lọc danh sách phòng ban
        private void fill_combobox_FilterPB()
        {
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from PHONG_BAN", connection);
                DataTable table = new DataTable();
                table.Clear();
                adapter.Fill(table);
                DataRow allRow = table.NewRow();
                allRow["MaPB"] = DBNull.Value;
                allRow["TenPB"] = "Tất cả";
                table.Rows.InsertAt(allRow, 0);
                cbFilterPB.DisplayMember = "TenPB";
                cbFilterPB.ValueMember = "MaPB";
                cbFilterPB.DataSource = table;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void cbFilterPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lọc danh sách phòng ban theo lựa chọn trong combo box
            if (cbFilterPB.SelectedValue == DBNull.Value)
            {
                dgvPB.DataSource = dataSet.Tables["PHONG_BAN"];
            }
            else
            {
                string filterValue = cbFilterPB.SelectedValue.ToString();
                DataRow[] filteredRows = dataSet.Tables["PHONG_BAN"].Select($"[Mã PB] = '{filterValue}'");

                if (filteredRows.Length > 0)
                {
                    DataTable tempTable = new DataTable();
                    tempTable.Columns.Add("Mã PB", typeof(string));
                    tempTable.Columns.Add("Tên PB", typeof(string));
                    tempTable.Columns.Add("SĐT", typeof(string));
                    tempTable.Columns.Add("Địa Chỉ", typeof(string));

                    foreach (DataRow row in filteredRows)
                    {
                        tempTable.Rows.Add(row["Mã PB"], row["Tên PB"], row["SĐT"], row["Địa Chỉ"]);
                    }

                    dgvPB.DataSource = tempTable;
                }
            }
        }
    }
}
