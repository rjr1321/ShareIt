using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int? IdParentComment { get; set; }
        public Comment? ParentComment { get; set; }
        public int IdPublication { get; set; }
        public Publication? publication { get; set; }
        public string IdProfile { get; set; }
        public AppProfile? Profile { get; set; }
        public ICollection<Comment>? Replies { get; set; }

        public DateTime DateTime { get; set; }

       
        
    }
}
