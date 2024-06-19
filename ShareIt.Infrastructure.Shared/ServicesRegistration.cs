using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareIt.Core.Application.Interfaces.Infrastructure;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Shared.Services;


namespace ShareIt.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructureSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailServices, EmailServices>();
        }
    }

}
