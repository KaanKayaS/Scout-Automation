using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scout_Otomasyonu_Framework.sınıflar
{
    public class SqlOp
    {
        public static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8N7R7RL\\SQLEXPRESS;Initial Catalog=Scout;Integrated Security=True;Encrypt=False");

        public static void Checkconnection(SqlConnection tempConnection)
        {
            if (tempConnection.State == ConnectionState.Closed)
            {
                tempConnection.Open();
            }
            else
            {

            }
        }
    }
}



