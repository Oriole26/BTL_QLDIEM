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
            bool check = true;
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
                    if (txtMaMH.Text.Trim().Length == 0)
                    {
                        errorProviderMH.SetError(txtMaMH, "Vui lòng nhập mã môn học ! ");
                        check = false;
                    }
                    else errorProviderMH.SetError(txtMaMH, "");


                    if (txtTenMH.Text.Trim().Length == 0)
                    {
                        errorProviderMH.SetError(txtTenMH, "Vui lòng nhập tên môn học!");
                        check = false;
                    }
                    else errorProviderMH.SetError(txtTenMH, "");

                    if (txtSotiet.Text.Trim().Length == 0)
                    {
                        errorProviderMH.SetError(txtSotiet, "Vui lòng nhập sĩ số!");
                        check = false;
                    }
                    else errorProviderMH.SetError(txtSotiet, "");

                    if(check)
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
            grvMH.Columns[0].HeaderText = "Mã môn học";
            grvMH.Columns[1].HeaderText = "Tên môn học";
            grvMH.Columns[2].HeaderText = "Số tiết";
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
            string maMHsua = (string)grvMH.CurrentRow.Cells["sMaMH"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn sửa môn học có mã : {0} ?", maMHsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {


                        cmd.CommandText = " tblMonHoc_Update";
                        cmd.Parameters.AddWithValue("@mamh", maMHsua);
                        cmd.Parameters.AddWithValue("@tenhs", txtTenMH.Text);
                        cmd.Parameters.AddWithValue("@Sotiet", txtSotiet.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }

                }

            }
            hienDSMH();
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

      

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaMH_xoa = (string)grvMH.CurrentRow.Cells["sMaMH"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa môn học có mã : {0} ?", MaMH_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("tblMonHoc_Xoa", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mamh", MaMH_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSMH();
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
