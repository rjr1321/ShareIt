using Microsoft.AspNetCore.Http;
using ShareIt.Core.Application;

namespace ShareItApp.MiddleWares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthResponse>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }

    }
}
