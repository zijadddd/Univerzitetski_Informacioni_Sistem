using Univerzitetski_Informacioni_Sistem.Models;

namespace Univerzitetski_Informacioni_Sistem.Services.Interfaces;

public interface IStudentAttendanceModelService
{
    Task<List<StudentAttendanceModel>> GetStudentAttendance(string subjectId);
    Task<string> GetSingleStudentAttendanceKey(string subjectId, string week);
    Task<StudentAttendanceModel> GetSingleStudentAttendance(string subjectId, string week);
    Task SetStudentAttendance(StudentAttendanceModel studentAttendanceModel);
    Task UpdateStudentAttendance(StudentAttendanceModel studentAttendanceModel);
}
