
namespace BTL_QLDIEM
{
    partial class frKhoiLop
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.grvKL = new System.Windows.Forms.DataGridView();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btlDatlai = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvKL)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã khối lớp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên khối lớp";
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(145, 83);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(190, 22);
            this.txtMa.TabIndex = 2;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(145, 175);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(190, 22);
            this.txtTen.TabIndex = 3;
            // 
            // grvKL
            // 
            this.grvKL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvKL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvKL.Location = new System.Drawing.Point(385, 45);
            this.grvKL.Name = "grvKL";
            this.grvKL.RowHeadersWidth = 51;
            this.grvKL.RowTemplate.Height = 24;
            this.grvKL.Size = new System.Drawing.Size(347, 204);
            this.grvKL.TabIndex = 4;
            this.grvKL.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvKL_CellContentClick);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(133, 277);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 36);
            this.btnSua.TabIndex = 6;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(253, 277);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 36);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btlDatlai
            // 
            this.btlDatlai.Location = new System.Drawing.Point(364, 277);
            this.btlDatlai.Name = "btlDatlai";
            this.btlDatlai.Size = new System.Drawing.Size(90, 36);
            this.btlDatlai.TabIndex = 8;
            this.btlDatlai.Text = "Đặt lại";
            this.btlDatlai.UseVisualStyleBackColor = true;
            this.btlDatlai.Click += new System.EventHandler(this.btlDatlai_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(489, 277);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(90, 36);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(25, 277);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 36);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // frKhoiLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 348);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btlDatlai);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.grvKL);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frKhoiLop";
            this.Text = "frKhoiLop";
            this.Load += new System.EventHandler(this.frKhoiLop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvKL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.DataGridView grvKL;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btlDatlai;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnThem;
    }
}