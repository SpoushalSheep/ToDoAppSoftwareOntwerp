using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.services;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class TakenLijstViewModel : ViewModel
    {
        public MessageService messageService { get; }
        private readonly ToDoService todoService;
        public NavigationService navigationService { get; }

        private string huidigeSortering = "Alles";

        public ICommand GaNaarPersonenLijstPageCommand { get; init; }
        public ICommand GaNaarTaakDetailPageCommand { get; init; }
        public ICommand SorteerAllesCommand { get; init; }
        public ICommand SorteerNietAfgewerktCommand { get; init; }
        public ICommand SorteerMeestRecentCommand { get; init; }


        private ObservableCollection<TaakViewModel> alleOrigineleTaken;
        private ObservableCollection<TaakViewModel> _takenLijst;
        public ObservableCollection<TaakViewModel> TakenLijst
        {
            get => _takenLijst;
            set
            {
                _takenLijst = value;
                NotifyPropertyChanged();
            }
        }

        private TaakViewModel _geselecteerdeTaak;
        public TaakViewModel GeselecteerdeTaak
        {
            get => _geselecteerdeTaak;
            set
            {
                _geselecteerdeTaak = value;
                NotifyPropertyChanged();
                if (_geselecteerdeTaak != null)
                {
                    GaNaarTaakDetailPage(GeselecteerdeTaak);
                    GeselecteerdeTaak = null;

                }
            }
        }


        public TakenLijstViewModel(ToDoService _todoService, NavigationService _navigationService, MessageService _messageService)
        {

            todoService = _todoService;
            navigationService = _navigationService;
            messageService = _messageService;
            GaNaarPersonenLijstPageCommand = new Command(async () => await GaNaarPersonenLijstPage());
            GaNaarTaakDetailPageCommand = new Command(async () => await GaNaarTaakDetailPage());
            SorteerAllesCommand = new Command(() => SorteerAlles());
            SorteerNietAfgewerktCommand = new Command(() => SorteerNietAfgewerkt());
            SorteerMeestRecentCommand = new Command(() => SorteerMeestRecent());
            alleOrigineleTaken = new ObservableCollection<TaakViewModel>(todoService.GeefAlleTaken().Select(ConvertToViewModel));
            SorteerAlles();
            messageService.Register<TaakUpdateMessage>(this, (sender, message) =>
            {
                var taakViewModel = alleOrigineleTaken.FirstOrDefault(o => o.Id == message.TaakUpdate.Id);

                if (taakViewModel != null)
                {

                    taakViewModel.Titel = message.TaakUpdate.Titel;
                    taakViewModel.Beschrijving = message.TaakUpdate.Beschrijving;
                    taakViewModel.Persoon = message.TaakUpdate.Persoon;

                    taakViewModel.UpdateIsAfgewerktZonderMessage(message.TaakUpdate.IsAfgewerkt);
                    if (taakViewModel.IsAfgewerkt && TakenLijst.Contains(taakViewModel) && huidigeSortering == "NietAfgewerkt")
                    {
                        TakenLijst.Remove(taakViewModel);
                    }
                }
                else
                {

                    var nieuweTaakVM = ConvertToViewModel(message.TaakUpdate);
                    alleOrigineleTaken.Add(nieuweTaakVM);

                    if (huidigeSortering == "Alles")
                    {
                        TakenLijst.Add(nieuweTaakVM);
                    }
                    else if (huidigeSortering == "NietAfgewerkt" && !nieuweTaakVM.IsAfgewerkt)
                    {
                        TakenLijst.Add(nieuweTaakVM);
                    }
                    else if (huidigeSortering == "MeestRecent")
                    {
                        TakenLijst.Insert(0, nieuweTaakVM);
                    }

                }
            });
        }


        private TaakViewModel ConvertToViewModel(Taak taak)
        {
            TaakViewModel taakvm = new TaakViewModel(todoService, messageService)
            {
                Id = taak.Id,
                Persoon = taak.Persoon,
                Titel = taak.Titel,
                Beschrijving = taak.Beschrijving,

                DatumTaakAanmaak = taak.DatumTaakAanmaak


            };
            taakvm.UpdateIsAfgewerktZonderMessage(taak.IsAfgewerkt);
            return taakvm;

        }

        private void SorteerMeestRecent()
        {
            TakenLijst = new ObservableCollection<TaakViewModel>(alleOrigineleTaken.OrderByDescending(t => t.DatumTaakAanmaak));
            huidigeSortering = "MeestRecent";

        }

        private void SorteerNietAfgewerkt()
        {
            TakenLijst = new ObservableCollection<TaakViewModel>(alleOrigineleTaken.Where(t => !t.IsAfgewerkt));
            huidigeSortering = "NietAfgewerkt";

        }

        private void SorteerAlles()
        {
            TakenLijst = new ObservableCollection<TaakViewModel>(alleOrigineleTaken);
            huidigeSortering = "Alles";
        }

        public async Task GaNaarPersonenLijstPage()
        {
            await navigationService.GoToPersonenLijstPageAsync();
        }
        public async Task GaNaarTaakDetailPage(TaakViewModel geselecteerdeTaak)
        {
            await navigationService.GoToTaakDetailAsync(new ShellNavigationQueryParameters
                {
                { "TaakVM", geselecteerdeTaak }
                });
        }
        public async Task GaNaarTaakDetailPage()
        {
            await navigationService.GoToTaakDetailAsync();
        }


    }
}
