using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class SubjectPage : ContentPage
{
    public SubjectPage(string subjectId)
    {
        InitializeComponent();
        BindingContext = new SubjectPageViewModel(subjectId);
    }
}