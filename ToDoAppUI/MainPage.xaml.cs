using ToDoAppUI.ViewModels;

namespace ToDoAppUI
{
    public partial class MainPage : ContentPage
    {


        public MainPage(TakenLijstViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }
}
