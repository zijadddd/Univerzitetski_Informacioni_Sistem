using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class SubjectsPage : ContentPage
{
    public SubjectsPage()
    {
        InitializeComponent();
        BindingContext = new SubjectsPageViewModel();
    }
}