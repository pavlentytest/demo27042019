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
    public partial class UserForm : Form
    {
        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);

        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            String sql = "select * from izdelie";
            SqlDataAdapter sda = new SqlDataAdapter(sql, connection);      
            DataSet ds = new DataSet();          
            sda.Fill(ds, "izdelie");
             dataGridView1.DataSource = ds.Tables["izdelie"];
           

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            Image image = Image.FromFile("C:/users/lickett2019/test.jpg");
            img.Image = image;
            dataGridView1.Columns.Add(img);
            img.HeaderText = "Image";
            img.Name = "img";

        }

        private void cgToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void конструкторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form constr = new ConstructorForm();
            constr.Show();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form order = new UserOrderForm();
            order.Show();
        }
    }
}
