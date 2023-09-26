using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class NoInternetConnectionPage : ContentPage
{
    public NoInternetConnectionPage()
    {
        InitializeComponent();
        BindingContext = new NoInternetConnectionPageViewModel();
    }
}