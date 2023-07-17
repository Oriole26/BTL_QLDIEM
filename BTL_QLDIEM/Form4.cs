using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLDIEM
{
    public partial class Form4 : Form
    {
        bool chk = false;

        public Form4()
        {
            InitializeComponent();
        }

        private void groupBox1_Validated(object sender, EventArgs e)
        {
            groupBox1.Controls.OfType<RadioButton>().First(n => n.Checked);

           
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (((RadioButton)groupBox1.Controls[i]).Checked)
                {
                    chk = true; break;
                }     
            }

            for (int i = 0; i < groupBox2.Controls.Count; i++)
            {
                if (((RadioButton)groupBox1.Controls[i]).Checked)
                {
                    chk = true; break;
                }     
            }

            if (chk) { button1.Enabled = true; }
            else { button1.Enabled = false; }
        }
    }
}
