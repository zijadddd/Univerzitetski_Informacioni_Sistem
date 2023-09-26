using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Univerzitetski_Informacioni_Sistem.Helpers;

public class Methods
{
    public async static void ToastMethod(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        var toast = Toast.Make(message, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}
