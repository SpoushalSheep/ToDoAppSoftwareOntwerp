using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppBL.Models;
using ToDoAppUI.ViewModels;

namespace ToDoAppUI.Converters
{
    public static class PersoonConverter
    {

        public static Persoon NaarDomein(PersoonViewModel VM)
        {
            Persoon p =  new Persoon()
            {
                Id = VM.Id,
                Voornaam = VM.Voornaam,
                Achternaam = VM.Achternaam,
                Url = VM.Url,

                GeboorteDatum = DateOnly.FromDateTime(VM.GeboorteDatum)
            };
            return p;
        }
        public static PersoonViewModel NaarViewModel(Persoon p)
        {
            PersoonViewModel persoonvm = new PersoonViewModel()
            {
                Id = p.Id,
                Voornaam = p.Voornaam,
                Achternaam = p.Achternaam,
                Url = p.Url,

                GeboorteDatum = p.GeboorteDatum.ToDateTime(TimeOnly.MinValue)


            };
            return persoonvm;

           
        }

    }
}
