using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class ConstructorForm : Form
    {
        private class Tkani
        {
            public int id { get; set; }
            public string название { get; set; }
            public string цвет { get; set; }
            public double цена { get; set; }
             
            public Tkani (int i, string n, string c, double p)
            {
                this.id = i;
                this.название = n;
                this.цвет = c;
                this.цена = p;
            }
        }

        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        
        public ConstructorForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConstructorForm_Load(object sender, EventArgs e)
        {
            // заполнение Combobox1 - списком из таблицы Ткани
            this.tkaniTableAdapter.Fill(this.test2DataSet2.tkani);

            // заполнение Combobox2 - списком из таблицы Фурнитуры
            this.furnitureTableAdapter.Fill(this.test2DataSet.furniture);
           
        }

        /*
        public void fillCombos()
        {
            String sql = "SELECT id, название, цвет, цена FROM tkani";
            connection.Open();
            SqlCommand command = new SqlCommand(sql,connection);
            SqlDataReader reader = command.ExecuteReader();
            List<Tkani> listoftkani = new List<Tkani>();
            
            while(reader.Read())
            {
                listoftkani.Add(new Tkani(Convert.ToInt32(reader["id"]),
                    reader["название"].ToString()+","+ reader["цвет"].ToString(),
                    reader["цвет"].ToString(),
                    Convert.ToDouble(reader["цена"])));
            }
            comboBox1.DataSource = listoftkani;
            comboBox1.DisplayMember = "название";
            reader.Close();
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            // выбранный идентификатор фурнитуры
            int selected_furniture = Convert.ToInt32(comboBox2.SelectedValue);
            // выбранный идентификатор ткани
            int selected_tkani = Convert.ToInt32(comboBox1.SelectedValue);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO izdelie (Наименование, Длина, Ширина) " +
                    "VALUES (@name,@width,@height); SELECT SCOPE_IDENTITY(); ", connection);
                command.Parameters.AddWithValue("@name", textBox1.Text);
                command.Parameters.AddWithValue("@width", textBox2.Text);
                command.Parameters.AddWithValue("@height", textBox3.Text);
                
                int izdelie = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
          
            }
            catch
            {
                MessageBox.Show("Ошибка !\n");

            }

        }
    }
}
