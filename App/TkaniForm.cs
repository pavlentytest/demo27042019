using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        DataSet ds;
        SqlDataAdapter sda;
        DataSet changes;

        public TkaniForm()
        {
            InitializeComponent();
        }

        public void LoadList()
        {
            String query = "SELECT * FROM tkani";
            sda = new SqlDataAdapter(query, connection);
            ds = new DataSet();
            sda.Fill(ds, "tkani");
            dataGridView1.DataSource = ds.Tables["tkani"];

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.Name = "img";
            img.HeaderText = "Картинка";
            dataGridView1.Columns.Add(img);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    string basePath = "C:/Users/lickett2019/source/repos/App/App/images/tkani/";
                    string filename = dataGridView1.Rows[i].Cells[1].Value.ToString() + ".jpg";
                    string fullPath = basePath + filename;
                    Image image;
                    if (File.Exists(fullPath))
                    {
                        image = Image.FromFile(fullPath);
                    }
                    else
                    {
                        image = Image.FromFile(basePath + "empty.jpg");
                    }
                    dataGridView1.Rows[i].Cells["img"].Value = image;
                }
            }
        }

        private void TkaniForm_Load(object sender, EventArgs e)
        {

            this.LoadList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            ware = new WareForm();
            ware.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changes = ds.GetChanges();
            if (changes != null)
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                builder.GetInsertCommand();
                int updatesRows = sda.Update(changes,"tkani");
                ds.AcceptChanges();
            }
            this.LoadList();
            

            MessageBox.Show("Успешно!", "Заголовок", MessageBoxButtons.OKCancel);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow items in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(items.Index);              
            }
            this.button2_Click(sender,e);
        }
    }
}
