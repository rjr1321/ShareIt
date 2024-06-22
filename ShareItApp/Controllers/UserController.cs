using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Identity;
using ShareItApp.MiddleWares;
using System.Threading.Tasks;

namespace ShareItApp.Controllers
{
    public class UserController : Controller
    {
        public readonly IUserServices _userServices;

        public readonly UserManager<User> _userManager;

        public readonly IMapper _mapper;
        public UserController(IUserServices profileServices, UserManager<User> userManager, IMapper mapper) {
        
        _userServices = profileServices;
            _userManager = userManager;
            _mapper = mapper;

        }

        [ServiceFilter(typeof(LoginAuth))]
        public IActionResult Index()
        {
            return View(new UserIndexViewModel
            {
                UserClaim = User,
                Login = new LoginViewModel(),
                Register = new RegisterViewModel(),
                ForgotPassword = new ForgotPasswordViewModel()
            });
        }


        [ServiceFilter(typeof(LoginAuth))]
        [HttpPost]
        public async Task<IActionResult> LogIn(UserIndexViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", vm);
            }



            AuthResponse userVm = await _userServices.LoginAsync(vm.Login);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthResponse>("user", userVm);

                return RedirectToRoute(new { controller = "Publication", action = "Index" });
                 
            }
             else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return RedirectToAction("Index", vm);
            }
           
        }


        public async Task<IActionResult> LogOut()
        {
            await _userServices.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        [ServiceFilter(typeof(LoginAuth))]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", vm);
            }


            var origin = Request.Headers["origin"];

            RegisterResponse response = await _userServices.RegisterAsync(vm, origin);


            if (!response.HasError)
            {
                /*vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);*/
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }



        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToAction("LogIn");
            }

            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(Id));

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(LoginAuth))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userServices.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }


        [ServiceFilter(typeof(LoginAuth))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userServices.ForgotPasswordAsync(vm, origin);
           /* if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }*/
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        [ServiceFilter(typeof(LoginAuth))]
        [HttpPost]
        public async Task<IActionResult> Update(RegisterViewModel svm)
        {
            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToAction("LogIn");
            }

            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPasswordand ");


            if (svm.Photo == null)
            {
                ModelState.Remove("Photo"); 
            }
         


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            User user = await _userManager.GetUserAsync(User);

            user.Email = svm.Email;
            user.UserName = svm.Username;
            user.PhoneNumber = svm.PhoneNumber;
            user.LastName = svm.LastName;
            user.Name = svm.Name;

            await _userManager.UpdateAsync(user);
            if (svm.Photo != null)
            {
                AppProfile profile = await _userServices.GetByIdAsync(user.Id);

                profile.PhotoProfile = _userServices.UploadFile(svm.Photo, user.Id, true, profile.PhotoProfile);

                await _userServices.UpdateAsync(profile, profile.IdUser);

            }



            return RedirectToRoute(new { controller = "Profile", action = "Index" });
        }





   


    }





  






}




