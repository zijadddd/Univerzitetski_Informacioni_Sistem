using Firebase.Auth;
using Firebase.Auth.Providers;
using Univerzitetski_Informacioni_Sistem.Data;
using Univerzitetski_Informacioni_Sistem.Services.Interfaces;

namespace Univerzitetski_Informacioni_Sistem.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{

    private readonly FirebaseAuthClient firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig()
    {
        ApiKey = FirebaseSecretData.ApiKey,
        AuthDomain = FirebaseSecretData.AuthDomain,
        Providers = new FirebaseAuthProvider[] { new EmailProvider() }
    });

    public async Task<UserCredential> AuthenticateUser(string email, string password)
    {
        return await firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
    }

    public async Task ChangePassword(string email)
    {
        await firebaseAuthClient.ResetEmailPasswordAsync(email);
    }
}
