namespace Univerzitetski_Informacioni_Sistem.Models;
public record UserModel
{
    public string UID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Birthday { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Index { get; set; }
    public string University { get; set; }
    public string Faculty { get; set; }
    public string Department { get; set; }
    public string ProfilePhoto { get; set; }
}
