using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShareIt.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareIt.Core.Application.Interfaces.Infrastructure;
using ShareIt.Infrastructure.Identity.Context;
using ShareIt.Infrastructure.Identity.Services;
using System.Reflection;

namespace ShareIt.Infrastructure.Identity
{

    public static class ServicesRegistration
    {

        public static void AddInfrastructureIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });
            #endregion

            #region Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();
            #endregion

            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAccountServices, AccountServices>();
            #endregion
        }
    }

}
