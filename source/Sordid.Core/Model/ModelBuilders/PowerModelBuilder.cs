using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class PowerModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CharacterPower>()
                .HasRequired(cp => cp.Character)
                .WithMany()
                .HasForeignKey(cp => cp.CharacterId);

            modelBuilder
                .Entity<CharacterPower>()
                .HasRequired(cp => cp.Power)
                .WithMany()
                .HasForeignKey(cp => cp.PowerId);
        }
    }
}
