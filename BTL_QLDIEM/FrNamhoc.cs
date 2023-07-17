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
    public partial class FrNamhoc : Form
    {
        public FrNamhoc()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;

       
        //Hiện danh sách năm học
        private void hienDSNH()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_NH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblNH");
                        tb.Clear();
                        ad.Fill(tb);
                        DataView v = new DataView(tb);
                        grvNH.DataSource = v;
                       
                    }
                }
            }
        }
       


        private void FrNamhoc_Load(object sender, EventArgs e)
        {
            hienDSNH();
            grvNH.Columns[0].HeaderText = "Mã năm học";
            grvNH.Columns[1].HeaderText = "Tên năm học";
        
        }
        //Thêm năm học 
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prChecktrung_MaNH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smanh", txtmaNH.Text));
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Trùng mã năm học!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (txtmaNH.Text.Trim().Length == 0)
                        {
                            errorProviderNH.SetError(txtmaNH, "Vui lòng nhập mã năm học ! ");
                            check = false;
                        }
                        else errorProviderNH.SetError(txtmaNH, "");


                        if (txttenNH.Text.Trim().Length == 0)
                        {
                            errorProviderNH.SetError(txttenNH, "Vui lòng nhập tên năm học!");
                            check = false;
                        }
                        else errorProviderNH.SetError(txttenNH, "");
                        if (check)
                        {
                            using (SqlCommand Cmd = new SqlCommand("prInsert_NH", cnn))
                            {
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@smanh", txtmaNH.Text));
                                Cmd.Parameters.Add(new SqlParameter("@stennh", txttenNH.Text));
                                Cmd.ExecuteNonQuery();
                                hienDSNH();
                            }
                        }
                    }
                    
                }
            }
        }
        //Sửa năm học
        private void grvNH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = grvNH.Rows[e.RowIndex];
            txtmaNH.Text = Convert.ToString(row.Cells["sMaNamHoc"].Value);
            txttenNH.Text = Convert.ToString(row.Cells["sTenNamHoc"].Value);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNHsua = (string)grvNH.CurrentRow.Cells["sMaNamHoc"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn năm học có mã : {0} ?", maNHsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prUpdate_NH";
                        cmd.Parameters.AddWithValue("@smanh", maNHsua);
                        cmd.Parameters.AddWithValue("@stennh", txttenNH.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            hienDSNH();

        }
        //Xoá năm học

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaNH_xoa = (string)grvNH.CurrentRow.Cells["sMaNamHoc"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa năm học có mã : {0} ?", MaNH_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("prDelete_NH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smanh", MaNH_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSNH();
            }
        }
        //Reset lại thông tin
        private void ResetNH()
        {
            txtmaNH.Text = "";
            txttenNH.Text = "";
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetNH();
        }
        //trở về trang chủ
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

        private void txtmaNH_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^NH\d{4}$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtmaNH.Text);
            if (!isValid)
            {
                errorProviderNH.SetError(txtmaNH, "Bạn phải nhập theo thứ tự NH sau đó đến 4 chữ số(VD: NH2021 ! ");
            }
            else errorProviderNH.SetError(txtmaNH, "");
        }

        private void txttenNH_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^20\d{2}-20\d{2}$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txttenNH.Text);
            if (!isValid)
            {
                errorProviderNH.SetError(txttenNH, "Bạn phải nhập hai năm phù hợp (VD: 2020-2021)!");
            }
            else errorProviderNH.SetError(txttenNH, "");
        }
    }
}
