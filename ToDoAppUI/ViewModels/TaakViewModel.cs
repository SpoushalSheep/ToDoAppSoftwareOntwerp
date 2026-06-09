using ToDoAppBL.Messages;
using ToDoAppBL.Models;
using ToDoAppBL.Services;
using ToDoAppUI.ViewModels.Base;

namespace ToDoAppUI.ViewModels
{
    public class TaakViewModel : ViewModel
    {
        public int Id { get; set; }

        private readonly ToDoService toDoService;
        private readonly MessageService messageService;

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

        private PersoonViewModel _persoonViewModel;
        public PersoonViewModel PersoonViewModel
        {
            get => _persoonViewModel;

            set
            {
                _persoonViewModel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isAfgewerkt;
        public bool IsAfgewerkt
        {
            get => _isAfgewerkt;

            set
            {
                if (_isAfgewerkt == value) return;
                _isAfgewerkt = value; 
                NotifyPropertyChanged();

                if (Id == 0) return;

                
                Taak taak = NaarTaak();
                toDoService.BewaarTaak(taak,false);
                messageService.Send(new TaakUpdateMessage(taak));


            }
        }
        

        public void UpdateIsAfgewerktZonderMessage(bool waarde) // reageert op een bericht zodat er geen oneindige lus onstaat
        {
            
            _isAfgewerkt = waarde;
            NotifyPropertyChanged(nameof(IsAfgewerkt));
        }
        private DateTime _datumTaakAanmaak;

        public TaakViewModel(ToDoService _toDoService, MessageService _messageService)
        {
            toDoService = _toDoService;
            messageService = _messageService;
        }

        public DateTime DatumTaakAanmaak
        {
            get => _datumTaakAanmaak;
            set
            {
                _datumTaakAanmaak = value;
                NotifyPropertyChanged();
            }
        }
        public string PersoonString => $"{PersoonViewModel.Voornaam} {PersoonViewModel.Achternaam}  ";

        public Taak NaarTaak()
        {
            return new Taak(Titel, Beschrijving, IsAfgewerkt, PersoonViewModel.NaarPersoon())
            {
                Id = Id,
                DatumTaakAanmaak = this.DatumTaakAanmaak
            };
        }
    }
}
