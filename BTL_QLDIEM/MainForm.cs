using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLDIEM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mnuQLHS_Click(object sender, EventArgs e)
        {

            this.Hide();
            Hocsinh hocsinh = new Hocsinh();
            hocsinh.ShowDialog();
            this.Close();

        }

        private void mnuQLMH_Click(object sender, EventArgs e)
        {
            this.Hide();
            frMonhoc monhoc = new frMonhoc();
            monhoc.ShowDialog();
            this.Close();

        }

        private void mnuKL_Click(object sender, EventArgs e)
        {
            this.Hide();
            frKhoiLop monhoc = new frKhoiLop();
            monhoc.ShowDialog();
            this.Close();
        }

        private void mnuQLGV_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrGiaovien giaovien = new FrGiaovien();
            giaovien.ShowDialog();
            this.Close();
        }

        private void mnuLophoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrLopHoc lophoc = new FrLopHoc();
            lophoc.ShowDialog();
            this.Close();
        }

        private void mnuDiem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrDiem diem = new FrDiem();
            diem.ShowDialog();
            this.Close();
        }

        private void mnuNamHoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrNamhoc namhoc = new FrNamhoc();
            namhoc.ShowDialog();
            this.Close();
        }

        private void mnuHocKy_Click(object sender, EventArgs e)
        {

        }
        bool isThoat = true;
        private void mnuDangxuat_Click(object sender, EventArgs e)
        {
            isThoat = false;
            
            this.Close();
            FrDangnhap dangnhap = new FrDangnhap();
            dangnhap.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isThoat)
            Application.Exit();
           
        }

        private void mnuDangnhap_Click(object sender, EventArgs e)
        {
         
            FrDangnhap dangnhap = new FrDangnhap();
            dangnhap.Show();
            this.Hide();
        }
    }
    
}
