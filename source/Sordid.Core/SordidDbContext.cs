using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Model;
using Sordid.Core.Model.ModelBuilders;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Sordid.Core
{
    public class SordidDbContext : IdentityDbContext<ApplicationUser>
    {
        static SordidDbContext()
        {
            // TODO: Eventually have to convert to using migrations
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

        public override int SaveChanges()
        {
            SetEntityDateFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SetEntityDateFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetEntityDateFields()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var baseEntity = entry.Entity as BaseEntity;
                if (baseEntity == null)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    baseEntity.DateCreated = DateTime.Now;
                    baseEntity.DateUpdated = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    baseEntity.DateUpdated = DateTime.Now;
                }
            }
        }
    }
}
