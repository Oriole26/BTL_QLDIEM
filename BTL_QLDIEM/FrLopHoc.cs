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
using System.Text.RegularExpressions;
using System.Configuration;
namespace BTL_QLDIEM
{
    public partial class FrLopHoc : Form
    {
        public FrLopHoc()
        {
            InitializeComponent();
        }
        string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

    
        //Hiện danh sách lớp học ra gridview
            private void hienDSLH()
             {
                using(SqlConnection cnn = new SqlConnection(str))
                {
                    using(SqlCommand cmd = new SqlCommand("prSelect_LH",cnn))
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
                    using (SqlCommand cmd = new SqlCommand("prSelect_MaTenKL", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable("tblKL");

                            ad.Fill(tb);

                     
                            cbMaKL.DisplayMember = "sTenKhoiLop";
                            cbMaKL.ValueMember = "sMaKhoiLop";
                      
                            cbMaKL.DataSource = tb;
                        }
                    }
                    //Lấy mã  và tên GV từ bảng GV
                    /*using (SqlCommand cmd = new SqlCommand("prGiaovientheoLH", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable("tblGV");

                            ad.Fill(tb);

                            cbMaGV.DisplayMember = "sTenGV";
                            cbMaGV.ValueMember = "sMaGV";
                            cbMaGV.DataSource = tb;
                       
                        }

                    }*/
                    
                }
            }
        private void FrLopHoc_Load(object sender, EventArgs e)
        {
            hienDSLH();
            grvLH.Columns[0].HeaderText = "Mã lớp";
            grvLH.Columns[1].HeaderText = "Tên lớp";
            grvLH.Columns[2].HeaderText = "Mã  khối lớp";
            grvLH.Columns[3].HeaderText = "Sĩ số";
            grvLH.Columns[4].HeaderText = "GVCN";
        }

        //Thêm lớp học
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(str))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prChecktrung_MaLH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smalh", txtMa.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if (count > 0)
                    {
                        MessageBox.Show("Trùng mã lớp!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (txtMa.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(txtMa, "Vui lòng nhập mã lớp ! ");
                            check = false;
                        }
                        else errorProviderLH.SetError(txtMa, "");


                        if (txtTen.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(txtTen, "Vui lòng nhập tên lớp!");
                            check = false;
                        }
                        else errorProviderLH.SetError(txtTen, "");

                        if (cbMaKL.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(cbMaKL, "Vui lòng chọn mã khối !");
                            check = false;
                        }
                        else errorProviderLH.SetError(cbMaKL, "");

                     

                        if (txtSiso.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(txtSiso, "Vui lòng nhập sĩ số !");
                            check = false;
                        }
                        else errorProviderLH.SetError(txtSiso, "");


                        if (cbMaGV.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(cbMaGV, "Vui lòng chọn mã giáo viên !");
                            check = false;
                        }
                        else errorProviderLH.SetError(cbMaGV, "");
                        
                        if (check)
                        {
                            using (SqlCommand Cmd = new SqlCommand("prInsert_LH", cnn))
                            {
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@smalh", txtMa.Text));
                                Cmd.Parameters.Add(new SqlParameter("@stenlh", txtTen.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smakl", cbMaKL.Text));
                                Cmd.Parameters.Add(new SqlParameter("@isiso", txtSiso.Text));
                                Cmd.Parameters.Add(new SqlParameter("@smagv", cbMaGV.Text));
                                Cmd.ExecuteNonQuery();
                                hienDSLH();
                            }
                        }


                    }
                }



            }
        }

        // Trở về trang chủ
       
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }

       //Sửa lớp học

        private void grvLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvLH.Rows[e.RowIndex];
            txtMa.Text = Convert.ToString(row.Cells["sMaLH"].Value);
            txtTen.Text = Convert.ToString(row.Cells["sTenLH"].Value);
            cbMaKL.Text = Convert.ToString(row.Cells["sMaKhoiLop"].Value);
            txtSiso.Text = Convert.ToString(row.Cells["iSiso"].Value);
            cbMaGV.Text = Convert.ToString(row.Cells["sMaGV"].Value);

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLHsua = (string)grvLH.CurrentRow.Cells["sMaLH"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn sửa lớp học có mã : {0} ?", maLHsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prUpdate_LH";
                        cmd.Parameters.AddWithValue("@smalh", maLHsua);
                        cmd.Parameters.AddWithValue("@stenlh", txtTen.Text);
                        cmd.Parameters.AddWithValue("@smakl ", cbMaKL.Text);
                        cmd.Parameters.AddWithValue("@isiso", txtSiso.Text);
                        cmd.Parameters.AddWithValue("@smagv", cbMaGV.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            hienDSLH();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaLH_xoa = (string)grvLH.CurrentRow.Cells["sMaLH"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa lớp học có mã : {0} ?", MaLH_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("prDelete_LH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smalh", MaLH_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSLH();
            }
        }
        private void ResetLH()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cbMaKL.Text = "";
            txtSiso.Text = " ";
            cbMaGV.Text = " ";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetLH();
        }

        private void txtTen_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^10|11|12A1|2|3$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtTen.Text);
            if (!isValid)
            {
                errorProviderLH.SetError(txtTen, "Tên lớp phải đúng định dạng(VD: 10A1)");
            }
            else errorProviderLH.SetError(txtTen, "");
        }

        private void txtMa_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^LOP10|11|12\d{4}$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtMa.Text);
            if (!isValid)
            {
                errorProviderLH.SetError(txtMa, "Tên lớp phải đúng định dạng(VD: 10A1)");
            }
            else errorProviderLH.SetError(txtMa, "");
        }
    }
}
