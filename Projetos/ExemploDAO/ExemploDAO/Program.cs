using ExemploDAO.Dao;
using System;
using System.Linq;

namespace ExemploDAO
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory connectionFactory = new DbConnection();
            connectionFactory.TryToConnectDataBase();

            IDao<Esporte> esporteDao = new EspoteDao();

            CreateSport(esporteDao);
            //UpdateSport(esporteDao);
            //GetAllSports(esporteDao);
            //GetByIdSports(esporteDao, 1);
            //DeleteSports(esporteDao, 2);



            Console.ReadKey();
        }

        public static void CreateSport(IDao<Esporte> esporteDao)
        {
            var esporte = new Esporte();
            esporte.Nome = "Basket";
            esporte.DataDeCricao = new DateTime(1891, 12, 21);

            var esporte2 = new Esporte();
            esporte2.Nome = "Volei";
            esporte.DataDeCricao = new DateTime(1895, 2, 09);

            esporteDao.Save(esporte);
            esporteDao.Save(esporte2);
        }

        public static void UpdateSport(IDao<Esporte> esporteDao)
        {
            var esporte = esporteDao.GetAll().FirstOrDefault();
            if (!(esporte is null))
            {
                esporte.Nome = "Natação";
                esporte.DataDeCricao = new DateTime(1891, 12, 21);

                esporteDao.Update(esporte);
            }
        }

        public static void GetAllSports(IDao<Esporte> esporteDao)
        {
            // Get All
            var esportes = esporteDao.GetAll();
            foreach (var esporte in esportes)
                Console.WriteLine(esporte.ToString());
        }

        public static void GetByIdSports(IDao<Esporte> esporteDao, int id)
        {
            var esporte = esporteDao.GetById(id);
            if (esporte is null)
            {
                Console.WriteLine("Esporte não encontrado");
            }
            else
            {
                Console.WriteLine(esporte.ToString());
            }
        }

        public static void DeleteSports(IDao<Esporte> esporteDao, int id)
        {
            var esporte = esporteDao.Delete(id);
            if (esporte)
            {
                Console.WriteLine("Esporte deletado com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao deletar o esporte");
            }
        }


    }   
}
    