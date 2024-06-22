using Microsoft.AspNetCore.Mvc.Filters;
using ShareItApp.Controllers;

namespace ShareItApp.MiddleWares
{
    public class LoginAuth : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public LoginAuth(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                context.Result = controller.RedirectToAction("Index", "Publication");
            }
            else
            {
                await next();
            }
        }
    }
}
