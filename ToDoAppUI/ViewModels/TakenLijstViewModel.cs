using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.Converters;
using ToDoAppUI.services;
using ToDoAppUI.States;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class TakenLijstViewModel : ViewModel
    {
        public MessageService messageService { get; }
        private readonly ToDoService todoService;
        public NavigationService navigationService { get; }
        private AbstractState _activeState;
        




        public ICommand GaNaarPersonenLijstPageCommand { get; init; }
        public ICommand GaNaarTaakDetailPageCommand { get; init; }
        public ICommand SorteerAllesCommand { get; init; }
        public ICommand SorteerNietAfgewerktCommand { get; init; }
        public ICommand SorteerMeestRecentCommand { get; init; }


        private ObservableCollection<TaakViewModel> _AlleTaken;
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

            _AlleTaken = new ObservableCollection<TaakViewModel>(todoService.GeefAlleTaken().Select(x => TaakConverter.NaarViewModel(x, todoService, messageService)));
           

            SorteerAlles();
            messageService.Register<TaakUpdateMessage>(this, (sender, message) =>
            {
                var taakViewModel = _AlleTaken.FirstOrDefault(o => o.Id == message.TaakUpdate.Id);

                if (taakViewModel != null)
                {

                    taakViewModel.Titel = message.TaakUpdate.Titel;
                    taakViewModel.Beschrijving = message.TaakUpdate.Beschrijving;
                    taakViewModel.PersoonViewModel = PersoonConverter.NaarViewModel( message.TaakUpdate.Persoon);

                    taakViewModel.UpdateIsAfgewerktZonderMessage(message.TaakUpdate.IsAfgewerkt);
                    if (taakViewModel.IsAfgewerkt && TakenLijst.Contains(taakViewModel) &&  _activeState is NietAfgewerktState)
                    {
                        TakenLijst.Remove(taakViewModel);
                    }


                }
                else
                {

                    TaakViewModel nieuweTaakVM = TaakConverter.NaarViewModel( message.TaakUpdate, _todoService, _messageService);
                    _AlleTaken.Add(nieuweTaakVM);
                    _activeState.Save(nieuweTaakVM);
                    TakenLijst = _activeState._gesorteerdeLijst;
                }
            });
        }


      

        private void SorteerMeestRecent()
        {

            _activeState =  new RecentState(_AlleTaken);
            _activeState.Sort();
            TakenLijst = _activeState._gesorteerdeLijst;

        }

        private void SorteerNietAfgewerkt()
        {

            _activeState = new NietAfgewerktState(_AlleTaken);
            _activeState.Sort();
            TakenLijst = _activeState._gesorteerdeLijst;

        }

        private void SorteerAlles()
        {
            _activeState = new AllesState(_AlleTaken);
            _activeState.Sort();
            TakenLijst = _activeState._gesorteerdeLijst;
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
