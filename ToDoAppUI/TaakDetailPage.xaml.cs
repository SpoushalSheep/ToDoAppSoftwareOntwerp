using ToDoAppUI.ViewModels;

namespace ToDoAppUI;

public partial class TaakDetailPage : ContentPage
{
    public TaakDetailPage(TaakDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel; // bindt aan het vm van het programma
    }
}