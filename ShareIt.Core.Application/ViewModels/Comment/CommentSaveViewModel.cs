using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class CommentSaveViewModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string Content { get; set; }

        public int? IdParentComment { get; set; }

        public int IdPublication { get; set; }
        public string IdProfile { get; set; }
        public AppProfile? Profile { get; set; }
        public DateTime DateTime { get; set; }

    }
}
