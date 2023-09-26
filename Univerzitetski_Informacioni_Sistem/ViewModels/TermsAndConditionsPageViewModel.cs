namespace Univerzitetski_Informacioni_Sistem.ViewModels;

public class TermsAndConditionsPageViewModel
{
    public Command GoBackCommand { get; }
    public TermsAndConditionsPageViewModel()
    {
        GoBackCommand = new Command(GoBack);
    }

    private async void GoBack()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
