using System;
using System.Data.SqlClient;

namespace SQL_test
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Server=localhost;" + "Database=Database1;");
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from Names";
        }
    }
}
