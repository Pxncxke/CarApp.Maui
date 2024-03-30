using CarApp.Maui.Services;
using CarApp.Maui.ViewModel;
using CarApp.Maui.Views;
using Microsoft.Extensions.Logging;

namespace CarApp.Maui
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


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");

            builder.Services.AddSingleton( s=> ActivatorUtilities.CreateInstance<CarService>(s, dbPath));
            builder.Services.AddSingleton<CarListViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddTransient<CarDetailsPage>();


         




#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
