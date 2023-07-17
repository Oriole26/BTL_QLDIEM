using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BTL_QLDIEM
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(richTextBox1.Text)) { btnSave.Enabled = false; }
            else { btnSave.Enabled = true; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());
                // Sub Item là nội dung cột đằng sau
                item.SubItems.Add(textBox1.Text);
                
                listView1.Items.Add(item);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text)) //Nếu không null hoặc rỗng
            {
                //Tách ghi chú thành các dòng chứa vào mảng (Nếu cần)
                //string[] lines = richTextBox1.Text.Split("\n".ToCharArray());

                /*
                //Gộp lại thành 1 chuỗi với dấu ; ngăn cách giữa mỗi ghi chú (Nếu cần)
                string oneLine = "";
                for (int i = 0; i<lines.Length; i++)
                {
                    oneLine += lines[i] + ';';
                }
                */

                //Insert SQL - Nội dung của richTextBox có thể được lấy bằng richTextBox1.Text
                //....
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text)) { btnSave.Enabled = false; }
            else { btnSave.Enabled = true; }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            // Nếu add ghi chú đầu tiên vào richTextBox thì sẽ không có \n - xuống dòng
            if (string.IsNullOrEmpty(richTextBox1.Text)) { richTextBox1.Text += listView1.SelectedItems[0].SubItems[1].Text; }
            // Ngược lại với những ghi chú sau sẽ có xuống dòng trước khi thêm
            else richTextBox1.Text += '\n' + listView1.SelectedItems[0].SubItems[1].Text;
        }
    }
}
