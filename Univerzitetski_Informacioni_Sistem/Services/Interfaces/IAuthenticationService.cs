using Firebase.Auth;

namespace Univerzitetski_Informacioni_Sistem.Services.Interfaces;

public interface IAuthenticationService
{
    Task<UserCredential> AuthenticateUser(string email, string password);
    Task ChangePassword(string email);
}
