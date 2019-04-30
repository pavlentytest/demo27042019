using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FurnituraForm : Form
    {
        public FurnituraForm()
        {
            InitializeComponent();
        }

        private void FurnituraForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'test2DataSet.furniture' table. You can move, or remove it, as needed.
            this.furnitureTableAdapter.Fill(this.test2DataSet.furniture);

        }
    }
}
