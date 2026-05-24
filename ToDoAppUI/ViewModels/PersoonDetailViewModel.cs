using System.Windows.Input;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.services;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class PersoonDetailViewModel : ViewModel, IQueryAttributable
    {
        public NavigationService NavigationService { get; }
        public ToDoService ToDoService { get; }
        public MessageService MessageService { get; set; }

        public Persoon Persoon { get; private set; }



        public ICommand BewaarCommand { get; init; } //wordt gebruikt voor de buttons te kunnen gebruiken zonder een click event
        public ICommand AnuleerCommand { get; init; }
        public ICommand VerwijderCommand { get; init; }

        public PersoonDetailViewModel(NavigationService navigationService, ToDoService toDoService, MessageService messageService)
        {
            NavigationService = navigationService;
            ToDoService = toDoService;
            MessageService = messageService;

            BewaarCommand = new Command(async () => await BewaarPersoon()); // geeft het command een betekenisvolle opdracht
            AnuleerCommand = new Command(async () => await AnuleerPersoon());
            VerwijderCommand = new Command(async () => await VerwijderPersoon());


        }


        // is verbonden met de entrys zo dat deze informatie kan worden gebruikt
        private string _voorNaam;
        public string Voornaam
        {
            get => _voorNaam;
            set
            {
                _voorNaam = value;
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
        private string? _url;
        public string? Url
        {
            get => _url;
            set
            {
                _url = value;
                NotifyPropertyChanged();
            }
        }
        private DateTime _geboorteDatum; // dit is er voor dat de date picker een dag kan gebruiken
        public DateTime GeboorteDatum
        {
            get => _geboorteDatum;
            set
            {
                _geboorteDatum = value;
                NotifyPropertyChanged();
            }
        }



        private async Task BewaarPersoon() // belangerijke methode
        {
            if (string.IsNullOrWhiteSpace(Voornaam))
            {
                await Shell.Current.DisplayAlert("Fout", "Voornaam is niet ingevuld", "OK");
                return;
            }else if(string.IsNullOrWhiteSpace(Achternaam))
            {
                await Shell.Current.DisplayAlert("Fout", "Achternaam is niet ingevuld", "OK");
                return;


            }else if(GeboorteDatum >= DateTime.Today) {
                await Shell.Current.DisplayAlert("Fout", "Geboorte Datum niet geldig", "OK");
                return;
            }


                Persoon.Voornaam = Voornaam;
            Persoon.Achternaam = Achternaam;
            Persoon.Url = Url;
            Persoon.GeboorteDatum = DateOnly.FromDateTime(GeboorteDatum);

            ToDoService.BewaarPersoon(Persoon);

            MessageService.Send(new PersoonUpdateMessage(Persoon.Id, ToDoService.GeefPersoonMetId(Persoon.Id)));

            await NavigationService.GoToAsync("..");

        }
        private async Task AnuleerPersoon()
        {
            await NavigationService.GoBackAsync(); // annuleer knop gaat gewoon terug naar de pagina
        }
        private async Task VerwijderPersoon() // verwijderd de persoon
        {
            List<Taak> taken = ToDoService.GeefAlleTaken();

           List<Taak> gelinkteTaken = taken.Where(t => t.Persoon.Id == Persoon.Id).ToList();

            if (gelinkteTaken.Count > 0) { 
                await Shell.Current.DisplayAlert("Fout", "Er zijn nog taken gelinkt aan deze persoon", "OK");
                return;
            }
            string action = await Shell.Current.DisplayActionSheet("ActionSheet: Weet je zeker dat je deze persoon wilt verwijderen?", "Cancel", "Delete");
            if (action == "Delete") {
                ToDoService.VerwijderPersoon(Persoon.Id);
                MessageService.Send(new PersoonUpdateMessage(Persoon.Id, null));
                await NavigationService.GoToAsync("..");
            }
            else
            {
                return;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query) 
        {

            if (query.TryGetValue("PersoonVM", out var obj) && obj is PersoonViewModel PersoonVM)
            {
                Persoon = ToDoService.GeefPersoonMetId(PersoonVM.Id);
                Voornaam = Persoon.Voornaam;
                Achternaam = Persoon.Achternaam;
                GeboorteDatum = Persoon.GeboorteDatum.ToDateTime(TimeOnly.MinValue);
                Url = Persoon.Url;
            }
            else
            {
                Persoon = new Persoon("", "", "", DateOnly.FromDateTime(DateTime.Now));
            }
        }
    }
}
