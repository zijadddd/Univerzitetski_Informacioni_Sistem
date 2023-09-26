using CommunityToolkit.Mvvm.Messaging;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class ProfilePageViewModel : ViewModelBase
{
    private readonly IUserModelService _userModelService;
    private UserModel UserModel;
    public Command ResetPasswordCommand { get; }
    public Command TermsAndConditionsCommand { get; }
    public Command ChangeProfilePhotoCommand { get; }

    public string FullName
    {
        get => UserModel.FirstName + " " + UserModel.LastName;
        set
        {
            UserModel.FirstName = value.Substring(0, value.IndexOf(' '));
            UserModel.LastName = value.Substring(value.IndexOf(' ') + 1);
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string Birthday
    {
        get => UserModel.Birthday;
        set
        {
            UserModel.Birthday = value;
            OnPropertyChanged(nameof(Birthday));
        }
    }

    public string Location
    {
        get => UserModel.Location;
        set
        {
            UserModel.Location = value;
            OnPropertyChanged(nameof(Location));
        }
    }

    public string PhoneNumber
    {
        get => UserModel.PhoneNumber;
        set
        {
            UserModel.PhoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }

    public string Email
    {
        get => UserModel.Email;
        set
        {
            UserModel.Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Index
    {
        get => UserModel.Index;
        set
        {
            UserModel.Index = value;
            OnPropertyChanged(nameof(Index));
        }
    }

    public string University
    {
        get => UserModel.University;
        set
        {
            UserModel.University = value;
            OnPropertyChanged(nameof(University));
        }
    }

    public string Faculty
    {
        get => UserModel.Faculty;
        set
        {
            UserModel.Faculty = value;
            OnPropertyChanged(nameof(Faculty));
        }
    }

    public string Department
    {
        get => UserModel.Department;
        set
        {
            UserModel.Department = value;
            OnPropertyChanged(nameof(Department));
        }
    }

    public string ProfilePhoto
    {
        get => UserModel.ProfilePhoto;
        set
        {
            UserModel.ProfilePhoto = value;
            OnPropertyChanged(nameof(ProfilePhoto));
        }
    }

    public ProfilePageViewModel()
    {
        _userModelService = new UserModelService();
        ResetPasswordCommand = new Command(ResetPassword);
        TermsAndConditionsCommand = new Command(TermsAndConditions);
        ChangeProfilePhotoCommand = new Command(ChangeProfilePhoto);

        UserModel = new UserModel();

        _ = SetPage();
    }

    private async Task SetPage()
    {
        var user = await _userModelService.GetLoggedUserAsync();
        FullName = user.FirstName + " " + user.LastName;
        Birthday = user.Birthday;
        Location = user.Location;
        PhoneNumber = user.PhoneNumber;
        Email = user.Email;
        Index = user.Index;
        University = user.University;
        Faculty = user.Faculty;
        Department = user.Department;
        if (string.IsNullOrEmpty(user.ProfilePhoto)) ProfilePhoto = "userprofilephotobase.png";
        else ProfilePhoto = user.ProfilePhoto;
    }

    private async void ResetPassword()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new ChangePasswordPage());
        }
    }

    private async void TermsAndConditions()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PushModalAsync(new TermsAndConditionsPage());
    }

    private async void ChangeProfilePhoto()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else
        {
            string profilePhoto = await _userModelService.ChangeUserPhoto();
            if (profilePhoto.Equals("Odustali ste od promjene profilne slike.") || profilePhoto.Equals("Profilna slika nije promijenjena.")) Methods.ToastMethod(profilePhoto);
            else
            {
                ProfilePhoto = profilePhoto;
                WeakReferenceMessenger.Default.Send(profilePhoto);
            }
        }
    }
}
