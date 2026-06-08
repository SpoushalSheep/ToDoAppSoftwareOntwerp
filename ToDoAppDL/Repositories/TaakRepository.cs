using LiteDB;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppDL.Repositories
{
    public class TaakRepository : ITaakRepository
    {

        private DataBaseConnection _DatabaseConnection { get; }
        private IPersoonRepository _persoonRepository { get; }

        public ILiteCollection<Taak> GetCollection()
        {
            return _DatabaseConnection.GetCollection<Taak>();
        }


        public TaakRepository(DataBaseConnection dataBaseConnection, IPersoonRepository persoonRepository) // pad/naam van de database als er nog geen db bestaat maakt hij er 1 aan
        {
            _DatabaseConnection = dataBaseConnection;
            _persoonRepository = persoonRepository;
        }
        public void BewaarTaak(Taak t)
        {
            GetCollection().Upsert(t);
        }

     


        public List<Taak> GeefAlleTaken()
        {
           List<Taak> taken = GetCollection().FindAll().ToList();
            foreach (Taak t in taken)
            {
                if (t.Persoon?.Id > 0)
                    t.Persoon = _persoonRepository.GeefPersoonMetId(t.Persoon.Id);
            }
            return taken;
        }


        public Taak GeefTaakMetId(int id)
        {
            Taak t = GetCollection().FindById(id);
            if (t.Persoon?.Id > 0)
                t.Persoon = _persoonRepository.GeefPersoonMetId(t.Persoon.Id);
            return t;
        }

    }
}
