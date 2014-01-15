using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class AspectModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CharacterAspect>()
                .HasRequired(ca => ca.Character)
                .WithMany()
                .HasForeignKey(ca => ca.CharacterId);

            modelBuilder
                .Entity<CharacterAspect>()
                .HasRequired(ca => ca.Aspect)
                .WithMany()
                .HasForeignKey(ca => ca.AspectId);
        }
    }
}
