using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExemploDAO.Dao
{
    public class EspoteDao: IDao<Esporte>
    {

        public Esporte Save(Esporte esporte)
        {
            IConnectionFactory connectionFactory = new DbConnection();
            var conn = connectionFactory.ObterMySqlConnection();
            var sqlCommand = conn.CreateCommand();

            sqlCommand.CommandText = "INSERT INTO Esporte(nome, datadecriacao) Values(?nome,?datadecriacao)";

            sqlCommand.Parameters.AddWithValue("?nome", esporte.Nome);
            sqlCommand.Parameters.AddWithValue("?datadecriacao", esporte.DataDeCricao);

            try
            {
                conn.Open();
                int regitrosAfetados = sqlCommand.ExecuteNonQuery();

                Console.WriteLine("Registros inseridos com sucesso");

                return esporte;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public Esporte Update(Esporte esporte)
        {
            IConnectionFactory connectionFactory = new DbConnection();
            var conn = connectionFactory.ObterMySqlConnection();

            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            string sql = "UPDATE Esporte SET nome=@nome, datadecriacao=@datadecriacao WHERE id=@id";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", esporte.Nome);
            cmd.Parameters.AddWithValue("@datadecriacao", esporte.DataDeCricao);
            cmd.Parameters.AddWithValue("@id", esporte.Id);

            try
            {
                conn.Open();
                var registrosAfetados = cmd.ExecuteNonQuery();

                Console.WriteLine("registro atualizado com sucesso");
                Console.WriteLine($"Registros Afetados: {registrosAfetados}");

                return esporte;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public bool Delete(int id)
        {
            IConnectionFactory connectionFactory = new DbConnection();
            var conn = connectionFactory.ObterMySqlConnection();

            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            string sql = "DELETE FROM Esporte WHERE id=@id";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conn.Open();
                var registrosAfetados = cmd.ExecuteNonQuery();

                Console.WriteLine("registro deletado com sucesso");
                Console.WriteLine($"Registros Afetados: {registrosAfetados}");

                return registrosAfetados >= 1;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Esporte> GetAll()
        {

            IConnectionFactory connectionFactory = new DbConnection();
            var conn = connectionFactory.ObterMySqlConnection();
            string sql = "SELECT id, nome, datadecriacao FROM esporte ";

            var esportes = new List<Esporte>();


            try { 

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    esportes.Add(new Esporte()
                    {
                        Id = rdr.GetInt32(0),
                        Nome = rdr.GetString(1),
                        DataDeCricao = rdr.GetDateTime(2)
                    });;

                }
                rdr.Close();    

                return esportes;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }


        public Esporte GetById(int id)
        {
            IConnectionFactory connectionFactory = new DbConnection();
            var conn = connectionFactory.ObterMySqlConnection();
            string sql = "SELECT id, nome, datadecriacao FROM esporte WHERE id=@id ";

            var esportes = new List<Esporte>();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    esportes.Add(new Esporte()
                    {
                        Id = rdr.GetInt32(0),
                        Nome = rdr.GetString(1),
                        DataDeCricao = rdr.GetDateTime(2)
                    }); ;
                }
                rdr.Close();

                return esportes.FirstOrDefault();
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

    }

}
