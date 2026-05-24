using ToDoAppUI.ViewModels;

namespace ToDoAppUI;

public partial class PersoonDetailPage : ContentPage
{
    public PersoonDetailPage(PersoonDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}