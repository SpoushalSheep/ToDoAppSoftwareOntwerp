using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoAppUI.ViewModels.Base
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; // maakt een "heerser" aan om ovre te erven aangezien veel vm NotifyProperty nodig hebben handig om geen 1000 keer het zelfde te moeten schrijven

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
