using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace AG.COM.SDM.GeoDataBase.SpatialIndex
{
    public partial class FormGridSetting : Form
    {
        public FormGridSetting()
        {
            InitializeComponent();
            m_GridSize = new double[3] { 0.0, 0.0, 0.0 };
            numericUpDown1.Maximum = decimal.MaxValue;
            numericUpDown2.Maximum = decimal.MaxValue;
            numericUpDown3.Maximum = decimal.MaxValue;
        }

        public double[] m_GridSize;
        public int m_GridCount = 1;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    numericUpDown1.Enabled = false;
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Enabled = false;
                    m_GridCount = 0;
                    break;
                case 1:
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = false;
                    numericUpDown3.Enabled = false;
                    m_GridCount = 1;
                    break;
                case 2:
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                    numericUpDown3.Enabled = false;
                    m_GridCount = 2;
                    break;
                default:
                    numericUpDown1.Enabled = true;
                    numericUpDown2.Enabled = true;
                    numericUpDown3.Enabled = true;
                    m_GridCount = 3;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_GridSize[0] = (double)numericUpDown1.Value;
            m_GridSize[1] = (double)numericUpDown2.Value;
            m_GridSize[2] = (double)numericUpDown3.Value;
        }


        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (numericUpDown2.Enabled && numericUpDown2.Value < numericUpDown1.Value * 3)
            {
                button1.Enabled = false;
                MessageBox.Show("二级索引的值最小为一级索引值的三倍");
            }
            else
                button1.Enabled = true;
            if (numericUpDown3.Enabled && numericUpDown3.Value < numericUpDown2.Value * 3)
            {
                button1.Enabled = false;
                MessageBox.Show("三级索引的值最小为二级索引值的三倍");
            }
            else
                button1.Enabled = true;
        }

  
    }
}
