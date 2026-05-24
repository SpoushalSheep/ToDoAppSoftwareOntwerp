using ToDoAppBL.Models;

namespace ToDoAppBL.Interfaces
{
    public interface IToDoRepository
    {
        //geeft taken door naar de Repository
        void BewaarPersoon(Persoon p);
        void VerwijderPersoon(int id);
        List<Persoon> GeefAllePersonen();
        Persoon GeefPersoonMetId(int id);



        void BewaarTaak(Taak t);
        List<Taak> GeefAlleTaken();
        Taak GeefTaakMetId(int id);

    }
}
