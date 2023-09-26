using CommunityToolkit.Maui;
using ZXing.Net.Maui;

namespace Univerzitetski_Informacioni_Sistem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Montserrat-Light.ttf", "MontserratLight");
                    fonts.AddFont("Montserrat-Medium.ttf", "MontserratMedium");
                    fonts.AddFont("MaterialIcons.ttf", "MaterialIcons");
                })
                .UseBarcodeReader()

            #region
                .ConfigureMauiHandlers(h =>
                 {
                     h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraBarcodeReaderView), typeof(CameraBarcodeReaderViewHandler));
                     h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraView), typeof(CameraViewHandler));
                     h.AddHandler(typeof(ZXing.Net.Maui.Controls.BarcodeGeneratorView), typeof(BarcodeGeneratorViewHandler));
                 });
            #endregion



            return builder.Build();
        }
    }
}