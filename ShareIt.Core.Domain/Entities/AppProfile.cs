using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain.Entities
{
    public class AppProfile
    {
        public string IdUser { get; set; }
        public string PhotoProfile { get; set; }
        public List<AppProfile>? Friends { get; set; }
        public Publications? Publications { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
