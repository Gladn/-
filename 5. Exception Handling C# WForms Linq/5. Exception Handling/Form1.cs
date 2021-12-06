using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _5._Exception_Handling
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string connectionString = Properties.Settings.Default.con;
        static List<Bludo> BludoList = new List<Bludo>();
        static List<Postavshik> PostavshikList = new List<Postavshik>();
        static List<Produkt> ProduktList = new List<Produkt>();

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadDataAsync();
        }
        private async void ReadDataAsync()
        {
            try
            {
                //stat

                await Task.Run(() => ReadData());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Пример исключения: Ошибка считывания данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            dataGridView1.DataSource = ProduktList.Select(x => new { x.Id, x.Name }).ToList();
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows[0].Selected = true;

            //    dataGridView4.DataSource = BludoList.Select(x => new { x.Id, x.Name, x.Trud }).ToList();

            //    dataGridView5.DataSource = PostavshikList.Select(x => new { x.Id, x.Name, x.Gorod }).ToList();


        }
        private async Task ReadData()
        {
            // Bluda
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("select bl, bludo, trud from Bluda", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object age = reader.GetValue(2);
                        Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
                        BludoList.Add(new Bludo(
                            (int)reader["bl"],
                            reader["bludo"].ToString(),
                            reader["trud"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["trud"])
                            ));
                    }
                }
                reader.Close();


            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("select pc, nazvanie, gorod from Postavshiki", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        PostavshikList.Add(new Postavshik(
                            (int)reader["pc"],
                            reader["nazvanie"].ToString(),
                            reader["gorod"].ToString()
                            ));
                    }
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("select pc, nazvanie, gorod from Postavshiki", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        PostavshikList.Add(new Postavshik(
                            (int)reader["pc"],
                            reader["nazvanie"].ToString(),
                            reader["gorod"].ToString()
                            ));
                    }
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("select pr, produkt from Produkt order by produkt", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        ProduktList.Add(new Produkt(
                            (int)reader["pr"],
                            reader["produkt"].ToString()
                            ));
                    }
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command;
                SqlDataReader reader;

                foreach (var item in ProduktList)
                {
                    command = new SqlCommand($"select bl, round(vec,0) as vec from Sostav where pr = {item.Id.ToString()}", connection);
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            item.Bluda.Add(new Spisok(
                                (int)reader["bl"],
                                reader["vec"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["vec"])
                                ));
                        }
                    }
                    reader.Close();
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command;
                SqlDataReader reader;

                foreach (var item in ProduktList)
                {
                    command = new SqlCommand($"select kol_vo, stoim from nalishie where pr = {item.Id.ToString()}", connection);
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            item.Nalichiye = new Sklad(
                                reader["kol_vo"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["kol_vo"]),
                                reader["stoim"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["stoim"])
                                );
                        }
                    }
                    reader.Close();
                }
            }

            // Поставки
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command;
                SqlDataReader reader;

                foreach (var item in ProduktList)
                {
                    command = new SqlCommand(
                        $"select pc, kol, data, cena from Postavki " +
                        $"left join Nakladnaya on Postavki.ID_post = Nakladnaya.ID_post " +
                        $"where pr = {item.Id.ToString()}", connection);
                    reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            item.Postavki.Add(new Postavka(
                                (int)reader["pc"],
                                reader["kol"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["kol"]),
                                reader["data"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["data"]),
                                reader["cena"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["cena"])
                                ));
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Produkt selected = ProduktList.Find(x => x.Name == dataGridView1.CurrentRow.Cells[1].Value.ToString());
            if (selected == null) return;
            // Блюда
            dataGridView2.DataSource = selected.Bluda.Join(
                BludoList,
                x => x.BludoId,
                y => y.Id,
                (x, y) => new { y.Name, x.Ves }
                ).ToList();
            // Поставщики
            dataGridView3.DataSource = selected.Postavki.Join(
                 PostavshikList,
                 x => x.PostavshikId,
                 y => y.Id,
                 (x, y) => new { y.Name, y.Gorod }
                 ).Distinct().OrderBy(x => x.Name).ToList();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
