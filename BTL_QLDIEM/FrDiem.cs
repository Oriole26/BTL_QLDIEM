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
    public partial class FrDiem : Form
    {
        public FrDiem()
        {
            InitializeComponent();
        }
        //Lấy ra danh sách điểm của học sinh
        private DataTable getDiem()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from tblDiem", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tbDiem");

                        ad.Fill(tb);
                        return tb;

                    }
                }
                
            }
        }
        //hiện danh sách điểm của học sinh
        private void hienDSDiem()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("tblDiem_Select", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblDiem");
                        tb.Clear();
                        ad.Fill(tb);

                        DataView v = new DataView(tb);
                        grvDiem.DataSource = v;
                    }
                }
                using (SqlCommand cmd = new SqlCommand("tblLopHoc_SelectMa_Ten", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblLH");

                        ad.Fill(tb);
                        cbmaLH.DisplayMember = "sMaLH";
                        cbmaLH.ValueMember = "sMaLH";
                        cbmaLH.DataSource = tb;
                        txtTenL.DataBindings.Clear();
                        txtTenL.DataBindings.Add("Text", cbmaLH.DataSource, "sTenLH");
                        
                    }

                }
                using(SqlCommand cmd = new SqlCommand("tblMonHoc_SelectMa_Ten",cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");
                        ad.Fill(tb);
                        cbmaMH.DisplayMember = "sMaMH";
                        cbmaMH.ValueMember = "sMaMH";
                        cbmaLH.DataSource = tb;
                        txtTenM.DataBindings.Clear();
                        txtTenM.DataBindings.Add("Text", cbmaMH.DataSource, "sTenLH");
                    }
                }
                
            }
        }

        private void maLH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("hs_by_maLH " + cbmaLH.Text, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblHS");
                        ad.Fill(tb);
                        cbmaHS.DataSource = null;
                        txtTenHS.Text = ""; 
                        cbmaHS.DisplayMember = "sMaHS";
                        cbmaHS.ValueMember = "sMaHS";
                        cbmaHS.DataSource = tb;
                        txtTenHS.DataBindings.Clear();
                        txtTenHS.DataBindings.Add("Text", cbmaHS.DataSource, "sHoTenHS");

                    }

                }
            }
        }

        private void FrDiem_Load(object sender, EventArgs e)
        {
            hienDSDiem();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem" + cbmaLH.Text].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;

            }
        }
    }
}
