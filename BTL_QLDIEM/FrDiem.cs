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
using System.Net;

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
                using (SqlCommand cmd = new SqlCommand("tblMonHoc_SelectMa_Ten", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");
                        ad.Fill(tb);
                        cbmaMH.DisplayMember = "sMaMH";
                        cbmaMH.ValueMember = "sMaMH";
                        cbmaMH.DataSource = tb;
                        txtTenM.DataBindings.Clear();
                        txtTenM.DataBindings.Add("Text", cbmaMH.DataSource, "sTenMH");
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select_all_nh", cnn))
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
                }

                using (SqlCommand cmd = new SqlCommand("select_all_hk", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblHK");
                        ad.Fill(tb);
                        cbHocKi.DisplayMember = "sTenHocKy";
                        cbHocKi.ValueMember = "sMaHocKy";
                        cbHocKi.DataSource = tb;
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
                        grvDiem.DataSource = tb;
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
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed) return;

                using (SqlCommand Cmd = new SqlCommand("tblDiem_Insert", cnn))
                {
                    Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@smaLH", cbmaLH.Text));
                    Cmd.Parameters.Add(new SqlParameter("@smaHS", cbmaHS.Text));
                    Cmd.Parameters.Add(new SqlParameter("@smaNamHoc", cbNamhoc.SelectedValue));
                    Cmd.Parameters.Add(new SqlParameter("@smaHocKy", cbHocKi.SelectedValue));
                    Cmd.Parameters.Add(new SqlParameter("@smaMH", cbmaMH.Text));
                    Cmd.Parameters.Add(new SqlParameter("@fdiemMieng", float.Parse(txtDiemM.Text)));
                    Cmd.Parameters.Add(new SqlParameter("@fdiem15P", float.Parse(txtDiem15p.Text)));
                    Cmd.Parameters.Add(new SqlParameter("@fdiem45P", float.Parse(txtDiem45p.Text)));
                    Cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", float.Parse(txtDiemHK.Text)));


                    Cmd.ExecuteNonQuery();

                    hienDSDiem();
                }
            }
        }

        private void txtTenM_TextChanged(object sender, EventArgs e)
        {

        }

        private void grvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
