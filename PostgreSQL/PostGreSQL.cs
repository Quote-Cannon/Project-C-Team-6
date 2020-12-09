using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;

namespace PgSql
{
    public class PostGreSQL
    {
        List<string> dataItems = new List<string>();

        public void PostgreSQL()
        {
        }

        public List<string> readSQL()
        {
            try
            {
                string connstring = "Server=localhost; Port=5432; Username=fetcher; Password=fetch;  Database=postgres;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM product", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                for (int i = 0; dataReader.Read(); i++)
                {
                    string output = "";
                    for (int j = 0; j < dataReader.FieldCount; j++)
                    {
                        output += dataReader[j].ToString() + ", ";
                    }
                    output = output.Substring(0, output.Length - 2);
                    output += "\r\n";
                    dataItems.Add(output);
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

        public void deleteSQL()
        {
            try
            {
                string connstring = "Server=localhost; Port=5432; Username=fetcher; Password=fetch;  Database=postgres;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM product WHERE dutchName = 'test'", connection);
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
        public void createSQL(string input)
        {
            try
            {
                string connstring = "Server=localhost; Port=5432; Username=fetcher; Password=fetch;  Database=postgres;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO product VALUES (99, 10, 'test', 'testia primus', '{input}', 'picture', 'seed', 'herb', 'high', 'mid', false)", connection);
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }

        public void updateSQL(string input)
        {
            try
            {
                string connstring = "Server=localhost; Port=5432; Username=fetcher; Password=fetch;  Database=postgres;";
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE product SET description = '{input}' WHERE dutchName = 'test'", connection);
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
    }
}
