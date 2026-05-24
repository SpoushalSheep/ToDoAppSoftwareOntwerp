using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.services;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class PersonenLijstViewModel : ViewModel 
    {

        public NavigationService navigationService { get; } //vraagt NavigatieSercice bij het aanmaken van de pagina
        public ToDoService toDoService { get; } //vraagt TodoService bij het aanmaken van de pagina
        public MessageService messageService { get; }

        public ICommand GaNaarNieuwPersoon { get; init; } // defineert een command voor knoppen te laten werken

        private ObservableCollection<PersoonViewModel> _personen;
        public ObservableCollection<PersoonViewModel> Personen
        {
            get => _personen;
            set
            {
                _personen = value;
                NotifyPropertyChanged(); // roept method aan in de overheersende klasse
            }
        }

        public PersonenLijstViewModel(NavigationService _navigationService, ToDoService _toDoService, MessageService _messageService) // maakt object aan
        {
            navigationService = _navigationService;
            toDoService = _toDoService;
            messageService = _messageService;

            Personen = new ObservableCollection<PersoonViewModel>(toDoService.GeefAllePersonen().Select(ConvertToViewModel)); // vraagt alle personen op en zet ze om naar PersoonVM
            GaNaarNieuwPersoon = new Command(async () => await OnNieuwPersoon()); // geeft het command een "job"
            RegistreerMessage();

        }

        private void RegistreerMessage()
        {
            messageService.Register<PersoonUpdateMessage>(this, (sender, message) =>
            {
                if (message.PersoonUpdate == null)
                {

                    var teVerwijderen = Personen.FirstOrDefault(o => o.Id == message.PersoonId, null);
                    if (teVerwijderen != null)
                        Personen.Remove(teVerwijderen);
                    return;
                }

                var persoonViewModel = Personen.FirstOrDefault(o => o.Id == message.PersoonUpdate.Id);
                if (persoonViewModel != null)
                {
                    persoonViewModel.Voornaam = message.PersoonUpdate.Voornaam;
                    persoonViewModel.Achternaam = message.PersoonUpdate.Achternaam;
                    persoonViewModel.Url = message.PersoonUpdate.Url;
                    persoonViewModel.GeboorteDatum = message.PersoonUpdate.GeboorteDatum.ToDateTime(TimeOnly.MinValue);
                }
                else
                {
                    var nieuwePersoonVM = ConvertToViewModel(message.PersoonUpdate);
                    Personen.Add(nieuwePersoonVM);
                }
            });
        }
        private async Task OnNieuwPersoon()
        {
            await navigationService.GoToPersoonDetailAsync(); //gaat zonder een object mee te geven naar PersoonDetailPage
        }
        private PersoonViewModel _geselcteerdePersoon;
        public PersoonViewModel GeselecteerdePersoon
        {
            get => _geselcteerdePersoon;
            set
            {
                _geselcteerdePersoon = value;
                NotifyPropertyChanged();
                if (_geselcteerdePersoon != null)
                {
                    GaNaarPersoonDetailScherm(GeselecteerdePersoon);
                    GeselecteerdePersoon = null;

                }
            }
        }

        private PersoonViewModel ConvertToViewModel(Persoon persoon) // methode om van een Model om te zetten naar een ViewModel
        {
            return new PersoonViewModel()
            {
                Id = persoon.Id,
                Voornaam = persoon.Voornaam,
                Achternaam = persoon.Achternaam,
                Url = persoon.Url,
                GeboorteDatum = persoon.GeboorteDatum.ToDateTime(TimeOnly.MinValue) // zet een dateOnly van het model om naar een dateTime dit zorgt er voor de de datepicker gekozen kan worden

            };
        }
        private async Task GaNaarPersoonDetailScherm(PersoonViewModel geselecteerdePersoon) // geeft een vm mee om dan automatisch de aanpassingen te verander in OC
        {
            await navigationService.GoToPersoonDetailAsync(new ShellNavigationQueryParameters
            {
            { "PersoonVM", geselecteerdePersoon }
            });

        }
       
    }
}
