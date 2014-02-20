using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class SkillModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Character>()
                .HasMany(c => c.Skills)
                .WithRequired(cs => cs.Character)
                .HasForeignKey(cs => cs.CharacterId);

            modelBuilder
                .Entity<CharacterSkill>()
                .HasRequired(cs => cs.Skill)
                .WithMany()
                .HasForeignKey(cs => cs.SkillId);
        }
    }
}
