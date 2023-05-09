
namespace BTL_QLDIEM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangnhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangxuat = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLMH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLophoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNamHoc = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLHS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLGV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDiem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.danhMụcToolStripMenuItem,
            this.quảnLýToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1222, 36);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDangnhap,
            this.mnuDangxuat});
            this.hệThốngToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(108, 32);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // mnuDangnhap
            // 
            this.mnuDangnhap.Name = "mnuDangnhap";
            this.mnuDangnhap.Size = new System.Drawing.Size(165, 26);
            this.mnuDangnhap.Text = "Đăng nhập";
            this.mnuDangnhap.Click += new System.EventHandler(this.mnuDangnhap_Click);
            // 
            // mnuDangxuat
            // 
            this.mnuDangxuat.Name = "mnuDangxuat";
            this.mnuDangxuat.Size = new System.Drawing.Size(165, 26);
            this.mnuDangxuat.Text = "Đăng xuất";
            this.mnuDangxuat.Click += new System.EventHandler(this.mnuDangxuat_Click);
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQLMH,
            this.mnuKL,
            this.mnuLophoc,
            this.mnuNamHoc});
            this.danhMụcToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(114, 32);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            // 
            // mnuQLMH
            // 
            this.mnuQLMH.Name = "mnuQLMH";
            this.mnuQLMH.Size = new System.Drawing.Size(152, 26);
            this.mnuQLMH.Text = "Môn học";
            this.mnuQLMH.Click += new System.EventHandler(this.mnuQLMH_Click);
            // 
            // mnuKL
            // 
            this.mnuKL.Name = "mnuKL";
            this.mnuKL.Size = new System.Drawing.Size(152, 26);
            this.mnuKL.Text = "Khối lớp";
            this.mnuKL.Click += new System.EventHandler(this.mnuKL_Click);
            // 
            // mnuLophoc
            // 
            this.mnuLophoc.Name = "mnuLophoc";
            this.mnuLophoc.Size = new System.Drawing.Size(152, 26);
            this.mnuLophoc.Text = "Lớp học";
            this.mnuLophoc.Click += new System.EventHandler(this.mnuLophoc_Click);
            // 
            // mnuNamHoc
            // 
            this.mnuNamHoc.Name = "mnuNamHoc";
            this.mnuNamHoc.Size = new System.Drawing.Size(152, 26);
            this.mnuNamHoc.Text = "Năm học";
            this.mnuNamHoc.Click += new System.EventHandler(this.mnuNamHoc_Click);
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQLHS,
            this.mnuQLGV,
            this.mnuDiem});
            this.quảnLýToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(98, 32);
            this.quảnLýToolStripMenuItem.Text = "Quản lý ";
            // 
            // mnuQLHS
            // 
            this.mnuQLHS.Name = "mnuQLHS";
            this.mnuQLHS.Size = new System.Drawing.Size(190, 26);
            this.mnuQLHS.Text = "Học sinh";
            this.mnuQLHS.Click += new System.EventHandler(this.mnuQLHS_Click);
            // 
            // mnuQLGV
            // 
            this.mnuQLGV.Name = "mnuQLGV";
            this.mnuQLGV.Size = new System.Drawing.Size(190, 26);
            this.mnuQLGV.Text = "Giáo viên";
            this.mnuQLGV.Click += new System.EventHandler(this.mnuQLGV_Click);
            // 
            // mnuDiem
            // 
            this.mnuDiem.Name = "mnuDiem";
            this.mnuDiem.Size = new System.Drawing.Size(190, 26);
            this.mnuDiem.Text = "Điểm môn học";
            this.mnuDiem.Click += new System.EventHandler(this.mnuDiem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::BTL_QLDIEM.Properties.Resources.phan_mem_quan_ly_diem;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1222, 644);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDangnhap;
        private System.Windows.Forms.ToolStripMenuItem mnuDangxuat;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuQLMH;
        private System.Windows.Forms.ToolStripMenuItem mnuKL;
        private System.Windows.Forms.ToolStripMenuItem mnuLophoc;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuQLHS;
        private System.Windows.Forms.ToolStripMenuItem mnuQLGV;
        private System.Windows.Forms.ToolStripMenuItem mnuDiem;
        private System.Windows.Forms.ToolStripMenuItem mnuNamHoc;
    }
}