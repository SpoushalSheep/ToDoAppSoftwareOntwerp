using ToDoAppBL.Models;

namespace ToDoAppBL.Interfaces
{
    public interface IPersoonRepository
    {
        //geeft taken door naar de Repository
        void BewaarPersoon(Persoon p);
        void VerwijderPersoon(int id);
        List<Persoon> GeefAllePersonen();
        Persoon GeefPersoonMetId(int id);



       

    }
}
