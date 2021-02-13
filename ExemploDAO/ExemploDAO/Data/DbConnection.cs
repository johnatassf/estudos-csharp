using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploDAO
{
    public class DbConnection: IConnectionFactory
    {
        public MySqlConnection ObterMySqlConnection()
        {
            return new MySqlConnection(ObterMySqlConnectionString());
        }

        public string ObterMySqlConnectionString()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "esportes",
                UserID = "root",
                Password = "usbw",
                Port = 3307,
                SslMode = MySqlSslMode.None
            };

            return builder.ToString();
        }

        public void TryToConnectDataBase()
        {
            try
            {
                Console.WriteLine("Try Connect");
                var connection = ObterMySqlConnection();

                connection.Open();

                Console.WriteLine("Success Connect");
                Console.ReadKey();
            }catch(MySqlException sqlException)
            {
                Console.WriteLine("Error Connect");
                Console.WriteLine(sqlException.Message);
                Console.ReadKey();
            }
            
        }

    }
}
