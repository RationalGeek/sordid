using Microsoft.AspNet.Identity.EntityFramework;

namespace Sordid.Core.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
    }
}
