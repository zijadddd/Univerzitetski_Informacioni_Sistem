namespace Univerzitetski_Informacioni_Sistem.Models;

public record StudentAttendanceModel
{
    public string StudentId { get; set; }
    public string SubjectId { get; set; }
    public string Week { get; set; }
    public string LecturesDate { get; set; }
    public string AttendanceOnLectures { get; set; }
    public string ExercisesDate { get; set; }
    public string AttendanceOnExercises { get; set; }
}
