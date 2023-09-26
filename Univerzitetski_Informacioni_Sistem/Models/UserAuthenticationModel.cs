using System.ComponentModel.DataAnnotations;

namespace Univerzitetski_Informacioni_Sistem.Models;
public record UserAuthenticationModel
{
    [EmailAddress(ErrorMessage = "Email adresa nije validna.")]
    [Required(ErrorMessage = "Molimo unesite svoju email adresu.")]
    public string Email { get; set; }
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Lozinka treba da ima minimalno 8 znakova.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Lozinka nije validna. Treba da bude u formatu: Abcdef1*")]
    [Required(ErrorMessage = "Molimo unesite svoju lozinku.")]
    public string Password { get; set; }
}
