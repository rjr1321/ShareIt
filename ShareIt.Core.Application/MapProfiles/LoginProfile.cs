using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.MapProfiles
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {


            CreateMap<AuthRequest, LoginViewModel>()
                .ReverseMap();

            CreateMap<RegisterRequest, RegisterViewModel>()
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ReverseMap();

          
        }
    }
}
