using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? PublicationId { get; set; }
        public Publication? Publication { get; set; }
    }
}
