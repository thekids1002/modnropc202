using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string tentitle;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Data(dataGridView1, txtAcc).LoadFile();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(new object[]
            {
                "",
                txtAcc.Text,
                txtPass.Text,
                cboSv.Text
            });
            new Data(dataGridView1, txtAcc).ExportFile();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Cells[1].Value = txtAcc.Text;
            dataGridView1.Rows[index].Cells[2].Value = txtPass.Text;
            dataGridView1.Rows[index].Cells[3].Value = cboSv.Text;
            new Data(dataGridView1, txtAcc).ExportFile();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                new Data(dataGridView1, txtAcc).ExportFile();
            }
            else
            {
                MessageBox.Show("Không có gì để xoá", "Loi xoa tai khoan", MessageBoxButtons.OK);
            }
        }
        public bool canLogin()
        {
            if (!File.Exists("Dragonboy_vn_v202.exe"))
            {
                MessageBox.Show("Không tìm thấy file game", "Lỗi đăng nhập", MessageBoxButtons.OK);
                return false;
            }
            if (File.Exists("TextData/Log.txt"))
            {
                AutoClosingMessageBox.Show("Đợi một chút, từ từ đã \n Thông báo tự động đóng sau 5s", "Thông báo", 5000);
                File.Delete("TextData/Log.txt");

                return true;


            }

            return true;
        }
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string windowClass, string windowName);

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);
        public void login(int index)
        {
            TextWriter text = new StreamWriter("TextData/Log.txt");
            for (int i = 0; i < 4; i++)
            {
                text.Write(dataGridView1.Rows[index].Cells[i].Value.ToString() + "|");
            }
            text.Close();
            // Process.Start("Dragonboy_vn_v202.exe", "DragonBall " + index);
            var process = Process.Start(Application.StartupPath + "//Dragonboy_vn_v202.exe");
            Thread.Sleep(1000);
            string title = "HK" + dataGridView1.Rows[index].Cells[0].Value.ToString() + "|"
                + dataGridView1.Rows[index].Cells[1].Value.ToString();
            SetWindowText(process.MainWindowHandle, title);
            tentitle = title;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (canLogin())
            {
                login(dataGridView1.CurrentRow.Index);

            }
        }

        private void btnLoginAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang phát triển", "Thông báo", MessageBoxButtons.OK);
        }
    }
}
