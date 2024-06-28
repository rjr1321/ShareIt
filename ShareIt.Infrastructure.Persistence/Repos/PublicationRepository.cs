using Microsoft.EntityFrameworkCore;
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


        private IQueryable<Comment> IncludeReplies(IQueryable<Comment> query, int maxDepth)
        {
            if (maxDepth == 0)
            {
                return query;
            }

            return query.Include(c => c.Replies)
                        .ThenInclude(r => r.Replies)
                        .ThenInclude(r => r.Replies);
        }

        public override async Task<ICollection<Publication>> GetAllAsync()
        {
            try
            {
                var query = _context.Publications
                    .Include(p => p.Profile);

                // Include comments and replies recursively
                var publications = await query.ToListAsync();
                foreach (var publication in publications)
                {
                    publication.Comments = IncludeReplies(_context.Comments.Where(c => c.IdPublication == publication.Id), 3).ToList();
                }

                return publications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
