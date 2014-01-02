using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Model;
using Sordid.Core.Model.ModelBuilders;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Sordid.Core
{
    public class SordidDbContext : IdentityDbContext<ApplicationUser>
    {
        static SordidDbContext()
        {
            // TODO: Eventually have to convert to using migrations
            Database.SetInitializer<SordidDbContext>(new DropCreateDatabaseIfModelChanges<SordidDbContext>());
        }

        public DbSet<Character> Characters { get; set; }

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


    }
}
