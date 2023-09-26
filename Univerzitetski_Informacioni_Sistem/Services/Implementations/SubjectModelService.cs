using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using Univerzitetski_Informacioni_Sistem.Data;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.Services.Implementations;
public class SubjectModelService : ISubjectModelService
{
    private readonly FirebaseClient firebaseDatabaseClient = new FirebaseClient(FirebaseSecretData.FirebaseDatabaseUrl, new FirebaseOptions
    {
        AuthTokenAsyncFactory = async () => await SecureStorage.GetAsync("authToken")
    });

    public async Task<List<SubjectModel>> GetAllSubjects(string faculty, string department)
    {
        string key = $"{faculty}_{department}";
        var response = await firebaseDatabaseClient.Child(nameof(SubjectModel)).OrderBy("FacultyDepartment").EqualTo(key).OnceSingleAsync<JObject>();
        List<SubjectModel> subjectModelList = new List<SubjectModel>();
        foreach (var data in response)
        {
            JToken jTokenData = data.Value;
            subjectModelList.Add(jTokenData.ToObject<SubjectModel>());
        }
        return subjectModelList;
    }

    public async Task<SubjectModel> GetSubject(string Id)
    {
        var response = await firebaseDatabaseClient.Child(nameof(SubjectModel)).OrderBy("Id").EqualTo(Id).OnceSingleAsync<JObject>();
        JToken jTokenResponse = response.First.First;
        return jTokenResponse.ToObject<SubjectModel>();
    }
}
