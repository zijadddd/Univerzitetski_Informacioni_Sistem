using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using Univerzitetski_Informacioni_Sistem.Data;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.Services.Implementations;

public class StudentAttendanceModelService : IStudentAttendanceModelService
{
    private readonly FirebaseClient firebaseDatabaseClient = new FirebaseClient(FirebaseSecretData.FirebaseDatabaseUrl, new FirebaseOptions
    {
        AuthTokenAsyncFactory = async () => await SecureStorage.GetAsync("authToken")
    });

    public async Task<StudentAttendanceModel> GetSingleStudentAttendance(string subjectId, string week)
    {
        string uid = await SecureStorage.GetAsync("uid");
        List<StudentAttendanceModel> studentAttendanceModelList = new List<StudentAttendanceModel>();
        var response = await firebaseDatabaseClient.Child(nameof(StudentAttendanceModel)).OrderBy("StudentId").EqualTo(uid).OnceSingleAsync<JObject>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            StudentAttendanceModel studentAttendanceModel = jTokenData.ToObject<StudentAttendanceModel>();
            studentAttendanceModelList.Add(studentAttendanceModel);
        }
        return studentAttendanceModelList.Where(attendance => attendance.Week.Equals(week) && attendance.SubjectId.Equals(subjectId)).FirstOrDefault();
    }

    public async Task<string> GetSingleStudentAttendanceKey(string subjectId, string week)
    {
        string uid = await SecureStorage.GetAsync("uid");
        var response = await firebaseDatabaseClient.Child(nameof(StudentAttendanceModel)).OrderBy("StudentId").EqualTo(uid).OnceSingleAsync<JObject>();
        string key = "";
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            StudentAttendanceModel studentAttendanceModel = jTokenData.ToObject<StudentAttendanceModel>();
            if (studentAttendanceModel.SubjectId.Equals(subjectId) && studentAttendanceModel.Week.Equals(week)) key = data.Key.ToString();
        }
        return key;
    }

    public async Task<List<StudentAttendanceModel>> GetStudentAttendance(string subjectId)
    {
        string uid = await SecureStorage.GetAsync("uid");
        List<StudentAttendanceModel> studentAttendanceModelList = new List<StudentAttendanceModel>();
        var response = await firebaseDatabaseClient.Child(nameof(StudentAttendanceModel)).OrderBy("StudentId").EqualTo(uid).OnceSingleAsync<JObject>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            StudentAttendanceModel studentAttendanceModel = jTokenData.ToObject<StudentAttendanceModel>();
            if (studentAttendanceModel != null) if (studentAttendanceModel.SubjectId.Equals(subjectId)) studentAttendanceModelList.Add(studentAttendanceModel);
        }
        return studentAttendanceModelList;
    }

    public async Task SetStudentAttendance(StudentAttendanceModel studentAttendanceModel)
    {
        await firebaseDatabaseClient.Child(nameof(StudentAttendanceModel)).PostAsync(studentAttendanceModel);
    }

    public async Task UpdateStudentAttendance(StudentAttendanceModel studentAttendanceModel)
    {
        var key = await GetSingleStudentAttendanceKey(studentAttendanceModel.SubjectId, studentAttendanceModel.Week);
        await firebaseDatabaseClient.Child(nameof(StudentAttendanceModel)).Child(key).PutAsync(studentAttendanceModel);
    }
}
