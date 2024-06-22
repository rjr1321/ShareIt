using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain
{
    public class AppProfile
    {
        public string IdUser { get; set; }
        public string PhotoProfile { get; set; }
        public ICollection<Friendship>? Friends { get; set; }
        public ICollection<Publication>? Publications { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
