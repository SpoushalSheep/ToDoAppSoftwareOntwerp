using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppDL.Repositories
{
    public class DataBaseConnection
    {
        private LiteDatabase _liteDatabase = new LiteDatabase("ToDoDataBase.db");

        public ILiteCollection<T> GetCollection<T>()
        {
            return _liteDatabase.GetCollection<T>();
        }
    }
}
