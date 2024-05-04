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
    }
}
