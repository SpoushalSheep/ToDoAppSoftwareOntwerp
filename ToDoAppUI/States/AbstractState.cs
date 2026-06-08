using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppUI.ViewModels;

namespace ToDoAppUI.States
{
    public abstract class AbstractState
    {
        public ObservableCollection<TaakViewModel> _takenLijst;
        public ObservableCollection<TaakViewModel> _gesorteerdeLijst;

        public AbstractState(ObservableCollection<TaakViewModel> takenLijstViewModels) { 

            _takenLijst =new ObservableCollection<TaakViewModel>( takenLijstViewModels);
        
        }
        public abstract void Sort();
        public abstract void Save(TaakViewModel taakViewModel);

   
    }
}
