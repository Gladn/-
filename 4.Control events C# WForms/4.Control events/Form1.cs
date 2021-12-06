using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4.Control_events
{
    public partial class Form1 : Form
    {
        string[][] mac;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
            textBox1.Multiline = true;
            textBox1.Height = 200;
            textBox1.Width = 450;
            tabControl1.Height = 275;
            tabControl1.Width = 450;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            mac = new string[textBox1.Lines.Count()][];
            for (int i = 0; i < textBox1.Lines.Count(); i++)
            {
                mac[i] = textBox1.Lines[i].Split(',');
            }
            UpdateDataGrid();

        }
        private void UpdateDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();            
            comboBox1.Items.Clear();           
            listBox1.Items.Clear();
            dataGridView2.Rows.Clear();
            string[] tmp = new string[mac.Count()];
            for (int i = 0; i < mac.Count(); i++)
            {
                tmp[i] = mac[i][0];
            }

            Array.Sort(tmp, mac);
            int max = 0;
            for (int i = 0; i < mac.Count(); i++)
            {
                Array.Sort(mac[i], 1, mac[i].Count() - 1);
                dataGridView1.Columns.Add(mac[i][0], mac[i][0]);
                dataGridView2.Rows.Add(mac[i][0], mac[i].Count() - 1);
                comboBox1.Items.Add(mac[i][0]);                           
                for (int j = 1; j < mac[i].Count(); j++)
                {
                    if (dataGridView1.Rows.Count < j)
                    {
                        dataGridView1.Rows.Add();
                    }
                    dataGridView1.Rows[j - 1].Cells[i].Value = mac[i][j];
                }
                if (mac[i].Count() > max)
                {
                    max = mac[i].Count() - 1;
                    //toolStripStatusLabel1.Text = "Максимальное число студентов в группе " + mac[i][0];
                }
            }
            if (comboBox1.Items != null) comboBox1.SelectedIndex = 0;          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mac = new string[textBox1.Lines.Count()][];
            for (int i = 0; i < textBox1.Lines.Count(); i++)
            {
                mac[i] = textBox1.Lines[i].Split(',');
            }
            UpdateDataGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 1; i < mac[comboBox1.SelectedIndex].Count(); i++)
            {
                listBox1.Items.Add(mac[comboBox1.SelectedIndex][i]);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) tabControl1.Visible = true;
            else tabControl1.Visible = false;
        }
    }
}
