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

        //lấy ra ds GV
        private DataTable getGV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("tblGV_Select", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tbGV");

                        ad.Fill(tb);
                        return tb;

                    }
                }
            }

        }
        //hiện danh sách giáo viên
        private void hienDSGV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("tblGV_Select", cnn))
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

                //Lấy mã MH từ bảng môn học
                using (SqlCommand cmd = new SqlCommand("tblMonhoc_Ma", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");

                        ad.Fill(tb);

                        cbMaMH.DisplayMember = "sMaMH";//sMaMH là tên trường bạn muốn hiển thị trong combobox
                        cbMaMH.ValueMember = "sMaMH";
                        cbMaMH.DataSource = tb;
                    }
                }
            }
        }

       
        private void FrGiaovien_Load(object sender, EventArgs e)
        {
            hienDSGV();
            

        }
      

        //Thêm giáo viên
        private void btnThem_Click(object sender, EventArgs e)

        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("tblGiaoVien_CheckMa", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Magv", txtMa.Text));
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


                        if (cbMaMH.Text.Trim().Length == 0)
                        {
                            errorProviderGV.SetError(cbMaMH, "Vui lòng chọn mã môn học !");
                            check = false;
                        }
                        else errorProviderGV.SetError(cbMaMH, "");
                        if(check)
                        {
                            using (SqlCommand Cmd = new SqlCommand("tblGV_Insert", cnn))
                            {
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@Magv", txtMa.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Tengv", txtTen.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Gioitinh", cbGT.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Diachi", txtDC.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Sdt", txtSDT.Text));
                                Cmd.Parameters.Add(new SqlParameter("@mamh", cbMaMH.Text));
                                Cmd.ExecuteNonQuery();
                                hienDSGV();
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
            cbMaMH.Text = Convert.ToString(row.Cells["sMaMH"].Value);





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
                        cmd.CommandText = "tblGV_Update";
                        cmd.Parameters.AddWithValue("@Magv", maGVsua);
                        cmd.Parameters.AddWithValue("@tenGV", txtTen.Text);
                        cmd.Parameters.AddWithValue("@Gioitinh", cbGT.Text);
                        cmd.Parameters.AddWithValue("@Diachi", txtDC.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                        cmd.Parameters.AddWithValue("@mamh",cbMaMH.Text);
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

                    using (SqlCommand cmd = new SqlCommand("tblGV_Xoa", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Magv", MaGV_xoa);
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
            cbMaMH.Text = " ";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGV();
        }
    }
}