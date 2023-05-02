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
    public partial class FrDangnhap : Form
    {
        public FrDangnhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(str))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                if (txtTenDN.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Vui lòng điền tên đăng nhập ", " Thông báo ", MessageBoxButtons.OK);
                    txtTenDN.Focus();

                }
                else
                {

                    using (SqlCommand cmd = new SqlCommand("pr_CheckTK", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@stennd", txtTenDN.Text.Trim());

                        int count = (int)cmd.ExecuteScalar();
                        if (count == 1)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK);
                        }
                        cmd.ExecuteNonQuery();

                    }

                }

                if (txtMK.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Vui lòng điền mật khẩu ", "Thông báo ", MessageBoxButtons.OK);
                    txtMK.Focus();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("pr_CheckMK", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smatkhau", txtMK.Text.Trim());

                        int count = (int)cmd.ExecuteScalar();
                        if (count == 1)
                        {
                            MessageBox.Show("Đăng nhập thành công ", "Thông báo ", MessageBoxButtons.OK);
                            this.Hide();
                            MainForm trangchu = new MainForm();
                            trangchu.ShowDialog();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                Application.Exit();
        }

        private void ckhienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (ckhienMK.Checked)
            {
                txtMK.PasswordChar = (char)0;
            }
            else
            {
                txtMK.PasswordChar = '*';
            }
        }
    }
}
