namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class QRCodeScannerTabbedPageViewModel : ViewModelBase
{
    public Command AttendanceRecordCommand { get; }
    public QRCodeScannerTabbedPageViewModel()
    {
        AttendanceRecordCommand = new Command(AttendanceRecord);   
    }

    private async void AttendanceRecord()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PushModalAsync(new QRCodeScannerPage());
    }
}
