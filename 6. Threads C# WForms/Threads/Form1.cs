using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Threads
{
    public partial class Form1 : Form
    {
        bool a = true;
        int k1 = 2; int k2 = 3; int k3 = 4;
        List<int> arr = new List<int>(); 
        List<int> spis = new List<int>();
        List<int> spis2 = new List<int>();
        List<int> spis3 = new List<int>();
        List<int> arr2 = new List<int>();
        List<int> arr3 = new List<int>();
        ConsoleKeyInfo cki = new ConsoleKeyInfo();
        public Form1()
        {
            InitializeComponent();
        }

        public void Thread2()
        {
            Random r2 = new Random();
            for (int i = 0; i <= k2; i++)
            {
                arr2.Add(r2.Next(10, 300));
                spis2.Add(r2.Next(5, 10));
            }
            while (a)
            {
                for (int i = 0; i < k2; i++)
                {
                    Thread.Sleep(10);
                    if (arr2[i] <= 10) spis2[i] = -spis2[i];
                    if (arr2[i] >= pictureBox2.Width - 10) spis2[i] = -spis2[i];
                    if (arr2[i + 1] <= 10) spis2[i + 1] = -spis2[i + 1];
                    if (arr2[i + 1] >= pictureBox2.Height - 10) spis2[i + 1] = -spis2[i + 1];
                    arr2[i] += spis2[i];
                    arr2[i + 1] += spis2[i + 1];
                    pictureBox2.Invalidate();
                }                          
            }
        }
      
        public void Thread3()
        {
            Random r3 = new Random();
            for (int i = 0; i <= k3; i++)
            {
                arr3.Add(r3.Next(10, 300));
                spis3.Add(r3.Next(1, 2));
            }
            while (a)
            {
                for (int i = 0; i < k3; i++)
                {
                    Thread.Sleep(1);
                    if (arr3[i] <= 10) spis3[i] = -spis3[i];
                    if (arr3[i] >= pictureBox3.Width) spis3[i] = -spis3[i];
                    if (arr3[i + 1] <= 10) spis3[i + 1] = -spis3[i + 1];
                    if (arr3[i + 1] >= pictureBox3.Height) spis3[i + 1] = -spis3[i + 1];
                    arr3[i] += spis3[i];
                    arr3[i + 1] += spis3[i + 1];
                    pictureBox3.Invalidate();
                }
            }
        }
      
        public void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Gray;
            timer1.Enabled = true;
            Thread thread1 = new Thread(t =>
            {
                Random r = new Random();
                for (int i = 0; i <= k1; i++)
                {
                    arr.Add(r.Next(10, 300));
                    spis.Add(r.Next(1, 5));
                }
            })
            { IsBackground = true };
            thread1.Start();

            pictureBox2.BackColor = Color.LightGray;
            Thread thread2 = new Thread(new ThreadStart(Thread2));
            thread2.Start();

            pictureBox3.BackColor = Color.DarkGray;
            Thread thread3 = new Thread(new ThreadStart(Thread3));
            thread3.Start();
             
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics rg = e.Graphics;
            SolidBrush redbru = new SolidBrush(Color.Red);
            for (int i = 0; i < k1; i++)
            {
                rg.FillEllipse(redbru, arr[i] - 10, arr[i + 1] - 10, 20, 20);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < k1; i++)
            {
                if (arr[i] <= 10) spis[i] = -spis[i]; 
                if (arr[i] >= pictureBox1.Width - 10) spis[i] = -spis[i];
                if (arr[i + 1] <= 10) spis[i + 1] = -spis[i + 1]; 
                if (arr[i + 1] >= pictureBox1.Height - 10) spis[i + 1] = -spis[i + 1];
                arr[i] += spis[i]; 
                arr[i + 1] += spis[i + 1];
                pictureBox1.Invalidate(); 
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics rg = e.Graphics;
            SolidBrush redbru = new SolidBrush(Color.Blue);
            for (int i = 0; i < k2; i++)
            {
                rg.FillEllipse(redbru, arr2[i] - 10, arr2[i + 1] - 10, 20, 20);
            }
        
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics rg = e.Graphics;
            SolidBrush redbru = new SolidBrush(Color.Green);
            for (int i = 0; i < k3; i++)
            {
                rg.FillEllipse(redbru, arr3[i] - 10, arr3[i + 1] - 10, 20, 20);
            }
        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            a = false;
        }

    }    
}
