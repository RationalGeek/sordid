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
            FixUpLovEntities(entity);

            return base.Update(entity);
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

        /// <summary>
        /// Ensure that reference entities, which should not be updated,
        /// are not included
        /// </summary>
        public void FixUpLovEntities(Character entity)
        {
            // TODO: Test if this is necessary or not

            entity.Powers.Select(p => (BaseEntity)p.Power)
                .Concat(entity.Skills.Select(s => (BaseEntity)s.Skill))
                .Concat(entity.Aspects.Select(a => (BaseEntity)a.Aspect))
                .Where(e => e != null)
                .ToList().ForEach(e =>
                {
                    var entry = DbContext.Entry(e);
                    entry.State = EntityState.Unchanged;
                });
        }
    }
}
