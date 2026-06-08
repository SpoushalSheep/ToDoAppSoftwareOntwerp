using ToDoAppBL.Interfaces;
using ToDoAppBL.Models;

namespace ToDoAppBL.Services
{
    public class ToDoService
    {
        private readonly IPersoonRepository _persoonRepository;
        private readonly ITaakRepository _taakRepository;

        

        public ToDoService(IPersoonRepository toDoRepository, ITaakRepository taakRepository)
        {
            _persoonRepository = toDoRepository;
            _taakRepository = taakRepository;
           
        }

        public List<Persoon> GeefAllePersonen()
        {
            return _persoonRepository.GeefAllePersonen();

        }

        public Persoon GeefPersoonMetId(int id)
        {
            return _persoonRepository.GeefPersoonMetId(id);
        }

        public void BewaarPersoon(Persoon persoon)
        {

            if (persoon.Id == 0)
            {
                persoon.DatumProfielAanmaak = DateTime.Now;
            }

            persoon.DatumProfielWijziging = DateTime.Now;

            _persoonRepository.BewaarPersoon(persoon);

        }

        public void VerwijderPersoon(int id)
        {

            _persoonRepository.VerwijderPersoon(id);
        }
        public void BewaarTaak(Taak taak)
        {

            if (taak.Id == 0)
            {
                taak.DatumTaakAanmaak = DateTime.Now;
            }

            taak.DatumTaakWijziging = DateTime.Now;

            _taakRepository.BewaarTaak(taak);
            
        }
        public List<Taak> GeefAlleTaken()
        {
            return _taakRepository.GeefAlleTaken();

        }
        public Taak GeefTaakMetId(int id)
        {
            return _taakRepository.GeefTaakMetId(id);
        }


    }
}
