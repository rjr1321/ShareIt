using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class UserIndexViewModel
    { 
        public ClaimsPrincipal? UserClaim { get; set; }

        public LoginViewModel? Login { get; set; }

        public RegisterViewModel? Register { get; set; } 

        public ForgotPasswordViewModel? ForgotPassword { get; set; }

        public bool HasError { get; set; }

        public string? Error { get; set; }

        public UserIndexViewModel()
        {
            this.HasError = false;
        }
    }
}
