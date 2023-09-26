using Univerzitetski_Informacioni_Sistem.Models;

namespace Univerzitetski_Informacioni_Sistem.Services.Interfaces;

public interface ISubjectModelService
{
    Task<List<SubjectModel>> GetAllSubjects(string faculty, string department);
    Task<SubjectModel> GetSubject(string Id);
}
