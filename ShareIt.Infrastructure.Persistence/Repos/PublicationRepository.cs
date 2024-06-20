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
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        public readonly DefaultContext _context;

            public PublicationRepository(DefaultContext context): base(context)
        {
            _context = context;
        }
    }
}
