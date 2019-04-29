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
    public partial class TkaniForm : Form
    {

        SqlConnection connection = new SqlConnection(Properties.Settings.Default.dbConnectionSettings);

        WareForm ware;


        public TkaniForm()
        {
            InitializeComponent();
        }

        private void TkaniForm_Load(object sender, EventArgs e)
        {
           String query = "SELECT * FROM tkani";
           SqlDataAdapter sda = new SqlDataAdapter(query, connection);
           DataSet ds = new DataSet();
           sda.Fill(ds, "tkani");
           dataGridView1.DataSource = ds.Tables["tkani"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            ware = new WareForm();
            ware.Show();
        }
    }
}
