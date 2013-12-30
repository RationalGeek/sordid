using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sordid.Core
{
    public class SordidDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}
