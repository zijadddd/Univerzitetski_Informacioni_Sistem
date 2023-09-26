using Firebase.Auth;
using System.ComponentModel.DataAnnotations;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class LoginPageViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    public Command Command { get; }

    private readonly UserAuthenticationModel _userAuthenticationModel;

    public LoginPageViewModel()
    {
        _authenticationService = new AuthenticationService();
        Command = new Command(LoginButtonTappedAsync);
        _userAuthenticationModel = new UserAuthenticationModel();
    }

    public string Email
    {
        get => _userAuthenticationModel.Email;
        set
        {
            _userAuthenticationModel.Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Password
    {
        get => _userAuthenticationModel.Password;
        set
        {
            _userAuthenticationModel.Password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private async void LoginButtonTappedAsync()
    {
        try
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
            else
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(_userAuthenticationModel, null, null);
                bool isValid = Validator.TryValidateObject(_userAuthenticationModel, validationContext, validationResults, true);
                if (!isValid)
                {
                    Methods.ToastMethod(validationResults.FirstOrDefault().ErrorMessage);
                    return;
                }

                var result = await _authenticationService.AuthenticateUser(Email, Password);

                await SecureStorage.SetAsync("uid", result.User.Uid);
                await SecureStorage.SetAsync("authToken", result.User.Credential.IdToken);

                App.Current.MainPage = new MainPage();
            }
        } catch (FirebaseAuthException ex)
        {
            if (ex.Reason.ToString().Equals("UnknownEmailAddress")) Methods.ToastMethod("Unijeli ste nepostojeću email adresu.");
            else if (ex.Reason.ToString().Equals("WrongPassword")) Methods.ToastMethod("Pogrešna lozinka.");
            else Methods.ToastMethod(ex.Reason.ToString());
        } catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
