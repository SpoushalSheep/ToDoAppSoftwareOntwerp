namespace ToDoAppUI.services
{
    public class NavigationService
    {

        public async Task GoToAsync(string routeName, ShellNavigationQueryParameters parameters = null)
        {
            if (parameters == null)
                await Shell.Current.GoToAsync(routeName); // hier wordt er gewoon genavigeerd naar de bestemming
            else
                await Shell.Current.GoToAsync(routeName, parameters); // hier worden er objecten mee gegeven
        }

        public async Task GoToPersonenLijstPageAsync()
        {
            await GoToAsync(nameof(PersonenLijstPage));
        }

        public async Task GoBackAsync(ShellNavigationQueryParameters parameters = null)
        {
            if (parameters == null)
                await Shell.Current.GoToAsync("..");
            else
                await Shell.Current.GoToAsync("..", parameters); //zorgt er voor dat de OC automatisch wordt aangepast als annuleren wordt aan geduid keert hij terug zonder gevens te weizigen
        }

        public async Task GoToPersoonDetailAsync(ShellNavigationQueryParameters parameters = null)
        {
            await GoToAsync(nameof(PersoonDetailPage), parameters); //geeft een persoonVM mee als dit nodig is als er op nieuw wordt gedrukt geeft hij niks mee
        }
        public async Task GoToTaakDetailAsync(ShellNavigationQueryParameters parameters = null)
        {
            await GoToAsync(nameof(TaakDetailPage), parameters); //geeft een persoonVM mee als dit nodig is als er op nieuw wordt gedrukt geeft hij niks mee
        }
    }
}
