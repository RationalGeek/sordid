using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
