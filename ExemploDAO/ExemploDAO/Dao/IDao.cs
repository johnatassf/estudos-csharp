using System;
using System.Collections.Generic;
using System.Text;

namespace ExemploDAO.Dao
{
    public interface IDao<T>
    {
        public T Save(T objectSave);
        public T Update(T objectUpdate);
        public bool Delete(int Id);
        public List<T> GetAll();
        public T GetById(int id);

    }
}
