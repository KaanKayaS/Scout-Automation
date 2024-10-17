using Scout_Otomasyonu_Framework.sınıflar;
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

namespace Scout_Otomasyonu_Framework
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlCommand commandList = new SqlCommand("Select * from Istatistik", SqlOp.connection);

            SqlOp.Checkconnection(SqlOp.connection);

            SqlDataAdapter dap = new SqlDataAdapter(commandList);

            DataTable dtb = new DataTable();

            dap.Fill(dtb);

            dataGridView1.DataSource = dtb;
        }
    }
}
