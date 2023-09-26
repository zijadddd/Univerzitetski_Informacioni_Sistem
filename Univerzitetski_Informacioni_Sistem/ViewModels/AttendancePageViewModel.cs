using System.Collections.ObjectModel;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class AttendancePageViewModel : ViewModelBase
{
    private readonly IStudentAttendanceModelService _studentAttendanceModelService;
    public ObservableCollection<StudentAttendanceUIModel> StudentAttendance { get; set; } = new ObservableCollection<StudentAttendanceUIModel>();
    public Command GoBackCommand { get; }

    private bool isRunning { get; set; }
    public bool IsRunning
    {
        get => isRunning;
        set
        {
            isRunning = value;
            OnPropertyChanged(nameof(IsRunning));
        }
    }

    private bool isVisible { get; set; }
    public bool IsVisible
    {
        get => isVisible;
        set
        {
            isVisible = value;
            OnPropertyChanged(nameof(IsVisible));
        }
    }
    private double activityIndicatorHeightRequest { get; set; }
    public double ActivityIndicatorHeightRequest
    {
        get => activityIndicatorHeightRequest;
        set
        {
            activityIndicatorHeightRequest = value;
            OnPropertyChanged(nameof(ActivityIndicatorHeightRequest));
        }
    }

    private double activityIndicatorWidthRequest { get; set; }
    public double ActivityIndicatorWidthRequest
    {
        get => activityIndicatorWidthRequest;
        set
        {
            activityIndicatorWidthRequest = value;
            OnPropertyChanged(nameof(ActivityIndicatorWidthRequest));
        }
    }

    private double margin { get; set; }
    public double Margin
    {
        get => margin;
        set
        {
            margin = value;
            OnPropertyChanged(nameof(Margin));
        }
    }

    private Color separator { get; set; } = Color.FromHex("#FFFFFF");
    public Color Separator
    {
        get => separator;
        set
        {
            separator = value;
            OnPropertyChanged(nameof(Separator));
        }
    }

    private string attendanceListEmptyMessage { get; set; }
    public string AttendanceListEmptyMessage
    {
        get => attendanceListEmptyMessage;
        set
        {
            attendanceListEmptyMessage = value;
            OnPropertyChanged(nameof(AttendanceListEmptyMessage));
        }
    }

    private bool attendanceListEmptyMessageIsVisible { get; set; } = false;
    public bool AttendanceListEmptyMessageIsVisible
    {
        get => attendanceListEmptyMessageIsVisible;
        set
        {
            attendanceListEmptyMessageIsVisible = value;
            OnPropertyChanged(nameof(AttendanceListEmptyMessageIsVisible));
        }
    }

    private bool attendanceListIsVisible { get; set; } = true;
    public bool AttendanceListIsVisible
    {
        get => attendanceListIsVisible;
        set
        {
            attendanceListIsVisible = value;
            OnPropertyChanged(nameof(AttendanceListIsVisible));
        }
    }

    public AttendancePageViewModel(string subjectId)
    {
        _studentAttendanceModelService = new StudentAttendanceModelService();
        GoBackCommand = new Command(GoBack);
        _ = SetPage(subjectId);
    }


    private async Task SetPage(string subjectId) {
        IsRunning = true;
        IsVisible = true;
        ActivityIndicatorHeightRequest = 50;
        ActivityIndicatorWidthRequest = 50;
        Margin = 10;

        var response = await _studentAttendanceModelService.GetStudentAttendance(subjectId);
        response.Sort((a, b) => int.Parse(a.Week).CompareTo(int.Parse(b.Week)));

        foreach (var data in response)
        {
            StudentAttendance.Add(new StudentAttendanceUIModel
            {
                LecturesDate = $"Predavanja ({data.LecturesDate}): " + (data.AttendanceOnLectures.Equals("1") ? "+" : (data.AttendanceOnLectures.Equals("0") ? "-" : "")),
                ExercisesDate = $"Vježbe ({data.ExercisesDate}): " + (data.AttendanceOnExercises.Equals("1") ? "+" : (data.AttendanceOnExercises.Equals("0") ? "-" : "")),
                Separator = Color.FromHex("#2a2a3e")
            });
        }

        if (StudentAttendance.Count == 0)
        {
            AttendanceListEmptyMessage = "Niti jedno prisustvo nije evidentirano.";
            AttendanceListEmptyMessageIsVisible = true;
            AttendanceListIsVisible = false;
        }

        IsRunning = false;
        IsVisible = false;
        ActivityIndicatorHeightRequest = 0;
        ActivityIndicatorWidthRequest = 0;
        Margin = 0;
    }

    private async void GoBack()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PopModalAsync();
    }
}
