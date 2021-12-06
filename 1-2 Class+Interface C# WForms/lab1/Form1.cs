using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Collections;

namespace Class_interface
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        List<Customer> list_customer = new List<Customer>()
        {new Customer(1, "Алексей Алексеев"),
        new Customer(2, "Мария Андрианова"),
        new Customer(3, "Анастасия Платова"),
        new Customer(4, "Александра Анисимова"),
        new Customer(5, "Милена Белова"),
        new Customer(6, "Тимур Бирюков"),
        new Customer(7, "Макар Иванов"),
        new Customer(8, "Виктория Кузьмина"),
        new Customer(9, "Максим Тарасов"),
        new Customer(10, "Даниил Яковлев"),
        new Customer(11, "Кира Галкина"),
        new Customer(12, "Ева Крылова")};

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = list_customer;
            setdatagridview1columnwidth();
            label3.Visible = false;
            label4.Visible = false;
            dataGridView3.DataSource = 1;
            dataGridView3.Update();

        }
        private void setdatagridview1columnwidth()
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 200;
        }
        private void setdatagridview2columnwidth()
        {
            if (dataGridView2.Rows.Count != 0)
            {
                dataGridView2.Columns[0].Width = 60;
                dataGridView2.Columns[1].Width = 120;
                dataGridView2.Columns[2].Width = 60;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
            }
        }
        public class Customer
        {
            [System.ComponentModel.DisplayName("Ключи")]
            public int ID_customer { get; set; }

            [System.ComponentModel.DisplayName("Имя Фамилия")]
            public string Name_customer { get; set; }
            public Customer() { }
            public Customer(int id_customer, string name_customer)
            {
                Name_customer = name_customer;
                ID_customer = id_customer;
            }

        }
        class Product : Customer
        {
            [System.ComponentModel.DisplayName("Ключи")]
            public int ID_product { get; set; }

            [System.ComponentModel.DisplayName("Товары")]
            public string Name_product { get; set; }

            [System.ComponentModel.DisplayName("Цены")]
            public int Price_product { get; set; }
            Customer inner;
            public Product(int id_customer, string name_customer, int id_product, string name_product, int price) : base(id_customer, name_customer)
            {
                inner = new Customer(id_customer, name_customer);
                ID_product = id_product; Name_product = name_product; Price_product = price;
            }
            public string getSecond(string name_customer)
            {
                return name_customer;
            }
            public int getSum(int price)
            {
                return 0;
            }
        }

        List<Product> list_product = new List<Product>(){
        new Product(1,"Алексей Алексеев", 1, "Ноутбук", 10000),
        new Product(1,"Алексей Алексеев", 2, "Провода №1", 500),
        new Product(1,"Алексей Алексеев", 3, "Провода №2", 1000),
        new Product(2,"Мария Андрианова", 1, "Телефон", 5000),
        new Product(2,"Мария Андрианова", 1, "Ноутбук", 10000),
        new Product(3,"Анастасия Платова", 3, "Провода №2", 1000),
        new Product(4,"Александра Анисимова", 6, "Консоль", 2000),
        new Product(5,"Милена Белова", 4, "Радио", 500),
        new Product(6,"Тимур Бирюков", 5, "Видеокарта", 4000)};



        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            label3.Visible = true;
            label4.Visible = true;
            string s = dataGridView1.CurrentCell.Value.ToString();
            label3.Text = s;
            ArrayList list = new ArrayList();
            for (int i = 0; i < list_product.Count; i++)
            {
                if (s == list_product[i].Name_customer)
                {
                    list.Add(list_product[i]);
                }
            }
            dataGridView2.DataSource = list;
            dataGridView2.Update();
            setdatagridview2columnwidth();


            int sum = 0;
            for (int i = 0; i < list_product.Count; i++)
            {
                if (s == list_product[i].Name_customer)
                {
                    sum += list_product[i].Price_product;
                }

            }
            label4.Text = sum.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application(); // класс для запуска Excel
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 25;
            ExcelApp.Cells[1, 1] = "Прибль";
            ExcelApp.Cells[1, 2] = "Ключ";
            ExcelApp.Cells[1, 3] = "Клиент";

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = dataGridView1[i, j].Value;
                }
            }

            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        
        
            for (int i = 0; i < list_product.Count; i++)
            {
                int sum1 = 0;
                for (int j = i+1; j < list_product.Count; j++)
                {
                    if (list_product[i].Name_customer == list_product[j].Name_customer)
                    {
                        sum1 = list_product[i].Price_product;                       
                        list_product[i].Price_product = sum1 + list_product[j].Price_product;
                       
                    }                    
                }
            }

            int index = list_product.Count - 1;
            while (index > 0) {
                if (list_product[index].Name_customer == list_product[index - 1].Name_customer) 
                {
                    list_product.RemoveAt(index);
                    index--; }
                else index--;
            }

            dataGridView3.DataSource = list_product;


            for (int i = 0; i < list_product.Count; i++)
            {
                int j_max = i;
                for (int j = i + 1; j < list_product.Count; j++)
                {
                    if (list_product[j_max].Price_product < list_product[j].Price_product)
                    {
                        j_max = j;
                    }
                }
                Product temp = list_product[i];
                list_product[i] = list_product[j_max];
                list_product[j_max] = temp;
            }
            dataGridView1.DataSource = list_product;
            dataGridView2.Visible = false;
            dataGridView1.Width = 500;
            dataGridView1.Columns.RemoveAt(0);
            dataGridView1.Columns.RemoveAt(0);
            dataGridView1.Update();
        }
    }
}


