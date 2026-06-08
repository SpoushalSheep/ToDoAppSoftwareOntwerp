using ToDoAppBL.Models;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class PersoonViewModel : ViewModel
    {


        public int Id { get; set; }
        public int Leeftijd => LeeftijdOmzetter(); //is verbonden met een binding voor in het overzicht de leeftijd te kunnen weergeven en niet de geboorte datum

        private string _voornaam;
        public string Voornaam
        {
            get => _voornaam;

            set
            {
                _voornaam = value;
                NotifyPropertyChanged();
            }
        }

        private string _achternaam;
        public string Achternaam
        {
            get => _achternaam;

            set
            {
                _achternaam = value;
                NotifyPropertyChanged();
            }
        }

        private string _url;
        public string? Url
        {
            get => _url;

            set
            {
                _url = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _geboorteDatum;



        public DateTime GeboorteDatum
        {
            get => _geboorteDatum;
            set
            {
                _geboorteDatum = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Leeftijd));
            }
        }


        public int LeeftijdOmzetter()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - GeboorteDatum.Year;

            if (today < GeboorteDatum.AddYears(age))
                age--;

            return age;
        }

        public Persoon NaarPersoon()
        {
            DateOnly geboorteDatumDateOnly = DateOnly.FromDateTime(GeboorteDatum);
            return new Persoon(Voornaam, Achternaam, Url, geboorteDatumDateOnly)
            {
                Id = Id
            };
        }

        public override string ToString()
        {
            return $"{Voornaam} {Achternaam} ({Leeftijd})";
        }
    }
}
