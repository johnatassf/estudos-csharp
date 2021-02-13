using MySql.Data.MySqlClient;

namespace ExemploDAO
{
    public interface IConnectionFactory
    {
        public MySqlConnection ObterMySqlConnection();
        public void TryToConnectDataBase();
    }
}