using ToDoAppUI.ViewModels;

namespace ToDoAppUI;

public partial class PersonenLijstPage : ContentPage
{
    public PersonenLijstPage(PersonenLijstViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }




}