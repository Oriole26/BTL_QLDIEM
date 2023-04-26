using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace BTL_QLDIEM
{
    public partial class FrLopHoc : Form
    {
        public FrLopHoc()
        {
            InitializeComponent();
        }
        private DataTable getLopHoc()
        {
            string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(str))
            {
                using(SqlCommand cmd = new SqlCommand("Select*from tblLopHoc",cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using(SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblLH");
                        ad.Fill(tb);
                        return tb;
                    }
                }
            }
        }
        //Hiện danh sách lớp học ra gridview
            private void hienDSLH()
        {
            string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(str))
            {
                using(SqlCommand cmd = new SqlCommand("tblLopHoc_Select",cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using(SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblLH");
                        tb.Clear();
                        ad.Fill(tb);

                        DataView v = new DataView(tb);
                        grvLH.DataSource = v;

                        
                    }
                }
                //Lấy mã Khối lớp từ bảng khối lớp
                using (SqlCommand cmd = new SqlCommand("tblKhoiLop_Ma", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblKL");

                        ad.Fill(tb);

                     
                        cbMaKL.DisplayMember = "sMaKhoiLop";
                        cbMaKL.ValueMember = "sMaKhoiLop";
                      
                        cbMaKL.DataSource = tb;
                    }
                }
                //Lấy mã GV từ bảng GV
                using (SqlCommand cmd = new SqlCommand("tblGiaoVien_Ma", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblGV");

                        ad.Fill(tb);

                        cbMaGV.DisplayMember = "sMaGV";
                        cbMaGV.ValueMember = "sMaGV";
                        cbMaGV.DataSource = tb;
                       
                    }
                }
            }
        }
        private void FrLopHoc_Load(object sender, EventArgs e)
        {
            hienDSLH();
        }
    }
}
