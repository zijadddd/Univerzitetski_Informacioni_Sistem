using Newtonsoft.Json.Linq;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem;

public partial class QRCodeScannerPage : ContentPage
{
	private readonly IStudentAttendanceModelService _studentAttendanceModelService;
    private readonly IStudentSubjectModelService _studentSubjectModelService;

    public QRCodeScannerPage()
    {
        _studentAttendanceModelService = new StudentAttendanceModelService();
        _studentSubjectModelService = new StudentSubjectModelService();

        InitializeComponent();
        BindingContext = this;
    }

	private void QRCodeDetectedProcessing(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        Camera.IsDetecting = false;
        Dispatcher.Dispatch(async () =>
        {
            try
            {
                JObject jsonObject = JObject.Parse(e.Results[0].Value.ToString());

                var studentPersonalisedDataAboutSubject = await _studentSubjectModelService.GetStudentPersonalisedDataAboutSubject(jsonObject.GetValue("SubjectId").ToString());

                if (!studentPersonalisedDataAboutSubject.CompletedActivePending.Equals("1"))
                {
                    Methods.ToastMethod("Prisustvo nije evidentirano.");
                    return;
                }

                if (string.IsNullOrEmpty(jsonObject.GetValue("LecturesDate").ToString()) && string.IsNullOrEmpty(jsonObject.GetValue("ExercisesDate").ToString()))
                {
                    Methods.ToastMethod("QR kod nije validan.");
                    return;
                }

                QRCodeModel qrCodeModel = new QRCodeModel
                {
                    SubjectId = jsonObject.GetValue("SubjectId").ToString(),
                    Week = jsonObject.GetValue("Week").ToString(),
                    LecturesDate = jsonObject.GetValue("LecturesDate").ToString(),
                    ExercisesDate = jsonObject.GetValue("ExercisesDate").ToString()
                };
                if (int.Parse(qrCodeModel.Week) < 1 || int.Parse(qrCodeModel.Week) > 16)
                {
                    Methods.ToastMethod("QR kod nije validan.");
                    return;
                }

                if (!string.IsNullOrEmpty(qrCodeModel.LecturesDate) && !string.IsNullOrEmpty(qrCodeModel.ExercisesDate))
                {
                    Methods.ToastMethod("Ne možete evidentirati u isto vrijeme dva prisustva.");
                    return;
                }

                var AttendanceExist = await _studentAttendanceModelService.GetSingleStudentAttendance(qrCodeModel.SubjectId, qrCodeModel.Week);

                if (AttendanceExist is null)
                {
                    if (!string.IsNullOrEmpty(qrCodeModel.LecturesDate) && string.IsNullOrEmpty(qrCodeModel.ExercisesDate))
                    {
                        await _studentAttendanceModelService.SetStudentAttendance(new StudentAttendanceModel
                        {
                            StudentId = await SecureStorage.GetAsync("uid"),
                            SubjectId = qrCodeModel.SubjectId,
                            Week = qrCodeModel.Week,
                            LecturesDate = qrCodeModel.LecturesDate,
                            AttendanceOnLectures = "1",
                            ExercisesDate = "",
                            AttendanceOnExercises = "",
                        });
                        Methods.ToastMethod("Uspješno ste evidentirali prisustvo.");
                        return;
                    }
                    if (string.IsNullOrEmpty(qrCodeModel.LecturesDate) && !string.IsNullOrEmpty(qrCodeModel.ExercisesDate))
                    {
                        await _studentAttendanceModelService.SetStudentAttendance(new StudentAttendanceModel
                        {
                            StudentId = await SecureStorage.GetAsync("uid"),
                            SubjectId = qrCodeModel.SubjectId,
                            Week = qrCodeModel.Week,
                            LecturesDate = "",
                            AttendanceOnLectures = "",
                            ExercisesDate = qrCodeModel.ExercisesDate,
                            AttendanceOnExercises = "1",
                        });
                        Methods.ToastMethod("Uspješno ste evidentirali prisustvo.");
                        return;
                    }
                } else
                {
                    if (!string.IsNullOrEmpty(AttendanceExist.LecturesDate) && !string.IsNullOrEmpty(qrCodeModel.LecturesDate) && AttendanceExist.Week.Equals(qrCodeModel.Week))
                    {
                        Methods.ToastMethod("Već ste evidentirali prisustvo.");
                        return;
                    }
                    if (!string.IsNullOrEmpty(AttendanceExist.ExercisesDate) && !string.IsNullOrEmpty(qrCodeModel.ExercisesDate) && AttendanceExist.Week.Equals(qrCodeModel.Week))
                    {
                        Methods.ToastMethod("Već ste evidentirali prisustvo.");
                        return;
                    }
                    if (string.IsNullOrEmpty(AttendanceExist.LecturesDate) && !string.IsNullOrEmpty(AttendanceExist.ExercisesDate))
                    {
                        await _studentAttendanceModelService.UpdateStudentAttendance(new StudentAttendanceModel
                        {
                            StudentId = await SecureStorage.GetAsync("uid"),
                            SubjectId = qrCodeModel.SubjectId,
                            Week = qrCodeModel.Week,
                            LecturesDate = qrCodeModel.LecturesDate,
                            AttendanceOnLectures = "1",
                            ExercisesDate = AttendanceExist.ExercisesDate,
                            AttendanceOnExercises = AttendanceExist.AttendanceOnExercises,
                        });
                        Methods.ToastMethod("Uspješno ste evidentirali prisustvo.");
                        return;
                    }
                    if (!string.IsNullOrEmpty(AttendanceExist.LecturesDate) && string.IsNullOrEmpty(AttendanceExist.ExercisesDate))
                    {
                        await _studentAttendanceModelService.UpdateStudentAttendance(new StudentAttendanceModel
                        {
                            StudentId = await SecureStorage.GetAsync("uid"),
                            SubjectId = qrCodeModel.SubjectId,
                            Week = qrCodeModel.Week,
                            LecturesDate = AttendanceExist.LecturesDate,
                            AttendanceOnLectures = AttendanceExist.AttendanceOnLectures,
                            ExercisesDate = qrCodeModel.ExercisesDate,
                            AttendanceOnExercises = "1",
                        });
                        Methods.ToastMethod("Uspješno ste evidentirali prisustvo.");
                        return;
                    }
                }
            } catch (Exception ex)
            {
                Methods.ToastMethod("Nešto nije uredu sa QR kodom.");
            }
        });
    }

    private async void GoBackCommand(object sender, EventArgs e)
    {
        Camera = null;
        await App.Current.MainPage.Navigation.PopModalAsync();
    }
}