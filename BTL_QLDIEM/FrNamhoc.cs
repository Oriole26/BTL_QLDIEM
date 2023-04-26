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
    public partial class FrNamhoc : Form
    {
        public FrNamhoc()
        {
            InitializeComponent();
        }
        //Lấy ra danh sách năm học
        private DataTable getNH()
        {
            string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("tblNamHoc_Select", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblNH");
                        ad.Fill(tb);
                        return tb;
                    }
                }
            }
        }
        //Hiện danh sách năm học
        private void hienDSNH()
        {
            string str = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("tblNamHoc_Select", cnn))
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
        }
        //Thêm năm học 
        private void btnThem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                bool check = true;
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("tblNamHoc_CheckMa", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smaNH", txtmaNH.Text));
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
                            using (SqlCommand Cmd = new SqlCommand("tblNamHoc_Insert", cnn))
                            {
                                Cmd.CommandType = CommandType.StoredProcedure;
                                Cmd.Parameters.Add(new SqlParameter("@smaNH", txtmaNH.Text));
                                Cmd.Parameters.Add(new SqlParameter("@stenNH", txttenNH.Text));
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
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "tblNamHoc_Update";
                        cmd.Parameters.AddWithValue("@smaNH", maNHsua);
                        cmd.Parameters.AddWithValue("@stenNH", txttenNH.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            hienDSNH();

        }
    }
}
