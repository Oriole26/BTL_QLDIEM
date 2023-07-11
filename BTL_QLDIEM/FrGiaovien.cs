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
    public partial class FrGiaovien : Form
    {
        public FrGiaovien()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

        
        //hiện danh sách giáo viên
        private void hienDSGV()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_GV", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblGV");
                        tb.Clear();
                        ad.Fill(tb);
                       
                        DataView v = new DataView(tb);
                        grvGV.DataSource = v;
                    }
                }

            }
        }

       
        private void FrGiaovien_Load(object sender, EventArgs e)
        {
            hienDSGV();
            grvGV.Columns[0].HeaderText = "Mã GV";
            grvGV.Columns[1].HeaderText = "Tên GV";
            grvGV.Columns[2].HeaderText = "Giới tính";
            grvGV.Columns[3].HeaderText = "Địa chỉ";
            grvGV.Columns[4].HeaderText = "SDT";
            

        }

        private bool phoneValidate(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length > 10) { return false; }

            phone = phone.Trim();
            if (phone[0] != '0') { return false; }

            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] - '0' < 0 || phone[i] - '0' > 9) { 

                    return false; }
            }

            return true;
        }
        //Thêm giáo viên
        private void btnThem_Click(object sender, EventArgs e)

        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prChecktrung_MaGV", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smagv", txtMa.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if (count > 0)
                    {
                        MessageBox.Show("Trùng mã giáo viên!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else 
                    {
                        if (txtMa.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(txtMa, "Vui lòng nhập mã giáo viên ! ");
                            check = false;
                        }
                        else errorProviderGV.SetError(txtMa, "");


                        if (txtTen.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(txtTen, "Vui lòng nhập tên giáo viên!");
                            check = false;
                        }
                        else errorProviderGV.SetError(txtTen, "");

                        if (cbGT.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(cbGT, "Vui lòng chọn giới tính !");
                            check = false;
                        }
                        else errorProviderGV.SetError(cbGT, "");

                        if (txtDC.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(txtDC, "Vui lòng nhập địa chỉ!");
                            check = false;
                        }
                        else errorProviderGV.SetError(txtDC, "");

                        if (txtSDT.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(txtSDT, "Vui lòng nhập số điện thoại !");
                            check = false;
                        }
                        else errorProviderGV.SetError(txtSDT, "");

                        if (check)
                        {
                            using (SqlCommand Cmd = new SqlCommand("prInsert_GV", cnn))
                            {

                                if (phoneValidate(txtSDT.Text) == true)
                                {
                                    Cmd.CommandType = CommandType.StoredProcedure;
                                    Cmd.Parameters.Add(new SqlParameter("@smagv", txtMa.Text));
                                    Cmd.Parameters.Add(new SqlParameter("@stengv", txtTen.Text));
                                    Cmd.Parameters.Add(new SqlParameter("@sgioitinh", cbGT.Text));
                                    Cmd.Parameters.Add(new SqlParameter("@sdiachi", txtDC.Text));
                                    Cmd.Parameters.Add(new SqlParameter("@ssdt", txtSDT.Text));

                                    Cmd.ExecuteNonQuery();
                                    hienDSGV();
                                    ResetGV();
                                }
                                else
                                {
                                    MessageBox.Show("Định dạng chưa chính xác!", "Thông báo", MessageBoxButtons.OK);

                                }
                            }
                        }
                       

                    }
                }


                
            }
        }

      
        // Trở về trang chủ
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn quay lại trang chủ không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }
        //Hiện thông tin giáo viên textbox khi click vào gridview
        private void grvGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvGV.Rows[e.RowIndex];
            txtMa.Text = Convert.ToString(row.Cells["sMaGV"].Value);
            txtTen.Text = Convert.ToString(row.Cells["sTenGV"].Value);
            cbGT.Text = Convert.ToString(row.Cells["sGioitinh"].Value);
            txtDC.Text = Convert.ToString(row.Cells["sDiaChi"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["sDienThoai"].Value);





        }
        //Sử giáo viên khi  click vào nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maGVsua = (string)grvGV.CurrentRow.Cells["sMaGV"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn sửa giáo viên có mã : {0} ?", maGVsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using(SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prUpdate_GV";
                        cmd.Parameters.AddWithValue("@smagv", maGVsua);
                        cmd.Parameters.AddWithValue("@stenGV", txtTen.Text);
                        cmd.Parameters.AddWithValue("@sgioitinh", cbGT.Text);
                        cmd.Parameters.AddWithValue("@sdiachi", txtDC.Text);
                        cmd.Parameters.AddWithValue("@ssdt", txtSDT.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
             }
            hienDSGV();

        }
        // Xoá giáo viên khi click vào nút Xoá
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaGV_xoa = (string)grvGV.CurrentRow.Cells["sMaGV"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa giáo viên có mã : {0} ?", MaGV_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("prDelete_GV", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smagv", MaGV_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSGV();
            }
        }
        //Reset lại thông tin
        private void ResetGV()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cbGT.Text = "";
            txtDC.Text = " ";
            txtSDT.Text = " ";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGV();
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTK.Text))
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == System.Data.ConnectionState.Closed)
                        return;
                    using (SqlCommand sqlCmd = new SqlCommand("prSearch_GV", cnn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@stengv", txtTK.Text));
                        sqlCmd.Parameters.Add(new SqlParameter("@sdiachi", txtTK.Text));
                        sqlCmd.Parameters.Add(new SqlParameter("@ssdt", txtTK.Text));
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            grvGV.DataSource = dt;
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
                using (SqlCommand cmd = new SqlCommand("prSelect_GV", cnn))
                {
                    


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        tbl.Load(reader);
                        grvGV.DataSource = tbl;
                        crpDSGV baocao = new crpDSGV();
                        baocao.SetDataSource(tbl);
                        dtGV bcDSGV = new dtGV();
                        frDSGV DSGV = new frDSGV();
                       DSGV.crystalReportViewer1.ReportSource = baocao;
                        DSGV.ShowDialog();
                    }
                }

            }
        }

    

    }
}