using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            phongBanAdapter = new SqlDataAdapter("Select * from PHONG_BAN", connection);
            nhanVienAdapter = new SqlDataAdapter("Select * from NHAN_VIEN", connection);

            //Tạo DataSet và điền dữ liệu từ cơ sở dữ liệu vào DataSet
            dataSet = new DataSet();
            phongBanAdapter.Fill(dataSet, "PHONG_BAN");
            nhanVienAdapter.Fill(dataSet, "NHAN_VIEN");

            //Tạo DataRelation giữa PHONG_BAN và NHAN_VIEN
            DataRelation relation = new DataRelation("PhongBan_NhanVien", dataSet.Tables["PHONG_BAN"].Columns["MaPB"], dataSet.Tables["NHAN_VIEN"].Columns["PhongBanID"]);
            dataSet.Relations.Add(relation);

            //Bind du lieu vao DataGridView
            dgvPB.DataSource = dataSet.Tables["PHONG_BAN"];
            dgvNV.DataSource = dataSet.Tables["NHAN_VIEN"];

            fill_combobox_PB();
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
        }
        //Xử lý sự kiện khi click 1 dòng dữ liệu trên bảng Phòng ban
        private void dgvPB_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idPB = dgvPB.CurrentRow.Cells[0].Value.ToString();
            txtTenPB.Text = dgvPB.CurrentRow.Cells[1].Value.ToString();
            txtDienThoaiPB.Text = dgvPB.CurrentRow.Cells[2].Value.ToString();
            txtDiaChiPB.Text = dgvPB.CurrentRow.Cells[3].Value.ToString();
            //Hàm thực hiện chức năng Show danh sách nhân viên thuộc 1 phòng ban
            loadChildData(dgvPB.CurrentRow.Index);
        }
        //Xử lý sự kiện khi click 1 dòng dữ liệu trên bảng Nhân viên
        private void dgvNV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idNV = dgvNV.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dgvNV.CurrentRow.Cells[1].Value.ToString();
            txtDiaChiNV.Text = dgvNV.CurrentRow.Cells[2].Value.ToString();
            txtDienThoaiNV.Text = dgvNV.CurrentRow.Cells[3].Value.ToString();
            txtChucVu.Text = dgvNV.CurrentRow.Cells[4].Value.ToString();
            cbPhongBan.SelectedValue = dgvNV.CurrentRow.Cells[5].Value.ToString();
        }
        //Chức năng làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }




        //=======================================================================================//
        //SÂM
        //Thiết kế giao diện form
        //Thêm, Sửa, Xóa Phòng Ban







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
            try
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
                MessageBox.Show("Them thanh cong");
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

    }
}
