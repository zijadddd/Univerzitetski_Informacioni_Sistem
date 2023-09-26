using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;
public class SubjectsPageViewModel : ViewModelBase
{
    private readonly IUserModelService _userModelService;
    private readonly ISubjectModelService _subjectModelService;
    private readonly IStudentSubjectModelService _studentSubjectModelService;
    private UserModel UserModel;

    public ObservableCollection<SubjectCardViewModel> ActiveSubjects { get; set; } = new ObservableCollection<SubjectCardViewModel>();
    public ObservableCollection<SubjectCardViewModel> PendingSubjects { get; set; } = new ObservableCollection<SubjectCardViewModel>();
    public ObservableCollection<SubjectCardViewModel> CompletedSubjects { get; set; } = new ObservableCollection<SubjectCardViewModel>();

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

    public string NumberOfStudentsInSubject { get; set; }
    private string _completedSubjectsNumber { get; set; }
    private string _activeSubjectsNumber { get; set; }
    private string _pendingSubjectsNumber { get; set; }

    public string CompletedSubjectsNumber
    {
        get => _completedSubjectsNumber;
        set
        {
            _completedSubjectsNumber = value;
            OnPropertyChanged(nameof(CompletedSubjectsNumber));
        }
    }
    public string ActiveSubjectsNumber
    {
        get => _activeSubjectsNumber;
        set
        {
            _activeSubjectsNumber = value;
            OnPropertyChanged(nameof(ActiveSubjectsNumber));
        }
    }
    public string PendingSubjectsNumber
    {
        get => _pendingSubjectsNumber;
        set
        {
            _pendingSubjectsNumber = value;
            OnPropertyChanged(nameof(PendingSubjectsNumber));
        }
    }

    public string FirstName
    {
        get => UserModel.FirstName;
        set
        {
            UserModel.FirstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string ProfilePhoto
    {
        get => UserModel.ProfilePhoto;
        set
        {
            UserModel.ProfilePhoto = value;
            OnPropertyChanged(nameof(ProfilePhoto));
        }
    }

    public string Date
    {
        get
        {
            string[] months = { "Januar", "Februar", "Mart", "April", "Maj", "Juni", "Juli", "August", "Septembar", "Oktobar", "Novembar", "Decembar" };
            return DateTimeOffset.Now.Day + ". " + months[DateTimeOffset.Now.Month - 1];
        }
        set
        {
            string[] months = { "Januar", "Februar", "Mart", "April", "Maj", "Juni", "Juli", "August", "Septembar", "Oktobar", "Novembar", "Decembar" };
            Date = DateTimeOffset.Now.Day + ". " + months[DateTimeOffset.Now.Month - 1];
            OnPropertyChanged(nameof(Date));
        }
    }

    public SubjectsPageViewModel()
    {
        _userModelService = new UserModelService();
        _subjectModelService = new SubjectModelService();
        _studentSubjectModelService = new StudentSubjectModelService();
        _ = SetPage();

        UserModel = new UserModel();

        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            ProfilePhoto = m;
        });
    }

