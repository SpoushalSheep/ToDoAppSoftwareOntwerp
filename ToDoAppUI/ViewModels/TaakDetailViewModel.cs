using System.Windows.Input;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.services;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class TaakDetailViewModel : ViewModel, IQueryAttributable
    {
        public MessageService messageService { get; set; }
        public NavigationService navigationService { get; }
        public ToDoService toDoService { get; }

        public ICommand BewaarCommand { get; init; }
        public ICommand AnnuleerCommand { get; init; }

        private List<Persoon> _personen;
        public List<Persoon> Personen
        {
            get => _personen;
            set
            {
                _personen = value;
                NotifyPropertyChanged();
            }
        }
        public Taak Taak { get; private set; }

        private string _titel;
        public string Titel
        {
            get => _titel;

            set
            {
                _titel = value;
                NotifyPropertyChanged();
            }
        }

        private string _beschrijving;
        public string Beschrijving
        {
            get => _beschrijving;

            set
            {
                _beschrijving = value;
                NotifyPropertyChanged();
            }
        }

        private Persoon _persoon;
        public Persoon Persoon
        {
            get => _persoon;

            set
            {
                _persoon = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isAfgewerkt;
        public bool IsAfgewerkt
        {
            get => _isAfgewerkt;

            set
            {
                _isAfgewerkt = value;

                NotifyPropertyChanged();
            }
        }
       
        public TaakDetailViewModel(NavigationService _navigationService, ToDoService _toDoService, MessageService _messageService)
        {
            messageService = _messageService;
            navigationService = _navigationService;
            toDoService = _toDoService;
            BewaarCommand = new Command(async () => await BewaarTaak()); // geeft het command een betekenisvolle opdracht
            AnnuleerCommand = new Command(async () => await AnnuleerTaak());
            Personen = toDoService.GeefAllePersonen();
        }


        private async Task BewaarTaak() // belangerijke methode
        {
            if (string.IsNullOrWhiteSpace(Titel))
            {
                await Shell.Current.DisplayAlert("Fout", "Titel is niet ingevuld", "OK");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Beschrijving))
            {
                await Shell.Current.DisplayAlert("Fout", "beschrijving is niet ingevuld", "OK");
                return;


            }
            else if (Persoon == null)
            {
                await Shell.Current.DisplayAlert("Fout", "geen persoon toegewezen", "OK");
                return;
            }


            Taak.Titel = Titel;
            Taak.Beschrijving = Beschrijving;
            Taak.Persoon = Persoon;
            Taak.IsAfgewerkt = IsAfgewerkt;

            toDoService.BewaarTaak(Taak);

            messageService.Send(new TaakUpdateMessage(Taak));
            await navigationService.GoToAsync("..");

        }
        private async Task AnnuleerTaak()
        {
            await navigationService.GoBackAsync(); // annuleer knop gaat gewoon terug naar de pagina
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)// zorgt er voor dat mijn propertys worden gelinkt
        {
            if (query.TryGetValue("TaakVM", out var obj) && obj is TaakViewModel taakVM)
            {
                Taak = toDoService.GeefTaakMetId(taakVM.Id);
                Titel = Taak.Titel;
                Beschrijving = Taak.Beschrijving;
                Persoon = Taak.Persoon;
                IsAfgewerkt = Taak.IsAfgewerkt;
                
            }
            else
            {
                Taak = new Taak("", "", false, null);
            }
        }
    }
}
