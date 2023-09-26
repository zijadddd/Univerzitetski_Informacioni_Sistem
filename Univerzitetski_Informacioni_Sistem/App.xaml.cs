using Firebase.Auth;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;

            MainPage = new LoginPage();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
        }
    }
}