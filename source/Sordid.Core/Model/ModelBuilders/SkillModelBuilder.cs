using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class SkillModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CharacterSkill>()
                .HasRequired(cs => cs.Character)
                .WithMany()
                .HasForeignKey(cs => cs.CharacterId);

            modelBuilder
                .Entity<CharacterSkill>()
                .HasRequired(cs => cs.Skill)
                .WithMany()
                .HasForeignKey(cs => cs.SkillId);
        }
    }
}
