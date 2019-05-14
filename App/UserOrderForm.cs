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
    public partial class UserOrderForm : Form
    {

        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
        double total = 0;
        double izdelie_price = 0;
        List<ComboBox> lst = new List<ComboBox>();
        String user = "";
      

        public UserOrderForm(String u)
        {
            InitializeComponent();
            this.user = u;
        }

        private void UserOrderForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'test2DataSet3.izdelie' table. You can move, or remove it, as needed.
            izdelieTableAdapter.Fill(test2DataSet3.izdelie);
           
          System.Object[] items2 = new System.Object[101];
            for(int i=0;i<101;i++)
            {
                items2[i] = i;
            }
            comboBox2.Items.AddRange(items2);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
         /*   ComboBox combo = new ComboBox();
            combo.Name = "Combobox" + counter;
            combo.DataSource = izdelieBindingSource;
            combo.DisplayMember = "Наименование";
            combo.SelectedIndexChanged += new EventHandler(this.combo_SelectedIndexChanged);
            flowLayoutPanel1.Controls.Add(combo);
            lst.Add(combo);
            counter++;
            */
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView item = (DataRowView)comboBox1.SelectedItem;

            if (item != null)
            {
                double item_width = Convert.ToDouble(item.Row.ItemArray[3]);
                double item_height = Convert.ToDouble(item.Row.ItemArray[4]);

                connection.Open();
                SqlCommand command = new SqlCommand("SELECT tkani.ID, tkani.ширина, tkani.длина, tkani.цена " +
                    "FROM tkani_izdelie INNER JOIN tkani ON tkani_izdelie.tkani_id = tkani.ID " +
                    "WHERE tkani_izdelie.izdelie_id=" + comboBox1.SelectedValue
                    , connection);
                SqlDataReader reader = command.ExecuteReader();
                int id = 0;
                double width = 0;
                double height = 0;
                double price = 0;
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader[0] == DBNull.Value ? 0 : reader[0]);
                    width = Convert.ToDouble(reader[1] == DBNull.Value ? 0 : reader[1]);
                    height = Convert.ToDouble(reader[2] == DBNull.Value ? 0 : reader[2]);
                    price = Convert.ToDouble(reader[3] == DBNull.Value ? 0 : reader[3]);
                }

                izdelie_price = (item_width * item_height * price) / (width * height);
                total = izdelie_price * Convert.ToInt32(comboBox2.SelectedIndex);
                label6.Text = total.ToString();


                reader.Close();
                connection.Close();
            }
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
       
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        total = izdelie_price * Convert.ToInt32(comboBox2.SelectedIndex);
        label6.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                Random random = new Random();               
                SqlCommand command = new SqlCommand("INSERT INTO [order] ([date], stage, client, manager, price) " +
                    "VALUES (getdate(),@stage,@client,@manager,@price); SELECT SCOPE_IDENTITY(); ", connection);
                command.Parameters.AddWithValue("@stage", "Новый");
                command.Parameters.AddWithValue("@client", user);
                command.Parameters.AddWithValue("@manager", "manager"); // from users table
                command.Parameters.AddWithValue("@price", total); // from users table
                
                int order = Convert.ToInt32(command.ExecuteScalar());

                SqlCommand command1 = new SqlCommand("INSERT INTO order_izdelie (order_id, izdelie_id,counter) " +
                   "VALUES (" + order + "," + comboBox1.SelectedValue + ", 1);", connection);
                command1.ExecuteScalar();

                MessageBox.Show("Ваш заказ N"+ order + "  забронирован!");
                
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Ошибка !\n");

            }
        }


        /* private void combo_SelectedIndexChanged(object sender, EventArgs e)
         {
            var test = (ComboBox)sender;
             var selection = test.Name;
             MessageBox.Show(selection);
             switch (selection) {
                 case "Combobox2":
                     MessageBox.Show("2");
                     break;
                 case "Combobox3":
                     MessageBox.Show("3");
                     break;
             }

         }*/
    }
}
