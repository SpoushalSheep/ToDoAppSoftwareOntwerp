using Microsoft.Extensions.Logging;
using ToDoAppBL.Interfaces;
using ToDoAppBL.Services;
using ToDoAppDL.Repositories;
using ToDoAppUI.services;
using ToDoAppUI.ViewModels;

namespace ToDoAppUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

  
            builder.Services.AddSingleton<AppShell>();
            
            builder.Services.AddSingleton<MessageService>();      
            builder.Services.AddSingleton<ToDoService>();         
            builder.Services.AddSingleton<ITaakRepository, TaakRepository>();
            builder.Services.AddSingleton<IPersoonRepository, PersoonRepository>();
            builder.Services.AddSingleton<NavigationService>();   
            builder.Services.AddSingleton<TakenLijstViewModel>(); 
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<PersonenLijstPage>();
            builder.Services.AddSingleton<PersonenLijstViewModel>();

            builder.Services.AddTransient<TaakDetailViewModel>();
            builder.Services.AddTransient<TaakDetailPage>();
            builder.Services.AddTransient<PersoonDetailPage>();
            builder.Services.AddTransient<PersoonDetailViewModel>();
            builder.Services.AddTransient<PersoonViewModel>();
            builder.Services.AddTransient<TaakViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
