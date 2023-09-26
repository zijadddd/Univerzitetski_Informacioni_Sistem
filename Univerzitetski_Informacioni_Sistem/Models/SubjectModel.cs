namespace Univerzitetski_Informacioni_Sistem.Models;
public record SubjectModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FacultyDepartment { get; set; }
    public string Professor { get; set; }
    public string Assistant { get; set; }
    public string LecturesDay { get; set; }
    public string LecturesTime { get; set; }
    public string ClassroomNumberForLectures { get; set; }
    public string ExercisesDay { get; set; }
    public string ExercisesTime { get; set; }
    public string ClassroomNumberForExercises { get; set; }
    public string ColorOne { get; set; }
    public string ColorTwo { get; set; }
    public string Picture { get; set; }
}
