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
    public partial class FrDiem1HS : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

        public FrDiem1HS()
        {
            InitializeComponent();
        }

        private void FrDiem1HS_Load(object sender, EventArgs e)
        {
            //hiện năm học
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_MaTenNH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblNH");

                        ad.Fill(tb);


                        cbNamhoc.DisplayMember = "sTenNamHoc";
                        cbNamhoc.ValueMember = "sMaNamHoc";

                        cbNamhoc.DataSource = tb;
                    }
                    using (SqlCommand Cmd = new SqlCommand("Select*from tblHocKy", cnn))
                    {
                        Cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(Cmd))
                        {
                            DataTable tb = new DataTable("tblHK");

                            ad.Fill(tb);


                            cbNamhoc.DisplayMember = "sTenHocKy";
                            cbNamhoc.ValueMember = "sMaHocKy";

                            cbNamhoc.DataSource = tb;
                        }
                    }
                    using (SqlCommand Cmd = new SqlCommand("prSelect_MaMHTenMH", cnn))
                    {
                        Cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(Cmd))
                        {
                            DataTable tb = new DataTable("tblMH");

                            ad.Fill(tb);


                            cbNamhoc.DisplayMember = "sTenMH";
                            cbNamhoc.ValueMember = "sMaMH";

                            cbNamhoc.DataSource = tb;
                        }
                    }
                }
            }
        }
    }
}
