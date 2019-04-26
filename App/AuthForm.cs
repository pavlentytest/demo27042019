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
    public partial class AuthForm : Form
    {

        Form reg;

        public AuthForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            reg = new RegForm();
            reg.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);
            connection.Open();
            String login = textBox1.Text;
            String pass = textBox2.Text;

            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = '" + login + "' AND password = '" + pass + "'", connection);
            SqlDataReader reader = command.ExecuteReader();

            String role = "", name ="";
            while(reader.Read())
            {
               
                role = reader[2].ToString();
                name = reader[3].ToString();
            }

            Form form = null;
            switch (role)
            {
                case "User":
                    form = new UserForm();                   
                    break;
                case "Director":
                    form = new AdminForm();               
                    break;
                case "Ware":
                    form = new WareForm();     
                    break;
                case "Manager":
                    form = new ManagerForm();                 
                    break;                                          
            }           
            this.Hide();
            form.Show();




        }
    }
}