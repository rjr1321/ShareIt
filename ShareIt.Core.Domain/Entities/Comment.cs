using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string IdProfile { get; set; }
        public AppProfile Profile { get; set; }
        public List<Comment>? Replies { get; set; }
        
    }
}
