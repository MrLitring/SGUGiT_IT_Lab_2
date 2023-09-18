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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private double[,] Coords;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;
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
                ReadFile(filename);
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
            Drawed();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Drawed();
        }

        private void Drawed()
        {
            int var = comboBox1.SelectedIndex;
            if (var == 1) { var = 3; }
            else if (var == 2) { var = 4; }

            foreach (Series ser in chart1.Series)
            {
                ser.ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)var;
            };
        }

        //
        //
        //

        private void ReadFile(string filename)
        {
            Coords = new double[15, 2];

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

                    if (point % 5 == 0)
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

            for (int i = 0; i < 5; i++)
            {
                x = Coords[i + NumSerie * 5, 0];
                y = Coords[i + NumSerie * 5, 1];
                chart1.Series[NameSerie].Points.Add(x,y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
