using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareIt.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {

        public static void AddInfrastructurePersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DefaultContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(DefaultContext).Assembly.FullName));
            });


            #region Repositories
       /*     services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<IAppProfileRepository, AppProfileRepository>();
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();*/


            #endregion
        }
    }
}
