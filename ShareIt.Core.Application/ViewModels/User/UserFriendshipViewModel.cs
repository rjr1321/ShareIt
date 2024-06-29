using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class UserFriendshipViewModel
    {
        public string? Id { get; set; }  
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Photo { get; set; }
        public string? Username { get; set; }
        public bool Added { get; set; }
        public FriendshipSaveViewModel svm { get; set; }
    }
}
