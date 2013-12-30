using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Model;
using Sordid.Core.Model.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sordid.Core
{
    public class SordidDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var modelBuilders = Assembly.GetAssembly(typeof(SordidDbContext)).GetTypes()
                .Where(t => t != typeof(IModelBuilder))
                .Where(t => typeof(IModelBuilder).IsAssignableFrom(t))
                .ToList();
            foreach (var builderType in modelBuilders)
            {
                var builder = Activator.CreateInstance(builderType) as IModelBuilder;
                builder.OnModelCreating(modelBuilder);
            }
        }

        static SordidDbContext()
        {
            Database.SetInitializer<SordidDbContext>(new DropCreateDatabaseAlways<SordidDbContext>());
        }
    }
}
