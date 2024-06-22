using ShareIt.Core.Application;
using ShareIt.Core.Application.Interfaces;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Persistence
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
