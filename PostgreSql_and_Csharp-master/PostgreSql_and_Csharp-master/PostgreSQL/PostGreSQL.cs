using System;
using System.Windows.Forms;
using Npgsql; 
using System.Collections.Generic;

namespace PgSql
{
   public class PostGreSQL
   {
      List<string> dataItems = new List<string>();

      public void PostgreSQL()
      {
      }

      public List<string> PostgreSQLtest1()
      {
         try
         {
            string connstring = "Server=127.0.0.1; Port=5432; Username=postgres; Password=User_Password;  Database=TestVanProject;";
            NpgsqlConnection connection = new NpgsqlConnection(connstring);
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Product", connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            for (int i=0; dataReader.Read(); i++)
            {
               dataItems.Add(dataReader[0].ToString() + "," + dataReader[1].ToString() + "," +  "\r\n");
            }
            connection.Close();
            return dataItems;
         }
         catch (Exception msg)
         {
            MessageBox.Show(msg.ToString());
            throw;
         }
      }

        public List<string> PostgreSQLtest2()
        {
            try
            {
                string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=1234; Database=my_geo_database;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM bike_route.points WHERE point_names > '005' AND point_names < '010'", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                for (int i = 0; dataReader.Read(); i++)
                {
                    dataItems.Add(dataReader[0].ToString() + "," + dataReader[1].ToString() + "," + dataReader[2].ToString() + "\r\n");
                }
                connection.Close();
                return dataItems;
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }

    }
}
