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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace BTL_QLDIEM
{
    public partial class crvHS_LH : Form
    {
        public crvHS_LH()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["db_QLdiem"].ConnectionString;
        crtHS_LH dshs_lh = new crtHS_LH();
        private void crvHS_LH_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Select*from tblLopHoc", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable("tblLH");
                            ad.Fill(tb);
                            cboLH.DisplayMember = "sTenLH";
                            cboLH.ValueMember = "sMaLH";
                            cboLH.DataSource = tb;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLH.Focused)
                {
                    string sMaLH = cboLH.SelectedValue + "";

                    crystalReportViewer1.ReportSource = dshs_lh;
                    dshs_lh.RecordSelectionFormula = "{tblLopHoc.sMaLH} = '" + sMaLH + "'";
                    
                    crystalReportViewer1.Refresh();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
