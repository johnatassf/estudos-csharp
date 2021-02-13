using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploDAO
{
    public class Esporte
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeCricao { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Id, Nome, DataDeCricao);
        }


    }
}
