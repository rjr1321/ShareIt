namespace ShareIt.Core.Application;

public class ResetPasswordRequest : Request
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
 