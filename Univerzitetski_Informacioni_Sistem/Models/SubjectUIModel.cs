namespace Univerzitetski_Informacioni_Sistem.Models;
public record SubjectUIModel
{
    public string Id { get; set; }
    public string Subject { get; set; }
    public string Professor { get; set; }
    public string Assistant { get; set; }
    public string LecturesDay { get; set; }
    public string LecturesTime { get; set; }
    public string ClassroomNumberForLectures { get; set; }
    public string ExerciseDay { get; set; }
    public string ExerciseTime { get; set; }
    public string ClassroomNumberForExercise { get; set; }
    public Color ColorOne { get; set; }
    public Color ColorTwo { get; set; }
    public string Picture { get; set; }
    public string NumberOfStudents { get; set; }
    public string StudentsText { get; set; }

    public SubjectUIModel(string id, string subject, string professor, string assistant, string lecturesDay, string lecturesTime, string classroomNumberForLectures, string exerciseDay, string exerciseTime, string classroomNumberForExercise, string colorOne, string colorTwo, string picture, string numberOfStudents)
    {
        Id = id;
        Subject = subject;
        Professor = professor;
        Assistant = assistant;
        LecturesDay = lecturesDay;
        LecturesTime = lecturesTime;
        ClassroomNumberForLectures = classroomNumberForLectures;
        ExerciseDay = exerciseDay;
        ExerciseTime = exerciseTime;
        ClassroomNumberForExercise = classroomNumberForExercise;
        ColorOne = Color.FromHex(colorOne);
        ColorTwo = Color.FromHex(colorTwo);
        Picture = picture;
        NumberOfStudents = numberOfStudents;
        if (int.Parse(numberOfStudents) % 10 == 0) StudentsText = "Studenata";
        else if (int.Parse(numberOfStudents) % 10 == 1) StudentsText = "Student";
        else if (int.Parse(numberOfStudents) % 10 >= 2 && int.Parse(numberOfStudents) <= 4) StudentsText = "Studenta";
        else StudentsText = "Studenata";
    }
}
