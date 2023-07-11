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
    public partial class frKhoiLop : Form
    {
        public frKhoiLop()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

       
        private void hienDSKL()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_KL", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblKL");
                        tb.Clear();
                        ad.Fill(tb);
                        DataView v = new DataView(tb);
                        grvKL.DataSource = v;
                    }
                }
            }
        }
        public bool KTThongTin()
        {
            if (txtMa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mã khối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMa.Focus();
                return false;
            }
            
            if (txtTen.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên khối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTen.Focus();
                return false;
            }
            return true;
        }

            private void frKhoiLop_Load(object sender, EventArgs e)
        {
            hienDSKL();
            grvKL.Columns[0].HeaderText = "Mã khối";
            grvKL.Columns[1].HeaderText = "Tên khối";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prChecktrung_MaKL", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smakl", txtMa.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if (count > 0)
                    {
                        MessageBox.Show("Trùng mã!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else if (KTThongTin())
                    {


                        using (SqlCommand Cmd = new SqlCommand("prInsert_KL", cnn))
                        {
                            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@smakl", txtMa.Text));
                            Cmd.Parameters.Add(new SqlParameter("@stenkl", txtTen.Text));
                            Cmd.ExecuteNonQuery();
                            hienDSKL();
                        }

                    }
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mã học sinh cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMa.Focus();
            }
            else if (KTThongTin())
            {
                try
                {
                    SqlConnection cnn = new SqlConnection();
                    cnn.ConnectionString = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "prUpdate_KL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@smakl", SqlDbType.VarChar).Value = txtMa.Text;
                    cmd.Parameters.Add("@stenkl", SqlDbType.NVarChar).Value = txtTen.Text;

                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    hienDSKL();
                    ResetKL();
                    MessageBox.Show("Đã sửa khối lớp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ResetKL()
        {
            txtMa.Text = "";
            txtTen.Text = "";
        
        }

        private void grvKL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvKL.Rows[e.RowIndex];
            txtMa.Text = Convert.ToString(row.Cells["sMaKhoiLop"].Value);
            txtTen.Text = Convert.ToString(row.Cells["sTenKhoiLop"].Value);
            
        }

        private void btlDatlai_Click(object sender, EventArgs e)
        {
            ResetKL();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mã khối lớp cần xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMa.Focus();
            }
            else
            {
                try
                {
                    SqlConnection cnn = new SqlConnection();
                    cnn.ConnectionString = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "prDelete_KL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@smakl", SqlDbType.VarChar).Value = txtMa.Text;
                    cmd.Connection = cnn;
                    cnn.Open();
                    hienDSKL();
                    ResetKL();
                    MessageBox.Show("Đã xóa khối lớp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }
    }
}
