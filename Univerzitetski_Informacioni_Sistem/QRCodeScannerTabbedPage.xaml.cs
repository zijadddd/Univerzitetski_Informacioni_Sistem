using Univerzitetski_Informacioni_Sistem.ViewModels;

namespace Univerzitetski_Informacioni_Sistem;

public partial class QRCodeScannerTabbedPage : ContentPage
{
    public QRCodeScannerTabbedPage()
    {
        InitializeComponent();
        BindingContext = new QRCodeScannerTabbedPageViewModel();
    }
}