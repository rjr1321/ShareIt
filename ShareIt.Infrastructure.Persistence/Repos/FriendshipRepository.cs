using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {

        public readonly DefaultContext _context;

        public FriendshipRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
    }
}
