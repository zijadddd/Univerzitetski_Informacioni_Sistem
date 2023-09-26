using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using Univerzitetski_Informacioni_Sistem.Data;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.Services.Implementations;

public class StudentSubjectModelService : IStudentSubjectModelService
{
    private readonly FirebaseClient firebaseDatabaseClient = new FirebaseClient(FirebaseSecretData.FirebaseDatabaseUrl, new FirebaseOptions
    {
        AuthTokenAsyncFactory = async () => await SecureStorage.GetAsync("authToken")
    });

    public async Task<StudentSubjectModel> GetStudentPersonalisedDataAboutSubject(string subjectId)
    {
        string uid = await SecureStorage.GetAsync("uid");
        StudentSubjectModel SS = new StudentSubjectModel();
        var response = await firebaseDatabaseClient.Child(nameof(StudentSubjectModel)).OrderBy("StudentId").EqualTo(uid).OnceSingleAsync<JObject>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            StudentSubjectModel studentSubject = jTokenData.ToObject<StudentSubjectModel>();
            if (studentSubject != null) if (studentSubject.SubjectId.Equals(subjectId)) return SS = studentSubject;
        }
        return SS;
    }

    public async Task<List<StudentSubjectModel>> GetAllStudentsFromSubject(string subjectId, string completedActiveCompleted)
    {
        var response = await firebaseDatabaseClient.Child(nameof(StudentSubjectModel)).OrderBy("SubjectId").EqualTo(subjectId).OnceSingleAsync<JObject>();
        List<StudentSubjectModel> studentSubjectList = new List<StudentSubjectModel>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            StudentSubjectModel studentSubject = jTokenData.ToObject<StudentSubjectModel>();
            if (studentSubject.CompletedActivePending.Equals(completedActiveCompleted))
                studentSubjectList.Add(studentSubject);
        }
        return studentSubjectList;
    }

    public async Task<List<StudentSubjectModel>> GetSubjectsOfStudent()
    {
        string uid = await SecureStorage.GetAsync("uid");
        var response = await firebaseDatabaseClient.Child(nameof(StudentSubjectModel)).OrderBy("StudentId").EqualTo(uid).OnceSingleAsync<JObject>();
        List<StudentSubjectModel> subjectModelList = new List<StudentSubjectModel>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            subjectModelList.Add(jTokenData.ToObject<StudentSubjectModel>());
        }
        return subjectModelList;
    }

    public async Task<string> GetNumberOfStudents(string subjectId, string completedActivePending)
    {
        int numberOfStudents = 0;
        var response = await firebaseDatabaseClient.Child(nameof(StudentSubjectModel)).OrderBy("SubjectId").EqualTo(subjectId).OnceAsync<StudentSubjectModel>();
        foreach (var data in response)
            if (data.Object.CompletedActivePending.Equals(completedActivePending))
                numberOfStudents++;
        return numberOfStudents.ToString();
    }
}
