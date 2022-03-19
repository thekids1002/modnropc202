using System;
using System.Windows.Forms;
using xNet;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string VERSION_CODE = "002";
        [STAThread]
        static void Main()
        {
            HttpRequest http = new HttpRequest();
            string htm = http.Get("http://kietml.tk/").ToString();
            if (htm.ToLower().Contains("update") && !htm.Contains(VERSION_CODE))
            {
                MessageBox.Show("Đã có bản cập nhật mới vui lòng vào web kietml.epizy.com để tải bản mới nhất", "Thông báo", MessageBoxButtons.OK);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

        }
    }
}
