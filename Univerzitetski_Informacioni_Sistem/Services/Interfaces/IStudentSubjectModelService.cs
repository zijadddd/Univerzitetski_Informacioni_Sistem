using Univerzitetski_Informacioni_Sistem.Models;

namespace Univerzitetski_Informacioni_Sistem.Services.Interfaces;

public interface IStudentSubjectModelService
{
    Task<StudentSubjectModel> GetStudentPersonalisedDataAboutSubject(string subjectId);
    Task<List<StudentSubjectModel>> GetAllStudentsFromSubject(string subjectId, string completedActivePending);
    Task<List<StudentSubjectModel>> GetSubjectsOfStudent();
    Task<string> GetNumberOfStudents(string subjectId, string completedActivePending);
}
