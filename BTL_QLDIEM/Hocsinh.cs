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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTL_QLDIEM
{
    public partial class Hocsinh : Form
    {
        public Hocsinh()
        {
            InitializeComponent();
        }

        private string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
        private DataTable tbHS = new DataTable("tblHS");
        private DataTable tbLH = new DataTable("tblLH");


        //hiện danh sách học sinh
        private void hienDSHS()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("prSelect_HS", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tbHS.Clear();
                        ad.Fill(tbHS);
                        DataView v = new DataView(tbHS);
                        grvHS.DataSource = v;
                        
                    }
                }
                using (SqlCommand cmd = new SqlCommand("prSelect_MaLH", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tbLH.Clear();

                        ad.Fill(tbLH);

                        cbMaLH.DisplayMember = "sTenLH";
                        cbMaLH.ValueMember = "sMaLH";
                        cbMaLH.DataSource = tbLH;
                    }
                }

            }
        }
        public bool KTTT()
        {
            if (txtMa.Text.Trim().Length == 0)
            {
                errorProviderHS.SetError(txtMa, "Vui lòng nhập mã học sinh ! ");
                return false;
            }
            else errorProviderHS.SetError(txtMa, "");


            if (txtTen.Text.Trim().Length == 0)
            {
                errorProviderHS.SetError(txtTen, "Vui lòng nhập tên học sinh!");
                return false;
            }
            else errorProviderHS.SetError(txtTen, "");

            if (cbGT.Text.Trim().Length == 0)
            {
                errorProviderHS.SetError(cbGT, "Vui lòng chọn giới tính !");
                return false;
            }
            else errorProviderHS.SetError(cbGT, "");

            if (txtDC.Text.Trim().Length == 0)
            {
                errorProviderHS.SetError(txtDC, "Vui lòng nhập địa chỉ!");
                return false;
            }
            else errorProviderHS.SetError(txtDC, "");

            if (txtDT.Text.Trim().Length == 0)
            {
                errorProviderHS.SetError(txtDT, "Vui lòng nhập dân tộc !");
                return false;
            }
            else errorProviderHS.SetError(txtDT, "");

            return true;
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hienDSHS();
            grvHS.Columns[0].HeaderText = "Mã HS";
            grvHS.Columns[1].HeaderText = "Tên HS";
            grvHS.Columns[2].HeaderText = "Ngày sinh";
            grvHS.Columns[3].HeaderText = "Giới tính";
            grvHS.Columns[4].HeaderText = "Địa chỉ";
            grvHS.Columns[5].HeaderText = "Dân tộc";
            grvHS.Columns[6].HeaderText = "Mã LH";



        }
       
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using(SqlCommand cmd = new SqlCommand("prChecktrung_MaHS", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smahs", txtMa.Text));
                    int count = (int)cmd.ExecuteScalar();//Sử dụng execteScalar để tr về số lượng bản ghi trùng mã
                    if(count > 0)
                    {
                        MessageBox.Show("Trùng mã!", "Thông báo", MessageBoxButtons.OK);
                    }
                    
                    else if (KTTT())
                    {
                        using (SqlCommand Cmd = new SqlCommand("prInsert_HS", cnn))
                        {
                            Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Cmd.Parameters.Add(new SqlParameter("@smahs", txtMa.Text));
                            Cmd.Parameters.Add(new SqlParameter("@stenhs", txtTen.Text));
                            Cmd.Parameters.Add(new SqlParameter("@dns", dtNS.Value.Date));
                            Cmd.Parameters.Add(new SqlParameter("@sgioitinh", cbGT.Text));
                            Cmd.Parameters.Add(new SqlParameter("@sdiachi", txtDC.Text));
                            Cmd.Parameters.Add(new SqlParameter("@sdantoc", txtDT.Text));
                            Cmd.Parameters.Add(new SqlParameter("@smalh", cbMaLH.SelectedValue.ToString()));
                            Cmd.ExecuteNonQuery();
                            int tmp = cbMaLH.SelectedIndex;
                            hienDSHS();
                            cbMaLH.SelectedIndex = tmp;
                            
                        }
                    } 
                }
            }

        }
        private void grvHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = new DataGridViewRow();
            row = grvHS.Rows[e.RowIndex];
            txtMa.Text = Convert.ToString(row.Cells["sMaHS"].Value);
            txtTen.Text = Convert.ToString(row.Cells["sHoTenHS"].Value);
            dtNS.Text = Convert.ToString(row.Cells["dNgaysinh"].Value);
            cbGT.Text = Convert.ToString(row.Cells["sGioitinh"].Value);
            txtDC.Text = Convert.ToString(row.Cells["sDiachi"].Value);
            txtDT.Text = Convert.ToString(row.Cells["sDantoc"].Value);
        }

        private void grvHS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = new DataGridViewRow();
            row = grvHS.Rows[e.RowIndex];
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prDiemtheoHS", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@smahs", Convert.ToString(row.Cells["sMaHS"].Value)));

                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        ad.Fill(tbl);
                        crpDiem baocao = new crpDiem();
                        baocao.SetDataSource(tbl);
                        dtDiem bcDSGV = new dtDiem();
                        frDSDiem DSGV = new frDSDiem();
                        DSGV.crystalReportViewer1.ReportSource = baocao;
                        DSGV.ShowDialog();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maHSsua = (string)grvHS.CurrentRow.Cells["sMaHS"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn sửa học sinh có mã : {0} ?", maHSsua), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("prUpdate_HS", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smahs", maHSsua);
                        cmd.Parameters.AddWithValue("@stenhs", txtTen.Text);
                        cmd.Parameters.AddWithValue("@dns", dtNS.Value);
                        cmd.Parameters.AddWithValue("@sgioitinh", cbGT.Text);
                        cmd.Parameters.AddWithValue("@sdiachi", txtDC.Text);
                        cmd.Parameters.AddWithValue("@sdantoc", txtDT.Text);
                        cmd.Parameters.AddWithValue("@smalh", cbMaLH.SelectedValue.ToString());

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    
                }
                
            }
            hienDSHS();
        }
        private void ResetHS()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            dtNS.Value = DateTime.Now;
            cbGT.Text = "";
            txtDC.Text = " ";
            txtDT.Text = " ";
            cbMaLH.Text = "";
        }
       
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetHS();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaHS_xoa = (string)grvHS.CurrentRow.Cells["sMaHS"].Value;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa học sinh có mã : {0} ?", MaHS_xoa), "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("prDelete_HS", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@smahs", MaHS_xoa);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                hienDSHS();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                MainForm trangchu = new MainForm();
                trangchu.ShowDialog();
                this.Close();
            }
        }
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimkiem.Text))
            {
                string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    if (cnn.State == ConnectionState.Closed)
                        return;
                    using (SqlCommand sqlCmd = new SqlCommand("prSearch_HS", cnn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@stenhs", txtTimkiem.Text));
                        sqlCmd.Parameters.Add(new SqlParameter("@sdiachi", txtTimkiem.Text));
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            grvHS.DataSource = dt;
                        }
                    }
                }
            } else
            {
                hienDSHS();
            }
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Closed)
                    return;
                using (SqlCommand cmd = new SqlCommand("prSelect_HS", cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        tbl.Load(reader);
                        grvHS.DataSource = tbl;
                        crpHocSinh baocao = new crpHocSinh();
                        baocao.SetDataSource(tbl);
                        dtHS bcDSGV = new dtHS();
                        frDSHS DSGV = new frDSHS();
                        DSGV.crystalReportViewer1.ReportSource = baocao;
                        DSGV.ShowDialog();
                    }
                }

            }
        }

        private void dtNS_Validating(object sender, CancelEventArgs e)
        {
            int birthday = dtNS.Value.Year;
            int currentYear = DateTime.Now.Year;
            if (currentYear - birthday <= 15)
            {
                errorProviderHS.SetError(dtNS, "Tuổi không được nhỏ hơn 15 tuổi !");
            }
            else
                errorProviderHS.SetError(dtNS, "");

        }

        private void txtMa_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^HS\d{3}|\d{2}$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtMa.Text);
            if (!isValid)
            {
                errorProviderHS.SetError(txtMa, "Mã phải nhập đúng định dạng(VD:HS01)");
            }
            else errorProviderHS.SetError(txtMa, "");
        }

        private void txtTen_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W|_]+$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtTen.Text);
            if (!isValid)
            {
                errorProviderHS.SetError(txtTen, "Họ tên không chứa số và kí tự đặc biệt");
            }
            else errorProviderHS.SetError(txtTen, "");
        }

        private void txtDT_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W|_]+$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtDT.Text);
            if (!isValid)
            {
                errorProviderHS.SetError(txtDT, "Địa chỉ không chứa số và kí tự đặc biệt");
            }
            else errorProviderHS.SetError(txtDT, "");
        }

        private void txtDC_Validating(object sender, CancelEventArgs e)
        {
            string sRegex = @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W|_]+$";
            Regex reg = new Regex(sRegex);
            bool isValid = reg.IsMatch(txtDC.Text);
            if (!isValid)
            {
                errorProviderHS.SetError(txtDC, "Địa chỉ không chứa số và kí tự đặc biệt");
            }
            else errorProviderHS.SetError(txtDC, "");
        }
                private void cbMaLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaLH.Focused)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("prSelectHSByMaLop", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@malh", cbMaLH.SelectedValue.ToString());
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            tbHS.Clear();
                            ad.Fill(tbHS);
                            //DataView v = new DataView(tbHS);
        private void cbMaLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaLH.Focused)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("prSelectHSByMaLop", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@malh", cbMaLH.SelectedValue.ToString());
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            tbHS.Clear();
                            ad.Fill(tbHS);
                            //DataView v = new DataView(tbHS);

                            grvHS.DataSource = tbHS;
                        }
                    }
                }
            } 
        }
    }
}

       