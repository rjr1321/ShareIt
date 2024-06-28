using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class PublicationIndexViewModel
    {
        public ClaimsPrincipal? UserClaim { get; set; }
        public PublicationSaveViewModel? Svm { get; set; }
        public List<PublicationViewModel>? Publications { get; set; }

    }
}
