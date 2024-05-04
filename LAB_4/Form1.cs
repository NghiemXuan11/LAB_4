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
        //new push
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

        }

        //Thắng
        //Thêm, Sửa, Xóa Nhân Viên

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
    }
}
