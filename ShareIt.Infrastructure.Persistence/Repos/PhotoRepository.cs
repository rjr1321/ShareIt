using ShareIt.Core.Application.Interfaces;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Persistence;
using ShareIt.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public readonly DefaultContext _context;

        public PhotoRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
    }
}
