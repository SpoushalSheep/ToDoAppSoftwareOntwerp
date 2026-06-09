using System;
using System.Collections.Generic;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;

namespace ToDoAppBL.Services
{
    public class ToDoService
    {
        private readonly IPersoonRepository _persoonRepository;
        private readonly ITaakRepository _taakRepository;
        public MessageService MessageService { get; set; }


        public ToDoService(IPersoonRepository toDoRepository, ITaakRepository taakRepository, MessageService messageService)
        {
            _persoonRepository = toDoRepository;
            _taakRepository = taakRepository;
            MessageService = messageService;
        }

        public List<Persoon> GeefAllePersonen()
        {
            return _persoonRepository.GeefAllePersonen();

        }

        public Persoon GeefPersoonMetId(int id)
        {
             return _persoonRepository.GeefPersoonMetId(id);
        }

        public void BewaarPersoon(Persoon persoon, bool IsNew)
        {

            if (persoon.Id == 0)
            {
                persoon.DatumProfielAanmaak = DateTime.Now;
            }

            persoon.DatumProfielWijziging = DateTime.Now;

            _persoonRepository.BewaarPersoon(persoon);
            if (IsNew)
            {
                MessageService.Send(new PersoonAddMessage(persoon));
            }
            else
            {
                MessageService.Send(new PersoonUpdateMessage(persoon.Id, GeefPersoonMetId(persoon.Id)));
                List<Taak> taken = GeefAlleTaken();

                List<Taak> gelinkteTaken = taken.Where(t => t.Persoon.Id == persoon.Id).ToList();
                foreach (Taak t in taken)
                {

                    t.Persoon.Voornaam = persoon.Voornaam;
                    t.Persoon.Achternaam = persoon.Achternaam;
                    t.Persoon.Url = persoon.Url;
                    t.Persoon.GeboorteDatum = persoon.GeboorteDatum;

                    BewaarTaak(t,false);
                }
            }
        }

        public void VerwijderPersoon(Persoon persoon)
        {

            _persoonRepository.VerwijderPersoon(persoon.Id);
            MessageService.Send(new PersoonVerwijderMessage(persoon));
        }
        public void BewaarTaak(Taak taak, bool IsNew)
        {
            if (IsNew)
            {
                MessageService.Send(new TaakAddMessage(taak));

            }
            else
            {
                MessageService.Send(new TaakUpdateMessage(taak));

            }

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
