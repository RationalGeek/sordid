using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sordid.Core.Repositories
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }

        public override Character Update(Character entity)
        {
            // If there are no powers, the UI will return null,
            // when a lot of this fix up code assumes an empty collection
            if (entity.Powers == null)
                entity.Powers = new List<CharacterPower>();

            // UI sets power level and template based on Id, so null out the entities
            entity.PowerLevel = null;
            entity.Template = null;

            FixUpPowers(entity);
            FixUpConsequences(entity);
            FixUpLinkingEntities(entity);
            DeletePowers(entity);
            DeleteConsequences(entity);

            return base.Update(entity);
        }

        /// <summary>
        /// Powers are weird because some of them are Stock which can't be added by
        /// the UI, and some of them are Custom which are only added by the UI.  This
        /// requires some mucking about with Ids and references.
        /// </summary>
        private void FixUpPowers(Character entity)
        {
            entity.Powers.ForEach(p =>
            {
                p.CharacterId = entity.Id;

                if (p.Power != null)
                {
                    // Powers can be added dynamically which means their IDs won't line up correctly
                    p.PowerId = p.Power.Id;

                    // Any powers being added are custom
                    if (p.Power.Id == 0)
                        p.Power.Type = PowerType.Custom;

                    // Fixes a bug where UI can add same stock power twice
                    // which causes EF to think there are dupe IDs
                    if (p.Power.Type == PowerType.Stock)
                        p.Power = null;
                }
            });
        }

        private void FixUpConsequences(Character entity)
        {
            entity.Consequences.ForEach(c => c.CharacterId = entity.Id);
        }

        /// <summary>
        /// The UI will remove powers from the incoming list if they
        /// should be deleted.  So we have to find child entities in
        /// the database that are no longer present in the local entity
        /// In addition to deleting the CharacterPowers linking rows,
        /// we also should delete Powers if they are Custom type.
        /// </summary>
        private void DeletePowers(Character entity)
        {
            var characterPowersToDelete = DbContext
                .CharacterPowers
                .Where(cp => cp.CharacterId == entity.Id)
                .Include(cp => cp.Power)
                .ToList() // Query returns all items because NOT IN is a PITA
                .Where(cp => !entity.Powers.Select(cp2 => cp2.Id).Contains(cp.Id))
                .ToList();

            var powerKeysToDelete = characterPowersToDelete.Select(cp => cp.Power)
                .Where(p => p.Type == PowerType.Custom)
                .Select(p => p.Id)
                .ToList();
            var powersToDelete = DbContext
                .Powers
                .Where(p => powerKeysToDelete.Contains(p.Id))
                .ToList();

            characterPowersToDelete.Cast<BaseEntity>()
                .Concat(powersToDelete.Cast<BaseEntity>())
                .ToList()
                .ForEach(e =>
            {
                var entry = DbContext.Entry(e);
                entry.State = EntityState.Deleted;
            });
        }

        private void DeleteConsequences(Character entity)
        {
            var consToDelete = DbContext
                .Consequences
                .Where(c => c.CharacterId == entity.Id)
                .ToList() // Query returns all items because NOT IN is a PITA
                .Where(c => !entity.Consequences.Select(c2 => c2.Id).Contains(c.Id))
                .ToList();

            consToDelete.ForEach(e =>
            {
                var entry = DbContext.Entry(e);
                entry.State = EntityState.Deleted;
            });
        }

        /// <summary>
        /// Ensure that child entities are included in the update
        /// </summary>
        public void FixUpLinkingEntities(Character entity)
        {
            entity.Skills.Cast<IIdKeyedEntity>()
                .Concat(entity.Aspects.Cast<IIdKeyedEntity>())
                .Concat(entity.Powers.Cast<IIdKeyedEntity>())
                .Concat(entity.Consequences.Cast<IIdKeyedEntity>())
                .Concat(entity.Powers.Select(p => p.Power)
                    .Where(p => p != null && p.Type == PowerType.Custom)
                    .Cast<IIdKeyedEntity>())
                .Where(e => e != null)
                .ToList().ForEach(e =>
                {
                    var entry = DbContext.Entry(e);
                    if (e.Id == 0)
                        entry.State = EntityState.Added;
                    else
                        entry.State = EntityState.Modified;
                });
        }
    }
}
