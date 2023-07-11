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
    public partial class Hocsinh : Form
    {
        public Hocsinh()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

        

        //hiện danh sách học sinh
        private void hienDSHS()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_HS", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblHS");
                        tb.Clear();
                        ad.Fill(tb);
                        DataView v = new DataView(tb);
                        grvHS.DataSource = v;
                        
                    }
                }
                using (SqlCommand cmd = new SqlCommand("prSelect_MaLH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblLH");

                        ad.Fill(tb);

                        cbMaLH.DisplayMember = "sTenLH";
                        cbMaLH.ValueMember = "sMaLH";
                        cbMaLH.DataSource = tb;
                    }
                }

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hienDSHS();
            grvHS.Columns[0].HeaderText = "Mã HS";
            grvHS.Columns[1].HeaderText = "Tên HS";
            grvHS.Columns[2].HeaderText = "Ngày sinh";
            grvHS.Columns[3].HeaderText = "Giới tính";
            grvHS.Columns[4].HeaderText = "Địa chỉ";
            grvHS.Columns[5].HeaderText = "Dân tộc";
            grvHS.Columns[6].HeaderText = "Mã LH";



        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bool check = true;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using(SqlCommand cmd = new SqlCommand("prChecktrung_MaHS", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smahs", txtMa.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if(count > 0)
                    {
                        MessageBox.Show("Trùng mã!", "Thông báo", MessageBoxButtons.OK);
                    }
                    if (txtMa.Text.Trim().Length == 0)
                    {
                        errorProviderHS.SetError(txtMa, "Vui lòng nhập mã học sinh ! ");
                        check = false;
                    }
                    else errorProviderHS.SetError(txtMa, "");


                    if (txtTen.Text.Trim().Length == 0)
                    {
                        errorProviderHS.SetError(txtTen, "Vui lòng nhập tên học sinh!");
                        check = false;
                    }
                    else errorProviderHS.SetError(txtTen, "");

                    if (cbGT.Text.Trim().Length == 0)
                    {
                        errorProviderHS.SetError(cbGT, "Vui lòng chọn giới tính !");
                        check = false;
                    }
                    else errorProviderHS.SetError(cbGT, "");

                    if (txtDC.Text.Trim().Length == 0)
                    {
                        errorProviderHS.SetError(txtDC, "Vui lòng nhập địa chỉ!");
                        check = false;
                    }
                    else errorProviderHS.SetError(txtDC, "");

                    if (txtDT.Text.Trim().Length == 0)
                    {
                        errorProviderHS.SetError(txtDT, "Vui lòng nhập dân tộc !");
                        check = false;
                    }
                    else errorProviderHS.SetError(txtDT, "");
                    

                   //kiểm tra ngày sinh k được vượt quá ngày hiện tại


                   
                    if (check)
                    {
                        using (SqlCommand Cmd = new SqlCommand("prInsert_HS", cnn))
                        {
                            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@smahs", txtMa.Text));
                            Cmd.Parameters.Add(new SqlParameter("@stenhs", txtTen.Text));
                            Cmd.Parameters.Add(new SqlParameter("@dns", dtNS.Value.Date));
                            Cmd.Parameters.Add(new SqlParameter("@sgioitinh", cbGT.Text));
                            Cmd.Parameters.Add(new SqlParameter("@sdiachi", txtDC.Text));
                            Cmd.Parameters.Add(new SqlParameter("@sdantoc", txtDT.Text));
                            Cmd.Parameters.Add(new SqlParameter("@smalh", cbMaLH.Text));
                            Cmd.ExecuteNonQuery();
                            hienDSHS();
                        }
                    }

                   
                    
                   
                }
            }

        }
        private void grvHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvHS.Rows[e.RowIndex];
            txtMa.Text = Convert.ToString(row.Cells["sMaHS"].Value);
            txtTen.Text = Convert.ToString(row.Cells["sHoTenHS"].Value);
            dtNS.Text = Convert.ToString(row.Cells["dNgaysinh"].Value);
            cbGT.Text = Convert.ToString(row.Cells["sGioitinh"].Value);
            txtDC.Text = Convert.ToString(row.Cells["sDiachi"].Value);
            txtDT.Text = Convert.ToString(row.Cells["sDantoc"].Value);
            cbMaLH.Text = Convert.ToString(row.Cells["sMaLH"].Value);
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maHSsua = (string)grvHS.CurrentRow.Cells["sMaHS"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn sửa học sinh có mã : {0} ?", maHSsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {


                        cmd.CommandText = "prUpdate_HS";
                        cmd.Parameters.AddWithValue("@smahs", maHSsua);
                        cmd.Parameters.AddWithValue("@stenhs", txtTen.Text);
                        cmd.Parameters.AddWithValue("@dns", txtTen.Text);
                        cmd.Parameters.AddWithValue("@sgioitinh", cbGT.Text);
                        cmd.Parameters.AddWithValue("@sdiachi", txtDC.Text);
                        cmd.Parameters.AddWithValue("@sdantoc", txtDT.Text);
                        cmd.Parameters.AddWithValue("@smalh", cbMaLH.Text);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    
                }
                
            }
            hienDSHS();
        }
        private void ResetHS()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            dtNS.Value = DateTime.Now;
            cbGT.Text = "";
            txtDC.Text = " ";
            txtDT.Text = " ";
            cbMaLH.Text = "";
        }
       
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetHS();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaHS_xoa = (string)grvHS.CurrentRow.Cells["sMaHS"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa học sinh có mã : {0} ?", MaHS_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("prDelete_HS", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smahs", MaHS_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSHS();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimkiem.Text))
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == System.Data.ConnectionState.Closed)
                        return;
                    using (SqlCommand sqlCmd = new SqlCommand("prSearch_HS", cnn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@stenhs", txtTimkiem.Text));
                        sqlCmd.Parameters.Add(new SqlParameter("@sdiachi", txtTimkiem.Text));
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            grvHS.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("tblHocsinh_Select", cnn))
                {



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        tbl.Load(reader);
                        grvHS.DataSource = tbl;
                        crpHocSinh baocao = new crpHocSinh();
                        baocao.SetDataSource(tbl);
                        dtHS bcDSGV = new dtHS();
                        frDSHS DSGV = new frDSHS();
                        DSGV.crystalReportViewer1.ReportSource = baocao;
                        DSGV.ShowDialog();
                    }
                }

            }
        }




        
    }
}

       