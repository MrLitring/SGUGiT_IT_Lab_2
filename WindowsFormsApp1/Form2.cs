using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string filename;

        public Form2()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog() { Filter = "Текстовые файлы(*.txt)|*.txt" };

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                filename = ofd.FileName;
                textBox1.Text = ofd.FileName;
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateFile();

        }

        private void CreateFile()
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text != "")
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = int.Parse(textBox2.Text);

                textBox1.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox2.Enabled = false;

                var random = new Random();

                using (StreamWriter streamWriter = new StreamWriter(filename, true))
                {
                    for (int i = 0; i < int.Parse(textBox2.Text); i++)
                    {
                        int x = random.Next(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                        int y = random.Next(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                        streamWriter.WriteLine($"{x} {y}");
                        progressBar1.Value++;
                        System.Threading.Thread.Sleep(10);
                    }
                }

                System.Threading.Thread.Sleep(1000);
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox2.Enabled = true;
                progressBar1.Value = 0;

            }
            else
            {
                MessageBox.Show("Заполните все поля", "Генератор", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
