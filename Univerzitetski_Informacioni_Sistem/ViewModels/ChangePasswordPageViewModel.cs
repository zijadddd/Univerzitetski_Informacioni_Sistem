using Firebase.Auth;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class ChangePasswordPageViewModel
{
    private readonly IUserModelService _userModelService;
    private readonly IAuthenticationService _authenticationService;

    public Command GoBackCommand { get; }
    public Command ChangePasswordCommand { get; }

    public ChangePasswordPageViewModel()
    {
        GoBackCommand = new Command(GoBack);
        ChangePasswordCommand = new Command(ChangePassword);
        _userModelService = new UserModelService();
        _authenticationService = new AuthenticationService();
    }

    private async void GoBack()
    {
        await App.Current.MainPage.Navigation.PopModalAsync();
    }

    private async void ChangePassword()
    {
        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
            else
            {
                var user = await _userModelService.GetLoggedUserAsync();
                await _authenticationService.ChangePassword(user.Email);
                Methods.ToastMethod("Email uspješno poslan.");
            }
        } catch (FirebaseAuthException ex)
        {
            await App.Current.MainPage.DisplayAlert("ok", ex.Message, "ok");
        }
    }
}
