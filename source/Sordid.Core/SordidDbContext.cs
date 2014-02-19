using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Model;
using Sordid.Core.Model.ModelBuilders;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Sordid.Core
{
    public class SordidDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Aspect> Aspects { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<PowerLevel> PowerLevels { get; set; }
        public DbSet<CharacterPower> CharacterPowers { get; set; }

        public SordidDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

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
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            ProcessBaseEntities();
        }

        private void ProcessBaseEntities()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var baseEntity = entry.Entity as BaseEntity;
                if (baseEntity == null)
                    continue;

                SetEntityDateFields(entry, baseEntity);
                UnmangleTimestampColumn(entry, baseEntity);
            }
        }

        private void SetEntityDateFields(DbEntityEntry entry, BaseEntity baseEntity)
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                if (baseEntity.DateCreated == default(DateTime))
                    baseEntity.DateCreated = DateTime.Now;
                baseEntity.DateUpdated = DateTime.Now;
            }
        }

        /// <summary>
        /// When an EF timestamp column (used for optimistic concurrency)
        /// goes over the wire in AJAX it gets mangled.  Therefore it is Base64 encoded
        /// and when it comes back the Base64 version is preserved and we need
        /// to put it back into the actual Timestamp column.
        /// </summary>
        private void UnmangleTimestampColumn(DbEntityEntry entry, BaseEntity baseEntity)
        {
            if (entry.State == EntityState.Modified && !String.IsNullOrWhiteSpace(baseEntity.ConcurrencyVersionBase64))
            {
                baseEntity.ConcurrencyVersion = Convert.FromBase64String(baseEntity.ConcurrencyVersionBase64);
                entry.OriginalValues["ConcurrencyVersion"] = baseEntity.ConcurrencyVersion;
            }
        }
    }
}
