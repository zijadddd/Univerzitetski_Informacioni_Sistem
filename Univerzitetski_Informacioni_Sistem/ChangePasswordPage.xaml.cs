using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class ChangePasswordPage : ContentPage
{
    public ChangePasswordPage()
    {
        InitializeComponent();
        BindingContext = new ChangePasswordPageViewModel();
    }
}