using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    /// <summary>
    /// By default the tables created for Microsoft.AspNet.Identity.EntityFramework
    /// start with "AspNet" which is not cool.  This fixes that.
    /// </summary>
    public class AppUserTableNames : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }
    }
}
