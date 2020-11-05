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
            Console.WriteLine("Connection opened!");
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Names";
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                    Console.WriteLine(reader["name"]);
            }
            finally
            {
                reader.Close();
                Console.WriteLine("Connection closed.");
            }
            Console.ReadLine();
        }
    }
}