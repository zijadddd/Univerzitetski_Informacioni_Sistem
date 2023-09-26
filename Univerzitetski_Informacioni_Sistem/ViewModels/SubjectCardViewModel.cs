namespace Univerzitetski_Informacioni_Sistem.ViewModels;

public class SubjectCardViewModel : ViewModelBase
{
    private string id { get; set; }
    public string Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    private string name { get; set; }
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    private string professor { get; set; }
    public string Professor
    {
        get => professor;
        set
        {
            professor = value;
            OnPropertyChanged(nameof(Professor));
        }
    }
    private string assistant { get; set; }
    public string Assistant
    {
        get => assistant;
        set
        {
            assistant = value;
            OnPropertyChanged(nameof(Assistant));
        }
    }
    private string lecturesDay { get; set; }
    public string LecturesDay
    {
        get => lecturesDay;
        set
        {
            lecturesDay = value;
            OnPropertyChanged(nameof(LecturesDay));
        }
    }
    private string lecturesTime { get; set; }
    public string LecturesTime
    {
        get => lecturesTime;
        set
        {
            lecturesTime = value;
            OnPropertyChanged(nameof(LecturesTime));
        }
    }
    private string classroomNumberForLectures { get; set; }
    public string ClassroomNumberForLectures
    {
        get => classroomNumberForLectures;
        set
        {
            classroomNumberForLectures = value;
            OnPropertyChanged(nameof(ClassroomNumberForLectures));
        }
    }
    private string exerciseDay { get; set; }
    public string ExerciseDay
    {
        get => exerciseDay;
        set
        {
            exerciseDay = value;
            OnPropertyChanged(nameof(ExerciseDay));
        }
    }
    private string exerciseTime { get; set; }
    public string ExerciseTime
    {
        get => exerciseTime;
        set
        {
            exerciseTime = value;
            OnPropertyChanged(nameof(ExerciseTime));
        }
    }
    private string classroomNumberForExercise { get; set; }
    public string ClassroomNumberForExercise
    {
        get => classroomNumberForExercise;
        set
        {
            classroomNumberForExercise = value;
            OnPropertyChanged(nameof(ClassroomNumberForExercise));
        }
    }
    private Color colorOne { get; set; }
    public Color ColorOne
    {
        get => colorOne;
        set
        {
            colorOne = value;
            OnPropertyChanged(nameof(ColorOne));
        }
    }
    private Color colorTwo { get; set; }
    public Color ColorTwo
    {
        get => colorTwo;
        set
        {
            colorTwo = value;
            OnPropertyChanged(nameof(ColorTwo));
        }
    }
    private string picture { get; set; }
    public string Picture
    {
        get => picture;
        set
        {
            picture = value;
            OnPropertyChanged(nameof(Picture));
        }
    }
    private string numberOfStudents { get; set; }
    public string NumberOfStudents
    {
        get => numberOfStudents;
        set
        {
            numberOfStudents = value;
            OnPropertyChanged(nameof(NumberOfStudents));
        }
    }
    private string studentsText { get; set; }
    public string StudentsText
    {
        get => studentsText;
        set
        {
            studentsText = value;
            OnPropertyChanged(nameof(StudentsText));
        }
    }

    public Command OpenSubjectPageCommand { get; }

    public SubjectCardViewModel()
    {
        OpenSubjectPageCommand = new Command(OpenSubjectPage);
    }

    private async void OpenSubjectPage()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PushModalAsync(new SubjectPage(Id));
    }
}