    private async Task SetPage()
    {
        IsRunning = true;
        IsVisible = true;
        ActivityIndicatorHeightRequest = 50;
        ActivityIndicatorWidthRequest = 50;
        Margin = 10;

        var user = await _userModelService.GetLoggedUserAsync();
        FirstName = user.FirstName;
        if (string.IsNullOrEmpty(user.ProfilePhoto)) ProfilePhoto = "userprofilephotobase.png";
        else ProfilePhoto = user.ProfilePhoto;

        var subjectsNumber = await _studentSubjectModelService.GetSubjectsOfStudent();
        int numberOfActiveSubjects = 0;
        int numberOfPendingSubjects = 0;
        int numberOfCompletedSubjects = 0;

        List<string> indexesOfActiveSubjects = new List<string>();
        List<string> indexesOfPendingSubjects = new List<string>();
        List<string> indexesOfCompletedSubjects = new List<string>();

        foreach (var item in subjectsNumber)
        {
            if (item != null)
            {
                if (item != null && item.CompletedActivePending.Equals("0"))
                {
                    numberOfCompletedSubjects++;
                    indexesOfCompletedSubjects.Add(item.SubjectId);
                } else if (item != null && item.CompletedActivePending.Equals("1"))
                {
                    numberOfActiveSubjects++;
                    indexesOfActiveSubjects.Add(item.SubjectId);
                } else
                {
                    numberOfPendingSubjects++;
                    indexesOfPendingSubjects.Add(item.SubjectId);
                }
            }
        }
        ActiveSubjectsNumber = numberOfActiveSubjects.ToString();
        PendingSubjectsNumber = numberOfPendingSubjects.ToString();
        CompletedSubjectsNumber = numberOfCompletedSubjects.ToString();

        List<SubjectModel> subjectModelList = await _subjectModelService.GetAllSubjects(user.Faculty, user.Department);

        foreach (var subject in subjectModelList)
        {
            var studentSubjectData = await _studentSubjectModelService.GetStudentPersonalisedDataAboutSubject(subject.Id);
            NumberOfStudentsInSubject = await _studentSubjectModelService.GetNumberOfStudents(subject.Id, studentSubjectData.CompletedActivePending);
            var subjectUIModel = new SubjectUIModel
                            (subject.Id, subject.Name, subject.Professor, subject.Assistant, subject.LecturesDay, subject.LecturesTime, subject.ClassroomNumberForLectures,
                            subject.ExercisesDay, subject.ExercisesTime, subject.ClassroomNumberForExercises, subject.ColorOne, subject.ColorTwo, subject.Picture, NumberOfStudentsInSubject);

            bool subjectUIModelAdded = false;

            for (int i = 0; i < indexesOfActiveSubjects.Count; i++)
                if (subjectUIModel.Id.Equals(indexesOfActiveSubjects[i]))
                {
                    ActiveSubjects.Add(new SubjectCardViewModel
                    {
                        Id = subjectUIModel.Id,
                        Name = subjectUIModel.Subject,
                        Professor = subjectUIModel.Professor,
                        Assistant = subjectUIModel.Assistant,
                        LecturesDay = subjectUIModel.LecturesDay,
                        LecturesTime = subjectUIModel.LecturesTime,
                        ClassroomNumberForLectures = subjectUIModel.ClassroomNumberForLectures,
                        ExerciseDay = subjectUIModel.ExerciseDay,
                        ExerciseTime = subjectUIModel.ExerciseTime,
                        ClassroomNumberForExercise = subjectUIModel.ClassroomNumberForExercise,
                        ColorOne = subjectUIModel.ColorOne,
                        ColorTwo = subjectUIModel.ColorTwo,
                        Picture = subjectUIModel.Picture,
                        NumberOfStudents = subjectUIModel.NumberOfStudents,
                        StudentsText = subjectUIModel.StudentsText
                    });
                    subjectUIModelAdded = true;
                    break;
                }
            if (subjectUIModelAdded) continue;
            for (int i = 0; i < indexesOfPendingSubjects.Count; i++)
                if (subjectUIModel.Id.Equals(indexesOfPendingSubjects[i]))
                {
                    PendingSubjects.Add(new SubjectCardViewModel
                    {
                        Id = subjectUIModel.Id,
                        Name = subjectUIModel.Subject,
                        Professor = subjectUIModel.Professor,
                        Assistant = subjectUIModel.Assistant,
                        LecturesDay = subjectUIModel.LecturesDay,
                        LecturesTime = subjectUIModel.LecturesTime,
                        ClassroomNumberForLectures = subjectUIModel.ClassroomNumberForLectures,
                        ExerciseDay = subjectUIModel.ExerciseDay,
                        ExerciseTime = subjectUIModel.ExerciseTime,
                        ClassroomNumberForExercise = subjectUIModel.ClassroomNumberForExercise,
                        ColorOne = subjectUIModel.ColorOne,
                        ColorTwo = subjectUIModel.ColorTwo,
                        Picture = subjectUIModel.Picture,
                        NumberOfStudents = subjectUIModel.NumberOfStudents,
                        StudentsText = subjectUIModel.StudentsText
                    });
                    subjectUIModelAdded = true;
                    break;
                }
            if (subjectUIModelAdded) continue;
            for (int i = 0; i < indexesOfCompletedSubjects.Count; i++)
                if (subjectUIModel.Id.Equals(indexesOfCompletedSubjects[i]))
                {
                    CompletedSubjects.Add(new SubjectCardViewModel
                    {
                        Id = subjectUIModel.Id,
                        Name = subjectUIModel.Subject,
                        Professor = subjectUIModel.Professor,
                        Assistant = subjectUIModel.Assistant,
                        LecturesDay = subjectUIModel.LecturesDay,
                        LecturesTime = subjectUIModel.LecturesTime,
                        ClassroomNumberForLectures = subjectUIModel.ClassroomNumberForLectures,
                        ExerciseDay = subjectUIModel.ExerciseDay,
                        ExerciseTime = subjectUIModel.ExerciseTime,
                        ClassroomNumberForExercise = subjectUIModel.ClassroomNumberForExercise,
                        ColorOne = subjectUIModel.ColorOne,
                        ColorTwo = subjectUIModel.ColorTwo,
                        Picture = subjectUIModel.Picture,
                        NumberOfStudents = subjectUIModel.NumberOfStudents,
                        StudentsText = subjectUIModel.StudentsText
                    });
                    break;
                }
        }
        IsRunning = false;
        IsVisible = false;
        ActivityIndicatorHeightRequest = 0;
        ActivityIndicatorWidthRequest = 0;
        Margin = 0;
    }
}
