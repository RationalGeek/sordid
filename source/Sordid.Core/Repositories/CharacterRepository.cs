using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Data.Entity;

namespace Sordid.Core.Repositories
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }

        public override Character Update(Character entity)
        {
            entity = base.Update(entity);

            // Make sure the skills are attached to the context so that they get updated
            entity.Skills.ForEach(s => {
                var entry = DbContext.Entry(s);
                entry.State = EntityState.Modified;
            });

            return entity;
        }
    }
}
