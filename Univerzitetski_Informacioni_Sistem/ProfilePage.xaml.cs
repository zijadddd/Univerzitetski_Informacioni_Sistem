using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class ProfilePage : ContentPage
{

    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new ProfilePageViewModel();
    }
}