using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppDL.Repositories
{
    public class PersoonRepository: IPersoonRepository
    {
        private DataBaseConnection DatabaseConnection { get; }

        public ILiteCollection<Persoon> GetCollection()
        {
            return DatabaseConnection.GetCollection<Persoon>();
        }

        public PersoonRepository(DataBaseConnection dataBaseConnection) // pad/naam van de database als er nog geen db bestaat maakt hij er 1 aan
        {
           DatabaseConnection = dataBaseConnection;
        }

        public void BewaarPersoon(Persoon p)
        {
           GetCollection().Upsert(p);
        }
        public void VerwijderPersoon(int id)
        {
             GetCollection().Delete(id);

        }

        public List<Persoon> GeefAllePersonen()
        {
            return GetCollection().FindAll().ToList();

        }
        //Weet niet of dit nodig is
        public Persoon GeefPersoonMetId(int id)
        {
            return GetCollection().FindById(id);
        }
    }
}
