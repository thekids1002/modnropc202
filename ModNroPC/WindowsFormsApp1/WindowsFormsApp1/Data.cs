using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Data
    {
        public DataGridView dataGridView
        {
            get
            {
                return this.DataGridView;
            }
            set
            {
                this.DataGridView = value;
            }
        }
        public TextBox acc
        {
            get
            {
                return this.aCC;
            }
            set
            {
                this.aCC = value;
            }
        }
        public Data(DataGridView dataGridView, TextBox text)
        {
            this.dataGridView = dataGridView;
            this.acc = text;
        }
        public Data(TextBox textBox)
        {
            this.acc = textBox;
        }
        public void ExportFile()
        {
            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                this.dataGridView.Rows[i].Cells[0].Value = i + 1;
            }
            TextWriter textWriter = new StreamWriter("TextData/Account.txt");
            for (int j = 0; j < this.dataGridView.Rows.Count; j++)
            {
                for (int k = 0; k < this.dataGridView.Columns.Count; k++)
                {
                    textWriter.Write(this.dataGridView.Rows[j].Cells[k].Value.ToString() + "|");
                }
                textWriter.WriteLine("");
            }
            textWriter.Close();
        }

        public void LoadFile()
        {
            try
            {
                this.dataGridView.Rows.Clear();
                string[] array = File.ReadAllLines("TextData/Account.txt");
                for (int i = 0; i < array.Length; i++)
                {
                    string[] array2 = array[i].ToString().Split(new char[]
                    {
                        '|'
                    });
                    this.dataGridView.Rows.Add(new object[]
                    {
                        array2[0],
                        array2[1],
                        array2[2],
                        array2[3]
                    });
                }

            }
            catch (Exception)
            {
            }
        }
        public DataGridView DataGridView;
        private TextBox aCC;
    }
}
