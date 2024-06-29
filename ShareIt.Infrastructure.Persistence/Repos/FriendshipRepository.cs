using Microsoft.EntityFrameworkCore;
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


        public async Task DeleteAsync(string appProfileId, string friendId)
        {
            try
            {
                var friendship = await _context.friendships
               .FirstOrDefaultAsync(f => f.AppProfileId == appProfileId && f.FriendId == friendId);

                var friendship2 = await _context.friendships
               .FirstOrDefaultAsync(f => f.AppProfileId == friendId && f.FriendId == appProfileId);

                if (friendship != null)
                {
                    _context.friendships.Remove(friendship);
                    _context.friendships.Remove(friendship2);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
