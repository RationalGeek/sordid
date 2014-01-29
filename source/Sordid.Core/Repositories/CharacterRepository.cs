using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;
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
            // Also, powers can be added dynamically which means their IDs won't line up correctly
            entity.Powers.ForEach(p =>
            {
                p.CharacterId = entity.Id;
                p.PowerId = p.Power.Id;

                // Fixes a bug where UI can add same stock power twice
                // which causes EF to think there are dupe IDs
                if (p.Power.Type == PowerType.Stock)
                    p.Power = null;
            });

            FixUpLinkingEntities(entity);
            DeletePowers(entity);

            return base.Update(entity);
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

            // TODO: Deleting custom powers hasn't been tested
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

        /// <summary>
        /// Ensure that child entities are included in the update
        /// </summary>
        public void FixUpLinkingEntities(Character entity)
        {
            entity.Skills.Cast<IIdKeyedEntity>()
                .Concat(entity.Aspects.Cast<IIdKeyedEntity>())
                .Concat(entity.Powers.Cast<IIdKeyedEntity>())
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
