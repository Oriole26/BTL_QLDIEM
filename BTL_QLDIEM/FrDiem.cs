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
using System.Xml;

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

        private void maLH_SelectedIndexChanged(object sender, EventArgs e)
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
                        txtTenHS.DataBindings.Clear();
                        //txtDiemM.Text = "";
                        //txtDiemM.DataBindings.Clear();
                        //txtDiem15p.Text = "";
                        //txtDiem15p.DataBindings.Clear();
                        //txtDiem45p.Text = "";
                        //txtDiem45p.DataBindings.Clear();
                        //txtDiemHK.Text = "";
                        //txtDiemHK.DataBindings.Clear();

                        cbmaHS.DisplayMember = "sMaHS";
                        cbmaHS.ValueMember = "sMaHS";
                        cbmaHS.DataSource = tb;
                        grvDiem.DataSource = tb;

                        txtTenHS.DataBindings.Add("Text", cbmaHS.DataSource, "sHoTenHS");
                        //txtDiemM.DataBindings.Add("Text", grvDiem.DataSource, "fDiemMieng");
                        //txtDiem15p.DataBindings.Add("Text", grvDiem.DataSource, "fDiem15P");
                        //txtDiem45p.DataBindings.Add("Text", grvDiem.DataSource, "fDiem45p");
                        //txtDiemHK.DataBindings.Add("Text", grvDiem.DataSource, "fDiemHocKy");
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

                using (SqlCommand cmd = new SqlCommand("select_diem_NHHK", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@maNH", cbNamhoc.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@maHK", cbHocKi.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@maMH", cbmaMH.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@maHS", cbmaHS.Text));

                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblDiem");
                        ad.Fill(tb);
                        if (tb.Rows.Count > 0)
                        {
                            MessageBox.Show(txtTenHS.Text + " đã có điểm môn " + cbmaMH.Text + " tại năm học, học kỳ này!", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            using (SqlCommand Cmd = new SqlCommand("tblDiem_Insert", cnn))
                            {
                                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@smaLH", cbmaLH.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smaHS", cbmaHS.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smaNamHoc", cbNamhoc.SelectedValue));
                                Cmd.Parameters.Add(new SqlParameter("@smaHocKy", cbHocKi.SelectedValue));
                                Cmd.Parameters.Add(new SqlParameter("@smaMH", cbmaMH.SelectedValue));
                                if (txtDiemM.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiemMieng",  DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiemMieng",  float.Parse(txtDiemM.Text)));
                                if (txtDiem15p.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiem15P", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiem15P", float.Parse(txtDiem15p.Text)));
                                if (txtDiem45p.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiem45P", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiem45P", float.Parse(txtDiem45p.Text)));
                                if (txtDiemHK.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", float.Parse(txtDiemHK.Text)));

                                Cmd.ExecuteNonQuery();

                                int tmp = cbmaLH.SelectedIndex;
                                hienDSDiem();
                                cbmaLH.SelectedIndex = tmp;
                            }
                        }
                    }
                }
                cnn.Close();
            }
        }

        private void txtTenM_TextChanged(object sender, EventArgs e)
        {

        }

        private void grvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn sửa chứ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == ConnectionState.Closed) return;

                    using (SqlCommand cmd = new SqlCommand("editDiem", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@maNH", cbNamhoc.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maHK", cbHocKi.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maMH", cbmaMH.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maHS", cbmaHS.Text));
                        if (txtDiemM.Text == "") cmd.Parameters.Add(new SqlParameter("@diemM", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@diemM", float.Parse(txtDiemM.Text)));
                        if (txtDiem15p.Text == "") cmd.Parameters.Add(new SqlParameter("@diem15", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@diem15", float.Parse(txtDiem15p.Text)));
                        if (txtDiem45p.Text == "") cmd.Parameters.Add(new SqlParameter("@diem45", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@diem45", float.Parse(txtDiem45p.Text)));
                        if (txtDiemHK.Text == "") cmd.Parameters.Add(new SqlParameter("@diemHK", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@diemHK", float.Parse(txtDiemHK.Text)));

                        cmd.ExecuteNonQuery();

                        int tmp = cbmaLH.SelectedIndex;
                        hienDSDiem();
                        cbmaLH.SelectedIndex = 0;
                        cbmaLH.SelectedIndex = tmp;
                    }

                    cnn.Close();
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn chứ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes){
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == ConnectionState.Closed) return;

                    using (SqlCommand cmd = new SqlCommand("deleteDiem", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@maNH", cbNamhoc.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maHK", cbHocKi.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maMH", cbmaMH.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@maHS", cbmaHS.Text));

                        cmd.ExecuteNonQuery();

                        int tmp = cbmaLH.SelectedIndex;
                        hienDSDiem();
                        cbmaLH.SelectedIndex = tmp;
                    }

                    cnn.Close();
                }
            }

        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn quay lại trang chủ không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDiemM.Text = "";
            txtDiem15p.Text = "";
            txtDiem45p.Text = "";
            txtDiemHK.Text = "";
        }

        private void txtDiemM_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDiem15p_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDiem45p_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDiemHK_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
