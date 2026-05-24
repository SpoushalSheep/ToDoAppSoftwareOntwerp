namespace ToDoAppUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TaakDetailPage), typeof(TaakDetailPage)); //maakt routes moogelijk
            Routing.RegisterRoute(nameof(PersonenLijstPage), typeof(PersonenLijstPage));
            Routing.RegisterRoute(nameof(PersoonDetailPage), typeof(PersoonDetailPage));

        }
    }
}
