using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class PowerLevelModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Character>()
                .HasOptional(c => c.PowerLevel)
                .WithMany()
                .HasForeignKey(c => c.PowerLevelId);
        }
    }
}
