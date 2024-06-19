using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ShareIt.Core.Application;
using ShareIt.Core.Application.Interfaces.Infrastructure;
using ShareIt.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Identity.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailServices _emailService;

        public AccountServices(UserManager<User> userManager, SignInManager<User> signInManager, IEmailServices emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<string> GetUsernameById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user != null ? user.UserName : null;
        }
        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        {
            AuthResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {

                user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No Accounts registered with this user or email";
                    return response;
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Password";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed yet";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.Username}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new User
            {
                Email = request.Email,
             
                Name = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                response.EntityId = user.Id;
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });


            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {

                user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"Account does not exist";
                    return response;
                }
            }

            string newPassword = GenerateRandomPassword();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resetting password";
                return response;
            }


            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = @$"<h3>Hi {user.UserName}!</h3> 
            
            <p>this is your new password: {newPassword}</p>
            
            <p>Share Log-In: ",
                Subject = "New Password for shareIt"
            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }
        private async Task<string> SendVerificationEmailUri(User user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        private string GenerateRandomPassword()
        {
            const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string specialChars = "!@#$%^&*()-_=+[{]};:'\",<.>/?";
            var random = new Random();

            // Include at least one special character and one digit
            var newPassword = new string(
                Enumerable.Repeat(alphanumericChars, 6)
                .Select(s => s[random.Next(s.Length)])
                .Concat(new[] { specialChars[random.Next(specialChars.Length)] })
                .Concat(new[] { alphanumericChars[random.Next(alphanumericChars.Length)] })
                .OrderBy(c => random.Next())
                .ToArray());

            return newPassword;
        }
    }
}
