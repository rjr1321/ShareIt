namespace ShareIt.Core.Application;

public class RegisterRequest : Request
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
}
