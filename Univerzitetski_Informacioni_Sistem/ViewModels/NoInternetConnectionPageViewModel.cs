using Univerzitetski_Informacioni_Sistem.Helpers;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;

public class NoInternetConnectionPageViewModel : ViewModelBase
{
    public Command GoBackCommand { get; }
    public NoInternetConnectionPageViewModel()
    {
        GoBackCommand = new Command(GoBack);
    }

    private async void GoBack()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            Methods.ToastMethod("Uređaj nema pristup Internetu.");
        else await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
