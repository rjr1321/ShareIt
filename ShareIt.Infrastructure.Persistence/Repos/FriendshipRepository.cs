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
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {

        public readonly DefaultContext _context;

        public FriendshipRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
    }
}
