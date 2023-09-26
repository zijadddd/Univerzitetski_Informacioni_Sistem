namespace Univerzitetski_Informacioni_Sistem.Models;

public record QRCodeModel
{
    public string SubjectId { get; set; }
    public string Week { get; set; }
    public string LecturesDate { get; set; }
    public string ExercisesDate { get; set; }
}
