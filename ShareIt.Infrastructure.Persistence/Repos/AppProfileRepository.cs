using AutoMapper;
using ShareIt.Core.Application.Interfaces.Repos;
using ShareIt.Core.Domain.Entities;
using ShareIt.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Persistence.Repos
{
    public class AppProfileRepository : GenericRepository<AppProfile>, IAppProfileRepository
    {
        public readonly DefaultContext _Context;


        public AppProfileRepository(DefaultContext context, IMapper mapper) : base(context) 
        {
            _Context = context;
        }
    }
}
