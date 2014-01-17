using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class PowerModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Power>()
                .HasRequired(p => p.Character)
                .WithMany()
                .HasForeignKey(p => p.CharacterId);
        }
    }
}
