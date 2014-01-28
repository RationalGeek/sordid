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
            // Must be done before base.Update
            entity.Powers.ForEach(p =>
            {
                p.CharacterId = entity.Id;
                p.PowerId = p.Power.Id;
            });

            entity = base.Update(entity);

            // TODO: Make some reusable code for updating child entities

            // Make sure the skills are attached to the context so that they get updated
            entity.Skills.ForEach(s => {
                var entry = DbContext.Entry(s);
                entry.State = EntityState.Modified;
            });

            // Make sure the aspects are attached to the context so that they get updated
            entity.Aspects.ForEach(a =>
            {
                var entry = DbContext.Entry(a);
                entry.State = EntityState.Modified;
            });

            // Make sure the powers are attached to the context so that they get updated
            entity.Powers.ForEach(p =>
            {
                // Powers can be added dynamically by the UI, so we need to account for that
                var entry = DbContext.Entry(p);
                if (p.Id == 0)
                    entry.State = EntityState.Added;
                else
                    entry.State = EntityState.Modified;
            });

            FixUpLovEntities(entity);

            return entity;
        }

        public void FixUpLovEntities(Character entity)
        {
            // TODO: Test if this is necessary or not

            entity.Powers.Select(p => (BaseEntity)p.Power)
                .Concat(entity.Skills.Select(s => (BaseEntity)s.Skill))
                .Concat(entity.Aspects.Select(a => (BaseEntity)a.Aspect))
                .ToList().ForEach(e =>
                {
                    var entry = DbContext.Entry(e);
                    entry.State = EntityState.Unchanged;
                });
        }
    }
}
