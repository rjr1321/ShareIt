using AutoMapper;
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
    public class UserServices : GenericServices<AppProfile, RegisterViewModel, UserViewModel>, IUserServices
    {
        public readonly IAppProfileRepository _repository;

        public readonly IMapper _mapper;

        private readonly IAccountServices _accountService;

        public UserServices(IAppProfileRepository repository, IMapper mapper, IAccountServices accountService) : base (repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _accountService = accountService;

        }

        public virtual async Task<AppProfile> GetByIdAsync(string id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }




        public async Task<AuthResponse> LoginAsync(LoginViewModel vm)
        {
            AuthRequest loginRequest = _mapper.Map<AuthRequest>(vm);
            AuthResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<AppProfile> UpdateAsync(AppProfile entity, string id)
        {
            try
            {
                return await _repository.UpdateAsync(entity, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            RegisterResponse response = await _accountService.RegisterBasicUserAsync(registerRequest, origin);

            if(!response.HasError) {

                await AddAsync(new AppProfile
                {
                    IdUser = response.EntityId,
                    PhotoProfile = UploadFile(vm.Photo, response.EntityId),

                });

                
            
            }

            return response;
           

        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Profile/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

        
    }
}
