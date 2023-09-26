namespace Univerzitetski_Informacioni_Sistem.Models;

public class StudentAttendanceUIModel
{
    public string LecturesDate { get; set; }
    public string ExercisesDate { get; set; }
    public Color Separator { get; set; } = Color.FromHex("#FFFFFF");
}
