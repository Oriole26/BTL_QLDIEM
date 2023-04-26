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
    public partial class frMonhoc : Form
    {
        public frMonhoc()
        {
            InitializeComponent();
        }
        //Lấy ra danh sách môn học
        private DataTable getMH()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select *from tblMonHoc", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");

                        ad.Fill(tb);
                        return tb;

                    }
                }
            }

        }
        public bool KTThongTin()
        {
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMH.Focus();
                return false;
            }
            if (txtTenMH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenMH.Focus();
                return false;
            }
           
            if (txtSotiet.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số tiết học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSotiet.Focus();
                return false;
            }
            
            return true;
        }
        //hiện danh sách môn học
        private void hienDSMH()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("tblMonhoc_Select", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");
                        tb.Clear();
                        ad.Fill(tb);
                        DataView v = new DataView(tb);
                        grvMH.DataSource = v;
                    }
                }
            }
        }
        //Thêm môn học
        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("tblMonHoc_CheckMa", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@mamh", txtMaMH.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if (count > 0)
                    {
                        MessageBox.Show("Bạn đã nhập trùng mã!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else if (KTThongTin())
                    {
                     
                       using (SqlCommand Cmd = new SqlCommand("tblMonHoc_Insert", cnn))
                        {
                            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@mamh", txtMaMH.Text));
                            Cmd.Parameters.Add(new SqlParameter("@tenmh", txtTenMH.Text));
                            Cmd.Parameters.Add(new SqlParameter("@Sotiet", txtSotiet.Text));


                            Cmd.ExecuteNonQuery();
                            hienDSMH();
                        }
                    }
                }
            }
        }
        //Sửa môn học


        private void frMonhoc_Load(object sender, EventArgs e)
        {
            hienDSMH();
        }

        private void grvMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvMH.Rows[e.RowIndex];
            txtMaMH.Text = Convert.ToString(row.Cells["sMaMH"].Value);
            txtTenMH.Text = Convert.ToString(row.Cells["sTenMH"].Value);
            txtSotiet.Text = Convert.ToString(row.Cells["iSoTiet"].Value);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã môn học cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMH.Focus();
            }
            else if (KTThongTin())
            {
                try
                {
                    SqlConnection cnn = new SqlConnection();
                    cnn.ConnectionString = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "tblMonHoc_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@mamh", SqlDbType.VarChar).Value = txtMaMH.Text;
                    cmd.Parameters.Add("@tenmh", SqlDbType.NVarChar).Value = txtTenMH.Text;
                    cmd.Parameters.Add("@Sotiet", SqlDbType.NVarChar).Value = txtSotiet.Text;
                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    hienDSMH();
                    ResetMH();
                    MessageBox.Show("Đã sửa môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ResetMH()
        {
            txtMaMH.Text = "";
            txtTenMH.Text = "";
            txtSotiet.Text = "";
        }

        private void btnDatlai_Click(object sender, EventArgs e)
        {
            ResetMH();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã môn học cần xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMH.Focus();
            }
            else
            {
                try
                {
                    SqlConnection cnn = new SqlConnection();
                    cnn.ConnectionString = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "tblMonHoc_Xoa";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@mamh", SqlDbType.VarChar).Value = txtMaMH.Text;
                    cmd.Connection = cnn;
                    cnn.Open();
                    hienDSMH();
                    ResetMH();
                    MessageBox.Show("Đã xóa môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
