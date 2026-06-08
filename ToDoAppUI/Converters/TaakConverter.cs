    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ToDoAppBL.Models;
    using ToDoAppBL.Services;
    using ToDoAppUI.ViewModels;

    namespace ToDoAppUI.Converters
    {
        public static class TaakConverter
        {


            public static Taak NaarDomein(TaakViewModel VM)
            {
                Taak t = new Taak()
                {
                    Id = VM.Id,
                    Persoon = PersoonConverter.NaarDomein(VM.PersoonViewModel),
                    Titel = VM.Titel,
                    Beschrijving = VM.Beschrijving,
                    IsAfgewerkt = VM.IsAfgewerkt,
                    DatumTaakAanmaak = VM.DatumTaakAanmaak
                };
                return t;
            }
            public static TaakViewModel NaarViewModel(Taak t, ToDoService ts , MessageService ms)
            {
                TaakViewModel VM= new TaakViewModel(ts , ms)
                {
                    Id = t.Id,
                    PersoonViewModel = PersoonConverter.NaarViewModel(t.Persoon),
                    Titel = t.Titel,
                    Beschrijving = t.Beschrijving,
                    //IsAfgewerkt=t.IsAfgewerkt,
                    DatumTaakAanmaak = t.DatumTaakAanmaak


                };
            VM.UpdateIsAfgewerktZonderMessage(t.IsAfgewerkt);
            return VM;


            }
        }
    }
