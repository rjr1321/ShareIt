using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain
{
    public class Friendship
    {
        public string AppProfileId { get; set; }
        public AppProfile AppProfile { get; set; }

        public string FriendId { get; set; }
        public AppProfile Friend { get; set; }
    }
}
