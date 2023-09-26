using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Implementations;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.ViewModels;

public class SubjectPageViewModel : ViewModelBase
{
    private readonly ISubjectModelService _subjectModelService;
    private readonly IStudentSubjectModelService _studentSubjectModelService;
    private readonly IUserModelService _userModelService;
    private readonly IStudentAttendanceModelService _studentAttendanceModelService;

    private SubjectModel SubjectModel;
    public Command GoBackCommand { get; }
    public Command OpenAttendancePageCommand { get; }
    private string SubjectId { get; set; }
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

    private string subjectName { get; set; }
    public string SubjectName
    {
        get => subjectName;
        set
        {
            subjectName = value;
            OnPropertyChanged(nameof(SubjectName));
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

    private string lecturesDay { get; set; }
    public string LecturesDay
    {
        get => lecturesDay;
        set
        {
            if (value.Equals("Ponedjeljak") ||
                value.Equals("Utorak") ||
                value.Equals("Četvrtak") ||
                value.Equals("Petak"))
                lecturesDay = $"Predavanja: Svaki {value}";
            else lecturesDay = $"Predavanja: Svaka {value}";
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

    private string lecturesLocation { get; set; }
    public string LecturesLocation
    {
        get => lecturesLocation;
        set
        {
            lecturesLocation = $"Učionica br. {value}";
            OnPropertyChanged(nameof(LecturesLocation));
        }
    }

    private string exercisesDay { get; set; }
    public string ExercisesDay
    {
        get => exercisesDay;
        set
        {
            if (value.Equals("Ponedjeljak") ||
                value.Equals("Utorak") ||
                value.Equals("Četvrtak") ||
                value.Equals("Petak"))
                exercisesDay = $"Vježbe: Svaki {value}";
            else exercisesDay = $"Vježbe: Svaka {value}";
            OnPropertyChanged(nameof(ExercisesDay));
        }
    }

    private string exercisesTime { get; set; }
    public string ExercisesTime
    {
        get => exercisesTime;
        set
        {
            exercisesTime = value;
            OnPropertyChanged(nameof(ExercisesTime));
        }
    }
    private string exercisesLocation { get; set; }
    public string ExercisesLocation
    {
        get => exercisesLocation;
        set
        {
            exercisesLocation = $"Učionica br. {value}";
            OnPropertyChanged(nameof(ExercisesLocation));
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

    private string presence { get; set; }
    public string Presence
    {
        get => presence;
        set
        {
            presence = $"Prisustvo: {value}b";
            OnPropertyChanged(nameof(Presence));
        }
    }

    private string seminarWork { get; set; }
    public string SeminarWork
    {
        get => seminarWork;
        set
        {
            seminarWork = $"Seminarski rad: {value}b";
            OnPropertyChanged(nameof(SeminarWork));
        }
    }

    private string homework { get; set; }
    public string Homework
    {
        get => homework;
        set
        {
            homework = $"Zadaće: {value}b";
            OnPropertyChanged(nameof(Homework));
        }
    }

    private string firstPartialExam { get; set; }
    public string FirstPartialExam
    {
        get => firstPartialExam;
        set
        {
            firstPartialExam = $"Prvi parcijalni ispit: {value}b";
            OnPropertyChanged(nameof(FirstPartialExam));
        }
    }

    private string secondPartialExam { get; set; }
    public string SecondPartialExam
    {
        get => secondPartialExam;
        set
        {
            secondPartialExam = $"Drugi parcijalni ispit: {value}b";
            OnPropertyChanged(nameof(SecondPartialExam));
        }
    }

    private string finalExam { get; set; }
    public string FinalExam
    {
        get => finalExam;
        set
        {
            finalExam = value;
            OnPropertyChanged(nameof(FinalExam));
        }
    }

    private string total { get; set; }
    public string Total
    {
        get => total;
        set
        {
            total = value;
            OnPropertyChanged(nameof(Total));
        }
    }

    private string grade { get; set; }
    public string Grade
    {
        get => grade;
        set
        {
            grade = value;
            OnPropertyChanged(nameof(Grade));
        }
    }

    private string professorName { get; set; }
    public string ProfessorName
    {
        get => professorName;
        set
        {
            professorName = value;
            OnPropertyChanged(nameof(ProfessorName));
        }
    }

    private string assistantName { get; set; }
    public string AssistantName
    {
        get => assistantName;
        set
        {
            assistantName = value;
            OnPropertyChanged(nameof(AssistantName));
        }
    }

    private string student1 { get; set; }
    public string Student1
    {
        get => student1;
        set
        {
            student1 = value;
            OnPropertyChanged(nameof(Student1));
        }
    }

    private string student2 { get; set; }
    public string Student2
    {
        get => student2;
        set
        {
            student2 = value;
            OnPropertyChanged(nameof(Student2));
        }
    }

    private string student3 { get; set; }
    public string Student3
    {
        get => student3;
        set
        {
            student3 = value;
            OnPropertyChanged(nameof(Student3));
        }
    }

    private string student4 { get; set; }
    public string Student4
    {
        get => student4;
        set
        {
            student4 = value;
            OnPropertyChanged(nameof(Student4));
        }
    }

    private string student5 { get; set; }
    public string Student5
    {
        get => student5;
        set
        {
            student5 = value;
            OnPropertyChanged(nameof(Student5));
        }
    }

    private Color weekLectures1 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures1
    {
        get => weekLectures1;
        set
        {
            weekLectures1 = value;
            OnPropertyChanged(nameof(WeekLectures1));
        }
    }

    private Color weekLectures2 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures2
    {
        get => weekLectures2;
        set
        {
            weekLectures2 = value;
            OnPropertyChanged(nameof(WeekLectures2));
        }
    }

    private Color weekLectures3 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures3
    {
        get => weekLectures3;
        set
        {
            weekLectures3 = value;
            OnPropertyChanged(nameof(WeekLectures3));
        }
    }

    private Color weekLectures4 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures4
    {
        get => weekLectures4;
        set
        {
            weekLectures4 = value;
            OnPropertyChanged(nameof(WeekLectures4));
        }
    }

    private Color weekLectures5 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures5
    {
        get => weekLectures5;
        set
        {
            weekLectures5 = value;
            OnPropertyChanged(nameof(WeekLectures5));
        }
    }

    private Color weekLectures6 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures6
    {
        get => weekLectures6;
        set
        {
            weekLectures6 = value;
            OnPropertyChanged(nameof(WeekLectures6));
        }
    }

    private Color weekLectures7 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures7
    {
        get => weekLectures7;
        set
        {
            weekLectures7 = value;
            OnPropertyChanged(nameof(WeekLectures7));
        }
    }

    private Color weekLectures8 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures8
    {
        get => weekLectures8;
        set
        {
            weekLectures8 = value;
            OnPropertyChanged(nameof(WeekLectures8));
        }
    }

    private Color weekLectures9 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures9
    {
        get => weekLectures9;
        set
        {
            weekLectures9 = value;
            OnPropertyChanged(nameof(WeekLectures9));
        }
    }

    private Color weekLectures10 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures10
    {
        get => weekLectures10;
        set
        {
            weekLectures10 = value;
            OnPropertyChanged(nameof(WeekLectures10));
        }
    }

    private Color weekLectures11 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures11
    {
        get => weekLectures11;
        set
        {
            weekLectures11 = value;
            OnPropertyChanged(nameof(WeekLectures11));
        }
    }

    private Color weekLectures12 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures12
    {
        get => weekLectures12;
        set
        {
            weekLectures12 = value;
            OnPropertyChanged(nameof(WeekLectures12));
        }
    }

    private Color weekLectures13 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures13
    {
        get => weekLectures13;
        set
        {
            weekLectures13 = value;
            OnPropertyChanged(nameof(WeekLectures13));
        }
    }

    private Color weekLectures14 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures14
    {
        get => weekLectures14;
        set
        {
            weekLectures14 = value;
            OnPropertyChanged(nameof(WeekLectures14));
        }
    }

    private Color weekLectures15 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures15
    {
        get => weekLectures15;
        set
        {
            weekLectures15 = value;
            OnPropertyChanged(nameof(WeekLectures15));
        }
    }

    private Color weekLectures16 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekLectures16
    {
        get => weekLectures15;
        set
        {
            weekLectures16 = value;
            OnPropertyChanged(nameof(WeekLectures16));
        }
    }

    private Color weekExercises1 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises1
    {
        get => weekExercises1;
        set
        {
            weekExercises1 = value;
            OnPropertyChanged(nameof(WeekExercises1));
        }
    }

    private Color weekExercises2 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises2
    {
        get => weekExercises2;
        set
        {
            weekExercises2 = value;
            OnPropertyChanged(nameof(WeekExercises2));
        }
    }

    private Color weekExercises3 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises3
    {
        get => weekExercises3;
        set
        {
            weekExercises3 = value;
            OnPropertyChanged(nameof(WeekExercises3));
        }
    }

    private Color weekExercises4 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises4
    {
        get => weekExercises4;
        set
        {
            weekExercises4 = value;
            OnPropertyChanged(nameof(WeekExercises4));
        }
    }

    private Color weekExercises5 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises5
    {
        get => weekExercises5;
        set
        {
            weekExercises5 = value;
            OnPropertyChanged(nameof(WeekExercises5));
        }
    }

    private Color weekExercises6 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises6
    {
        get => weekExercises6;
        set
        {
            weekExercises6 = value;
            OnPropertyChanged(nameof(WeekExercises6));
        }
    }

    private Color weekExercises7 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises7
    {
        get => weekExercises7;
        set
        {
            weekExercises7 = value;
            OnPropertyChanged(nameof(WeekExercises7));
        }
    }

    private Color weekExercises8 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises8
    {
        get => weekExercises8;
        set
        {
            weekExercises8 = value;
            OnPropertyChanged(nameof(WeekExercises8));
        }
    }

    private Color weekExercises9 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises9
    {
        get => weekExercises9;
        set
        {
            weekExercises9 = value;
            OnPropertyChanged(nameof(WeekExercises9));
        }
    }

    private Color weekExercises10 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises10
    {
        get => weekExercises10;
        set
        {
            weekExercises10 = value;
            OnPropertyChanged(nameof(WeekExercises10));
        }
    }

    private Color weekExercises11 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises11
    {
        get => weekExercises11;
        set
        {
            weekExercises11 = value;
            OnPropertyChanged(nameof(WeekExercises11));
        }
    }

    private Color weekExercises12 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises12
    {
        get => weekExercises12;
        set
        {
            weekExercises12 = value;
            OnPropertyChanged(nameof(WeekExercises12));
        }
    }

    private Color weekExercises13 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises13
    {
        get => weekExercises13;
        set
        {
            weekExercises13 = value;
            OnPropertyChanged(nameof(WeekExercises13));
        }
    }

    private Color weekExercises14 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises14
    {
        get => weekExercises14;
        set
        {
            weekExercises14 = value;
            OnPropertyChanged(nameof(WeekExercises14));
        }
    }

    private Color weekExercises15 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises15
    {
        get => weekExercises15;
        set
        {
            weekExercises15 = value;
            OnPropertyChanged(nameof(WeekExercises15));
        }
    }

    private Color weekExercises16 { get; set; } = Color.FromHex("#f4f5fc");
    public Color WeekExercises16
    {
        get => weekExercises16;
        set
        {
            weekExercises16 = value;
            OnPropertyChanged(nameof(WeekExercises16));
        }
    }

    public SubjectPageViewModel(string subjectId)
    {
        _subjectModelService = new SubjectModelService();
        _studentSubjectModelService = new StudentSubjectModelService();
        _userModelService = new UserModelService();
        _studentAttendanceModelService = new StudentAttendanceModelService();
        SubjectModel = new SubjectModel();
        SubjectId = subjectId;
        GoBackCommand = new Command(GoBack);
        OpenAttendancePageCommand = new Command(OpenAttendancePage);
        _ = SetPage(subjectId);
    }

    private async Task SetPage(string subjectId)
    {
        IsRunning = true;
        IsVisible = true;
        ActivityIndicatorHeightRequest = 50;
        ActivityIndicatorWidthRequest = 50;
        Margin = 10;

        SubjectModel = await _subjectModelService.GetSubject(subjectId);
        SubjectName = SubjectModel.Name;
        ProfessorName = SubjectModel.Professor;
        AssistantName = SubjectModel.Assistant;
        LecturesDay = SubjectModel.LecturesDay;
        LecturesTime = SubjectModel.LecturesTime;
        LecturesLocation = SubjectModel.ClassroomNumberForLectures;
        ExercisesDay = SubjectModel.ExercisesDay;
        ExercisesTime = SubjectModel.ExercisesTime;
        ExercisesLocation = SubjectModel.ClassroomNumberForExercises;
        StudentSubjectModel studentSubjectResponse = await _studentSubjectModelService.GetStudentPersonalisedDataAboutSubject(subjectId);
        NumberOfStudents = await _studentSubjectModelService.GetNumberOfStudents(subjectId, studentSubjectResponse.CompletedActivePending);
        Presence = studentSubjectResponse.Presence;
        SeminarWork = studentSubjectResponse.SeminarWork;
        Homework = studentSubjectResponse.Homework;
        FirstPartialExam = studentSubjectResponse.FirstPartialExam;
        SecondPartialExam = studentSubjectResponse.SecondPartialExam;
        if (int.Parse(studentSubjectResponse.FirstPartialExam) <= 0 || int.Parse(studentSubjectResponse.SecondPartialExam) <= 0) FinalExam = $"Završni ispit: 0b";
        else FinalExam = $"Završni ispit: {(int.Parse(studentSubjectResponse.FirstPartialExam) + int.Parse(studentSubjectResponse.SecondPartialExam))}b";
        if (studentSubjectResponse.CompletedActivePending.Equals("0"))
        {
            int totalPoints = int.Parse(studentSubjectResponse.Presence) + int.Parse(studentSubjectResponse.SeminarWork) + int.Parse(studentSubjectResponse.Homework) +
                        int.Parse(studentSubjectResponse.FinalExam);
            Total = $"Ukupno: {totalPoints}b";
            if (totalPoints < 55) Grade = "Ocjena: 5";
            else if (totalPoints < 65) Grade = "Ocjena: 6";
            else if (totalPoints < 75) Grade = "Ocjena: 7";
            else if (totalPoints < 85) Grade = "Ocjena: 8";
            else if (totalPoints < 95) Grade = "Ocjena: 9";
            else Grade = "Ocjena: 10";
        } else
        {
            int totalPoints = int.Parse(studentSubjectResponse.Presence) + int.Parse(studentSubjectResponse.SeminarWork) + int.Parse(studentSubjectResponse.Homework) +
                        int.Parse(studentSubjectResponse.FirstPartialExam) + int.Parse(studentSubjectResponse.SecondPartialExam);
            Total = $"Ukupno: {totalPoints}b";
            Grade = "Ocjena: 5";
        }

        var studentSubjectResult = await _studentSubjectModelService.GetAllStudentsFromSubject(subjectId, studentSubjectResponse.CompletedActivePending);
        var random = new Random();
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < studentSubjectResult.Count) numbers.Add(random.Next(0, studentSubjectResult.Count));
        List<int> randomNumbers = numbers.ToList<int>();
        for (int i = 0; i < studentSubjectResult.Count; i++) 
        {
            var student = await _userModelService.GetUser(studentSubjectResult[randomNumbers[i]].StudentId);
            if (student != null) {
                if (i == 0)
                {
                    if (string.IsNullOrEmpty(student.ProfilePhoto))
                        Student1 = "userprofilephotobase.png";
                    else Student1 = student.ProfilePhoto;
                }
                if (i == 1)
                {
                    if (string.IsNullOrEmpty(student.ProfilePhoto))
                        Student2 = "userprofilephotobase.png";
                    else Student2 = student.ProfilePhoto;
                }
                if (i == 2)
                {
                    if (string.IsNullOrEmpty(student.ProfilePhoto))
                        Student3 = "userprofilephotobase.png";
                    else Student3 = student.ProfilePhoto;
                }
                if (i == 3)
                {
                    if (string.IsNullOrEmpty(student.ProfilePhoto))
                        Student4 = "userprofilephotobase.png";
                    else Student4 = student.ProfilePhoto;
                }
                if (i == 4)
                {
                    if (string.IsNullOrEmpty(student.ProfilePhoto))
                        Student5 = "userprofilephotobase.png";
                    else Student5 = student.ProfilePhoto;
                    break;
                }
            }
        }

        var responseFromStudentAttendanceService = await _studentAttendanceModelService.GetStudentAttendance(subjectId);
        responseFromStudentAttendanceService.Sort((a, b) => int.Parse(a.Week).CompareTo(int.Parse(b.Week)));

        if (responseFromStudentAttendanceService.Count > 0) {
            if (responseFromStudentAttendanceService.ElementAtOrDefault(0) != null)
            {
                if (responseFromStudentAttendanceService[0].AttendanceOnLectures.Equals("1")) WeekLectures1 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[0].AttendanceOnExercises.Equals("1")) WeekExercises1 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(1) != null)
            {
                if (responseFromStudentAttendanceService[1].AttendanceOnLectures.Equals("1")) WeekLectures2 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[1].AttendanceOnExercises.Equals("1")) WeekExercises2 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(2) != null)
            {
                if (responseFromStudentAttendanceService[2].AttendanceOnLectures.Equals("1")) WeekLectures3 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[2].AttendanceOnExercises.Equals("1")) WeekExercises3 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(3) != null)
            {
                if (responseFromStudentAttendanceService[3].AttendanceOnLectures.Equals("1")) WeekLectures4 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[3].AttendanceOnExercises.Equals("1")) WeekExercises4 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(4) != null)
            {
                if (responseFromStudentAttendanceService[4].AttendanceOnLectures.Equals("1")) WeekLectures5 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[4].AttendanceOnExercises.Equals("1")) WeekExercises5 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(5) != null)
            {
                if (responseFromStudentAttendanceService[5].AttendanceOnLectures.Equals("1")) WeekLectures6 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[5].AttendanceOnExercises.Equals("1")) WeekExercises6 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(6) != null)
            {
                if (responseFromStudentAttendanceService[6].AttendanceOnLectures.Equals("1")) WeekLectures7 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[6].AttendanceOnExercises.Equals("1")) WeekExercises7 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(7) != null)
            {
                if (responseFromStudentAttendanceService[7].AttendanceOnLectures.Equals("1")) WeekLectures8 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[7].AttendanceOnExercises.Equals("1")) WeekExercises8 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(8) != null)
            {
                if (responseFromStudentAttendanceService[8].AttendanceOnLectures.Equals("1")) WeekLectures9 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[8].AttendanceOnExercises.Equals("1")) WeekExercises9 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(9) != null)
            {
                if (responseFromStudentAttendanceService[9].AttendanceOnLectures.Equals("1")) WeekLectures10 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[9].AttendanceOnExercises.Equals("1")) WeekExercises10 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(10) != null)
            {
                if (responseFromStudentAttendanceService[10].AttendanceOnLectures.Equals("1")) WeekLectures11 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[10].AttendanceOnExercises.Equals("1")) WeekExercises11 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(11) != null)
            {
                if (responseFromStudentAttendanceService[11].AttendanceOnLectures.Equals("1")) WeekLectures12 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[11].AttendanceOnExercises.Equals("1")) WeekExercises12 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(12) != null)
            {
                if (responseFromStudentAttendanceService[12].AttendanceOnLectures.Equals("1")) WeekLectures13 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[12].AttendanceOnExercises.Equals("1")) WeekExercises13 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(13) != null)
            {
                if (responseFromStudentAttendanceService[13].AttendanceOnLectures.Equals("1")) WeekLectures14 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[13].AttendanceOnExercises.Equals("1")) WeekExercises14 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(14) != null)
            {
                if (responseFromStudentAttendanceService[14].AttendanceOnLectures.Equals("1")) WeekLectures15 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[14].AttendanceOnExercises.Equals("1")) WeekExercises15 = Color.FromHex("#ff7272");
            }
            if (responseFromStudentAttendanceService.ElementAtOrDefault(15) != null)
            {
                if (responseFromStudentAttendanceService[15].AttendanceOnLectures.Equals("1")) WeekLectures16 = Color.FromHex("#0067ff");
                if (responseFromStudentAttendanceService[15].AttendanceOnExercises.Equals("1")) WeekExercises16 = Color.FromHex("#ff7272");
            }
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

    private async void OpenAttendancePage()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            await App.Current.MainPage.Navigation.PushModalAsync(new NoInternetConnectionPage());
        else await App.Current.MainPage.Navigation.PushModalAsync(new AttendancePage(SubjectId));
    }
}
