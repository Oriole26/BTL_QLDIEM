using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_QLDIEM
{
    public partial class FrDiem : Form
    {
        public FrDiem()
        {
            InitializeComponent();
        }
         string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

        //hiện danh sách điểm của học sinh
        private void hienDSDiem()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_Diem", cnn))
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
                using (SqlCommand cmd = new SqlCommand("prSelect_MaLHTenLH", cnn))
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
                using (SqlCommand cmd = new SqlCommand("prSelect_MaMHTenMH", cnn))
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

                using (SqlCommand cmd = new SqlCommand("prSelect_AllNH", cnn))
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

                using (SqlCommand cmd = new SqlCommand("prSelect_AllHK", cnn))
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
                using (SqlCommand cmd = new SqlCommand("prSelect_MaTenHS_byLH " + cbmaLH.Text, cnn))
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

                using (SqlCommand cmd = new SqlCommand("prSelect_diemNH_HK", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smanh", cbNamhoc.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@smahk", cbHocKi.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@smamh", cbmaMH.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@smahs", cbmaHS.Text));

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
                            using (SqlCommand Cmd = new SqlCommand("prInsert_Diem", cnn))
                            {
                                Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@smalh", cbmaLH.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smahs", cbmaHS.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smanh", cbNamhoc.SelectedValue));
                                Cmd.Parameters.Add(new SqlParameter("@smahk", cbHocKi.SelectedValue));
                                Cmd.Parameters.Add(new SqlParameter("@smamh", cbmaMH.SelectedValue));
                                if (txtDiemM.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiemMieng",  DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiemMieng",  float.Parse(txtDiemM.Text)));
                                if (txtDiem15p.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiem15P", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiem15P", float.Parse(txtDiem15p.Text)));
                                if (txtDiem45p.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiem45P", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiem45P", float.Parse(txtDiem45p.Text)));
                                if (txtDiemHK.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", float.Parse(txtDiemHK.Text)));
                                if (txtDiemHK.Text == "") Cmd.Parameters.Add(new SqlParameter("@fdiemTBHK", DBNull.Value));
                                else Cmd.Parameters.Add(new SqlParameter("@fdiemTBHK", float.Parse(txtDiemTB.Text)));
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

      

        private void grvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvDiem.Rows[e.RowIndex];
            cbmaHS.Text = Convert.ToString(row.Cells["sMaHS"].Value);
            txtTenHS.Text = Convert.ToString(row.Cells["sHotenHS"].Value);
            cbNamhoc.Text = Convert.ToString(row.Cells["sMaNamHoc"].Value);
            cbHocKi.Text = Convert.ToString(row.Cells["sMaHocKy"].Value);
            cbmaMH.Text = Convert.ToString(row.Cells["sMaMH"].Value);
            txtDiemM.Text = Convert.ToString(row.Cells["fDiemMieng"].Value);
            txtDiem15p.Text = Convert.ToString(row.Cells["fDiem15P"].Value);
            txtDiem45p.Text = Convert.ToString(row.Cells["fDiem45P"].Value);
            txtDiemHK.Text = Convert.ToString(row.Cells["fDiemHocKy"].Value);
            txtDiemTB.Text = Convert.ToString(row.Cells["fDiemTBHK"].Value);
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

                    using (SqlCommand cmd = new SqlCommand("prUpdate_Diem", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@smalh", cbmaLH.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smahs", cbmaHS.Text));
                        cmd.Parameters.Add(new SqlParameter("@smanh", cbNamhoc.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smahk", cbHocKi.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smamh", cbmaMH.SelectedValue));
                        if (txtDiemM.Text == "") cmd.Parameters.Add(new SqlParameter("@fdiemMieng", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@fdiemMieng", float.Parse(txtDiemM.Text)));
                        if (txtDiem15p.Text == "") cmd.Parameters.Add(new SqlParameter("@fdiem15P", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@fdiem15P", float.Parse(txtDiem15p.Text)));
                        if (txtDiem45p.Text == "") cmd.Parameters.Add(new SqlParameter("@fdiem45P", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@fdiem45P", float.Parse(txtDiem45p.Text)));
                        if (txtDiemHK.Text == "") cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@fdiemHocKy", float.Parse(txtDiemHK.Text)));
                        if (txtDiemHK.Text == "") cmd.Parameters.Add(new SqlParameter("@fdiemTBHK", DBNull.Value));
                        else cmd.Parameters.Add(new SqlParameter("@fdiemTBHK", float.Parse(txtDiemTB.Text)));
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
            if (MessageBox.Show("Bạn chắc chắn muốn xoá chứ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes){
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == ConnectionState.Closed) return;

                    using (SqlCommand cmd = new SqlCommand("prDelete_Diem", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@smanh", cbNamhoc.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smahk", cbHocKi.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smamh", cbmaMH.SelectedValue));
                        cmd.Parameters.Add(new SqlParameter("@smahs", cbmaHS.Text));

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

        private void txtDiemHK_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prDiemtheoLop", cnn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smalh", cbmaLH.Text.Trim()));

                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        ad.Fill(tbl);
                        grvDiem.DataSource = tbl;
                        crpDiem baocao = new crpDiem();
                        baocao.SetDataSource(tbl);
                        dtDiem bcDSGV = new dtDiem();
                        frDSDiem DSGV = new frDSDiem();
                        DSGV.crystalReportViewer1.ReportSource = baocao;
                        DSGV.ShowDialog();
                    }
                }
            }
        }

        private void txtDiem_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDiemM.Text) && !String.IsNullOrEmpty(txtDiem15p.Text) 
                && !String.IsNullOrEmpty(txtDiem45p.Text) && !String.IsNullOrEmpty(txtDiemHK.Text))
            {
                float diemM = float.Parse(txtDiemM.Text);
                float diem15p = float.Parse(txtDiem15p.Text);
                float diem45p = float.Parse(txtDiem45p.Text);
                float diemHK = float.Parse(txtDiemHK.Text);

                txtDiemTB.Text = (((diemM + diem15p) + 2 * diem45p + 3 * diemHK) / 7).ToString();
            } else
            {
                txtDiemTB.Text = "";
            }
        }

      
    }
}
