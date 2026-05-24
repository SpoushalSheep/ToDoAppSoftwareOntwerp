using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppBL.Services
{
    public class ToDoService
    {
        private readonly IToDoRepository ToDoRepository;

        

        public ToDoService(IToDoRepository toDoRepository)
        {
            ToDoRepository = toDoRepository;
           
        }

        public List<Persoon> GeefAllePersonen()
        {
            return ToDoRepository.GeefAllePersonen();

        }

        public Persoon GeefPersoonMetId(int id)
        {
            return ToDoRepository.GeefPersoonMetId(id);
        }

        public void BewaarPersoon(Persoon persoon)
        {

            if (persoon.Id == 0)
            {
                persoon.DatumProfielAanmaak = DateTime.Now;
            }

            persoon.DatumProfielWijziging = DateTime.Now;

            ToDoRepository.BewaarPersoon(persoon);

        }

        public void VerwijderPersoon(int id)
        {

            ToDoRepository.VerwijderPersoon(id);
        }
        public void BewaarTaak(Taak taak)
        {

            if (taak.Id == 0)
            {
                taak.DatumTaakAanmaak = DateTime.Now;
            }

            taak.DatumTaakWijziging = DateTime.Now;

            ToDoRepository.BewaarTaak(taak);
            
        }
        public List<Taak> GeefAlleTaken()
        {
            return ToDoRepository.GeefAlleTaken();

        }
        public Taak GeefTaakMetId(int id)
        {
            return ToDoRepository.GeefTaakMetId(id);
        }


    }
}
