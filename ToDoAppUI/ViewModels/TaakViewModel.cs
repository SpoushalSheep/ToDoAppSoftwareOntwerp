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

        private Persoon _persooon;
        public Persoon Persoon
        {
            get => _persooon;

            set
            {
                _persooon = value;
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


                var taak = toDoService.GeefTaakMetId(Id);
                taak.IsAfgewerkt = _isAfgewerkt;
                toDoService.BewaarTaak(taak);
                messageService.Send(new TaakUpdateMessage(taak));


            }
        }


        public void UpdateIsAfgewerktZonderMessage(bool waarde)
        {
            if (_isAfgewerkt == waarde) return;
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
        public string PersoonString => $"{Persoon.Voornaam} {Persoon.Achternaam}  ";

        public Taak NaarTaak()
        {
            return new Taak(Titel, Beschrijving, IsAfgewerkt, Persoon)
            {
                Id = Id,
                DatumTaakAanmaak = this.DatumTaakAanmaak
            };
        }
    }
}
