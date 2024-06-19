namespace ShareIt.Core.Application;

public class RegisterRequest : Request
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Phone { get; set; }
}
