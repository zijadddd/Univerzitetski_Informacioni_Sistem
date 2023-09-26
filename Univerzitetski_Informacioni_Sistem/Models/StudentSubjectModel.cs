namespace Univerzitetski_Informacioni_Sistem.Models;
public record StudentSubjectModel
{
    public string StudentId { get; set; }
    public string SubjectId { get; set; }
    public string CompletedActivePending { get; set; }
    public string Presence { get; set; }
    public string SeminarWork { get; set; }
    public string Homework { get; set; }
    public string FirstPartialExam { get; set; }
    public string SecondPartialExam { get; set; }
    public string FinalExam { get; set; }
}
