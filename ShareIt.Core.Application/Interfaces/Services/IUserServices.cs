using Microsoft.AspNetCore.Http;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public interface IUserServices : IGenericServices<AppProfile, RegisterViewModel, UserFriendshipViewModel>
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(RegisterViewModel vm, string origin);
        Task SignOutAsync();

        Task<AppProfile> GetByIdAsync(string id);

        public string UploadFile(string archive, IFormFile file, string id, bool isEditMode = false, string imagePath = "");

        Task<AppProfile> UpdateAsync(AppProfile entity, string id);
    }
}
