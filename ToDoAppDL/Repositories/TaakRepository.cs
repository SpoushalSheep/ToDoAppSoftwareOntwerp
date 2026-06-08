using LiteDB;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppDL.Repositories
{
    public class TaakRepository : ITaakRepository
    {

        private readonly string Pad;

        public TaakRepository(string pad = "ToDoDataBase.db") // pad/naam van de database als er nog geen db bestaat maakt hij er 1 aan
        {
            Pad = pad;
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
