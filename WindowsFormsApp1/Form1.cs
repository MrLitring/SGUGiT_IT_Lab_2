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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices.ComTypes;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        double[,] Coords;
        string lastPath = string.Empty;
        int pointInSeries = 5;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;
            textBox1.Text = pointInSeries.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog ofd = new OpenFileDialog() {Filter = "Текстовые файлы(*.txt)|*.txt" };

            if(ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                filename = ofd.FileName;
                lastPath = filename;
                label3.Text = $"Точек: {LineCount(filename).ToString()}";
                label5.Text = $"Серий: {LineCount(filename) / pointInSeries}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    CreateSeries(i);
                }
            }
            Drawing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            ReadFile(lastPath);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Drawing();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != "0")
            {
                int points = int.Parse(textBox1.Text);
                pointInSeries = points;
                label5.Text = $"Серий: " + (LineCount(lastPath) / points).ToString();
            }
        }



        //
        //
        //


        private void ReadFile(string filename)
        {
            int cord = LineCount(lastPath);
            Coords = new double[cord, 2];

            string line = string.Empty;
            int point = 0;
            int ser = 1;

            using (StreamReader sr = new StreamReader(filename))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(' ');
                    Coords[point, 0] = Convert.ToDouble(splitLine[0]);
                    Coords[point, 1] = Convert.ToDouble(splitLine[1]);

                    if (point % pointInSeries == 0)
                    {
                        string NameSeries = "Серия " + Convert.ToString(ser);
                        checkedListBox1.Items.Add(NameSeries);
                        ser++;   
                    }
                    point++;
                }
            }
        }

        public void CreateSeries(int NumSerie)
        {
            double x, y;
            string NameSerie = "Серия" + Convert.ToString(NumSerie + 1);
            chart1.Series.Add(NameSerie);
            chart1.Series[NameSerie].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart1.Series[NameSerie].Enabled = true;
            chart1.Series[NameSerie].BorderWidth = 2;

            for (int i = 0; i < pointInSeries; i++)
            {
                try
                { 
                    x = Coords[i + NumSerie * pointInSeries, 0];
                    y = Coords[i + NumSerie * pointInSeries, 1];
                    chart1.Series[NameSerie].Points.Add(x, y);
                }
                catch
                {
                    
                }
            }
        }

        private void Drawing()
        {
            int var = comboBox1.SelectedIndex;
            if (var == 1) { var = 3; }
            else if (var == 2) { var = 4; }

            foreach (Series ser in chart1.Series)
            {
                ser.ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)var;
            };
        }

        

        

        private int LineCount(string path)
        {
            string line;
            int count = 0;

            if (path != "")
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        
    }
}
