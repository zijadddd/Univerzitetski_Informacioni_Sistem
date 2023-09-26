using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class AttendancePage : ContentPage
{
	public AttendancePage(string subjectId)
	{
		InitializeComponent();
		BindingContext = new AttendancePageViewModel(subjectId);
	}
}