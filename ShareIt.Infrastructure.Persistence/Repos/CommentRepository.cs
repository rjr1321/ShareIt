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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public readonly DefaultContext _context;
        
        public CommentRepository(DefaultContext context): base(context) {
        
        _context = context;
        }
    }
}
