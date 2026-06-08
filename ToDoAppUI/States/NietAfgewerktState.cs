using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppUI.ViewModels;

namespace ToDoAppUI.States
{
    public class NietAfgewerktState: AbstractState
    {
        public NietAfgewerktState(ObservableCollection<TaakViewModel> takenLijstViewModels) : base(takenLijstViewModels)
        {
        }

        public override void Save(TaakViewModel taakViewModel)
        {
            _takenLijst.Add(taakViewModel);
            Sort();
        }

        public override void Sort()
        {
            _gesorteerdeLijst = new ObservableCollection<TaakViewModel>(this._takenLijst.Where(t => !t.IsAfgewerkt));

        }
    }
}
