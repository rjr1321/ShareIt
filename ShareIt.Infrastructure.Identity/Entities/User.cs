using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Identity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
