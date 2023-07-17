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
using System.Text.RegularExpressions;

namespace BTL_QLDIEM
{
    public partial class frMonhoc : Form
    {
        public frMonhoc()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;


        
        //hiện danh sách môn học
        private void hienDSMH()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_MH", cnn))
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
                using (SqlCommand cmd = new SqlCommand("prSelect_MaGV", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblMH");
                        tb.Clear();
                        ad.Fill(tb);
                        cbmaGV.DisplayMember = "sMaGV";
                        cbmaGV.ValueMember = "sMaGV";
                        cbmaGV.DataSource = tb;

                    }
                }
            }
        }
        //Thêm môn học
        private void btnThem_Click(object sender, EventArgs e)
        {
            bool check = true;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prChecktrung_MH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smamh", txtMaMH.Text));
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
                        using (SqlCommand Cmd = new SqlCommand("prInsert_MH", cnn))
                        {
                            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@smamh", txtMaMH.Text));
                            Cmd.Parameters.Add(new SqlParameter("@stenmh", txtTenMH.Text));
                            Cmd.Parameters.Add(new SqlParameter("@isotiet", txtSotiet.Text));
                            Cmd.Parameters.Add(new SqlParameter("@smagv", cbmaGV.Text));


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
            grvMH.Columns[3].HeaderText = "Giáo viên dạy";
        }

        private void grvMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvMH.Rows[e.RowIndex];
            txtMaMH.Text = Convert.ToString(row.Cells["sMaMH"].Value);
            txtTenMH.Text = Convert.ToString(row.Cells["sTenMH"].Value);
            txtSotiet.Text = Convert.ToString(row.Cells["iSoTiet"].Value);
            cbmaGV.Text = Convert.ToString(row.Cells["sMaGV"].Value);
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

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prUpdate_MH";
                        cmd.Parameters.AddWithValue("@smamh", maMHsua);
                        cmd.Parameters.AddWithValue("@stenmh", txtTenMH.Text);
                        cmd.Parameters.AddWithValue("@isotiet", txtSotiet.Text);
                        cmd.Parameters.AddWithValue("@smagv", cbmaGV.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }

                }
            }
            ResetMH();
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

                    using (SqlCommand cmd = new SqlCommand("prDelete_MH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smamh", MaMH_xoa);
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

        private void txtMaMH_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^[A-Z]+$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtMaMH.Text);
            if (!isValid)
            {
                errorProviderMH.SetError(txtMaMH, "Mã môn không được chứa số và kí tự, viết hoa và tương ứng với tên môn viết liền không dấu(VD:SINHHOC)");
            }
            else errorProviderMH.SetError(txtMaMH, "");
        }

        private void txtTenMH_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^[A-Z]{1}\w|\s\w$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtTenMH.Text);
            if (!isValid)
            {
                errorProviderMH.SetError(txtTenMH, "Tên môn không được chứa số và kí tự và viết hoa chữ cái đầu");
            }
            else errorProviderMH.SetError(txtTenMH, "");
        }

        private void txtSotiet_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^(?!0+$)[0-9]{1,12}$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtSotiet.Text);
            if (!isValid)
            {
                errorProviderMH.SetError(txtSotiet, "Số tiết phải là số và lớn hơn 0!");
            }
            else errorProviderMH.SetError(txtSotiet, "");
        }
    }
}
