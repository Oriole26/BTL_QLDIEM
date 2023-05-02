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
                    //Lấy  năm học
                    using (SqlCommand cmd = new SqlCommand("tblNamHoc_SelectMa", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable("tblNH");

                            ad.Fill(tb);

                            cbNH.DisplayMember = "sMaNamHoc";
                            cbNH.ValueMember = "sMaNamHoc";
                            cbNH.DataSource = tb;

                        }

                    }
                }
            }
        //Thêm lớp học
        private void FrLopHoc_Load(object sender, EventArgs e)
        {
            hienDSLH();
            grvLH.Columns[0].HeaderText = "Mã lớp";
            grvLH.Columns[1].HeaderText = "Tên lớp";
            grvLH.Columns[2].HeaderText = "Mã  khối lớp";
            grvLH.Columns[3].HeaderText = "Năm học";
            grvLH.Columns[4].HeaderText = "Sĩ số";
            grvLH.Columns[5].HeaderText = "GVCN";
        }
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("tblLopHoc_CheckMa", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@malh", txtMa.Text));
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

                        if (cbNH.Text.Trim().Length == 0)
                        {
                            errorProviderLH.SetError(cbNH, "Vui lòng chọn năm học!");
                            check = false;
                        }
                        else errorProviderLH.SetError(cbNH, "");

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
                            using (SqlCommand Cmd = new SqlCommand("tblLopHoc_Insert", cnn))
                            {
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@Malh", txtMa.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Tenlh", txtTen.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Makl", cbMaKL.Text));
                                Cmd.Parameters.Add(new SqlParameter("@Manh", cbNH.Text));
                            
                                    Cmd.Parameters.Add(new SqlParameter("@Siso", txtSiso.Text));

                                Cmd.Parameters.Add(new SqlParameter("@Magv", cbMaGV.Text));
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
            cbNH.Text = Convert.ToString(row.Cells["sMaNamHoc"].Value);
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
                        cmd.CommandText = "tblLopHoc_Update";
                        cmd.Parameters.AddWithValue("@malh", maLHsua);
                        cmd.Parameters.AddWithValue("@tenlh", txtTen.Text);
                        cmd.Parameters.AddWithValue("@Makl ", cbMaKL.Text);
                        cmd.Parameters.AddWithValue("@Manh", cbNH.Text);
                        cmd.Parameters.AddWithValue("@Siso", txtSiso.Text);
                        cmd.Parameters.AddWithValue("@Magv", cbMaGV.Text);
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

                    using (SqlCommand cmd = new SqlCommand("tblLopHoc_Xoa", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@malh", MaLH_xoa);
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
            cbNH.Text = " ";
            txtSiso.Text = " ";
            cbMaGV.Text = " ";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetLH();
        }
    }
}
