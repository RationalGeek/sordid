﻿using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class PowerModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Character>()
                .HasMany(c => c.Powers)
                .WithRequired(p => p.Character)
                .HasForeignKey(cp => cp.CharacterId);

            modelBuilder
                .Entity<CharacterPower>()
                .HasRequired(cp => cp.Power)
                .WithMany()
                .HasForeignKey(cp => cp.PowerId);
        }
    }
}
