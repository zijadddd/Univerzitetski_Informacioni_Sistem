using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Newtonsoft.Json.Linq;
using Univerzitetski_Informacioni_Sistem.Data;
using Univerzitetski_Informacioni_Sistem.Helpers;
using Univerzitetski_Informacioni_Sistem.Models;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.Services.Implementations;

public class UserModelService : IUserModelService
{
    private readonly FirebaseClient firebaseDatabaseClient = new FirebaseClient(FirebaseSecretData.FirebaseDatabaseUrl, new FirebaseOptions
    {
        AuthTokenAsyncFactory = async () => await SecureStorage.GetAsync("authToken")
    });

    private readonly FirebaseStorage firebaseStorageClient = new FirebaseStorage(FirebaseSecretData.FirebaseStorageUrl, new FirebaseStorageOptions
    {
        AuthTokenAsyncFactory = async () => await SecureStorage.GetAsync("authToken")
    });

    public async Task<UserModel> GetLoggedUserAsync()
    {
        string uid = await SecureStorage.GetAsync("uid");
        var response = await firebaseDatabaseClient.Child(nameof(UserModel)).OrderBy("UID").EqualTo(uid).OnceSingleAsync<JObject>();
        JToken jTokenResponse = response.First.First;
        return jTokenResponse.ToObject<UserModel>();
    }

    public async Task<UserModel> GetUser(string studentId)
    {
        var response = await firebaseDatabaseClient.Child(nameof(UserModel)).OrderBy("UID").EqualTo(studentId).OnceSingleAsync<JObject>();
        JToken jTokenResponse = response.First.First;
        return jTokenResponse.ToObject<UserModel>();
    }

    public async Task<string> ChangeUserPhoto()
    {
        try
        {
            var filePicker = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });

            if (filePicker is not null)
            {
                var fileToUpload = await filePicker.OpenReadAsync();
                var linkToFile = await firebaseStorageClient.Child(filePicker.FileName).PutAsync(fileToUpload);

                string uid = await SecureStorage.GetAsync("uid");
                var response = await firebaseDatabaseClient.Child(nameof(UserModel)).OrderBy("UID").EqualTo(uid).OnceSingleAsync<JObject>();

                string key = "";
                foreach (var data in response) key = data.Key.ToString();

                JToken jTokenResponse = response.First.First;
                UserModel userModel = jTokenResponse.ToObject<UserModel>();
                userModel.ProfilePhoto = linkToFile;

                await firebaseDatabaseClient.Child(nameof(UserModel)).Child(key).PutAsync(userModel);
                Methods.ToastMethod("Uspješno ste promijenili profilnu sliku.");

                return linkToFile;
            }
            return "Odustali ste od promjene profilne slike.";
        } catch (Exception ex)
        {
            return "Profilna slika nije promijenjena.";
        }
    }
}
