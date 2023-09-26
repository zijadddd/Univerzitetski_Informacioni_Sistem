using Univerzitetski_Informacioni_Sistem.Models;

namespace Univerzitetski_Informacioni_Sistem.Services.Interfaces;
public interface IUserModelService
{
    Task<UserModel> GetLoggedUserAsync();
    Task<UserModel> GetUser(string studentId);
    Task<string> ChangeUserPhoto();
}
