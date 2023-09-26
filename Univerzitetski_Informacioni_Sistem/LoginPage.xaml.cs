using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }
}