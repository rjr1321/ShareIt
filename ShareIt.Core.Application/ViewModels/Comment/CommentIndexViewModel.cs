using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.ViewModels.Comment
{
    public class CommentIndexViewModel
    {
        public ClaimsPrincipal User { get; set; }
      
    }
}
