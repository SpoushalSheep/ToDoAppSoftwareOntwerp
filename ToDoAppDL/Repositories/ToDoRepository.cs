using LiteDB;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppDL.Repositories
{
    public class ToDoRepository : IToDoRepository
    {

        private readonly string Pad;

        public ToDoRepository(string pad = "ToDoDataBase.db") // pad/naam van de database als er nog geen db bestaat maakt hij er 1 aan
        {
            Pad = pad;
        }

        public void BewaarPersoon(Persoon p)
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Persoon>("personen");
                collectie.Upsert(p); // maakt een persoon aan als er nog geen tabel is maakt hij de tabel aan en als de persoon al bestaat past hij de gegevens aan
            }
        }
        public void VerwijderPersoon(int id)
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Persoon>("personen");
                collectie.Delete(id); // verwijderd via id 

            }
        }

        public List<Persoon> GeefAllePersonen()
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Persoon>("personen");
                return collectie.FindAll().ToList(); // geeft een lijst terug met alle personen 
            }
        }
        //Weet niet of dit nodig is
        public Persoon GeefPersoonMetId(int id)
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Persoon>("personen");
                return collectie.FindById(id); // geeft een persoon terug met 
            }
        }


        public void BewaarTaak(Taak t)
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Taak>("taken");
                collectie.Upsert(t);
            }
        }


        public List<Taak> GeefAlleTaken()
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Taak>("taken");
                return collectie.FindAll().ToList();
            }
        }


        public Taak GeefTaakMetId(int id)
        {
            using (LiteDatabase db = new LiteDatabase(Pad))
            {
                var collectie = db.GetCollection<Taak>("taken");
                return collectie.FindById(id);
            }
        }

    }
}
