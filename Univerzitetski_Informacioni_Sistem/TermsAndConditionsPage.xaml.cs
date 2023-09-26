using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class TermsAndConditionsPage : ContentPage
{
    public TermsAndConditionsPage()
    {
        InitializeComponent();
        BindingContext = new TermsAndConditionsPageViewModel();
    }
}