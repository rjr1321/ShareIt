using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class FriendshipIndexViewModel
    {
        public ClaimsPrincipal? UserClaim { get; set; }
        public FriendshipSaveViewModel? Svm { get; set; }
        public List<UserFriendshipViewModel>? Users { get; set; }
        public List<PublicationViewModel>? Publications { get; set; }
    }
}
