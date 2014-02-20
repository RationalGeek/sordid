using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class CharacterModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasRequired<ApplicationUser>(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.ApplicationUserId);

            modelBuilder.Entity<Character>()
                .Property(c => c.Notes)
                .IsMaxLength();

            modelBuilder
                .Entity<Character>()
                .HasMany(c => c.Consequences)
                .WithRequired(c => c.Character)
                .HasForeignKey(c => c.CharacterId);
        }
    }
}
