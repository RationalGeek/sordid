using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;

namespace Sordid.Core.Repositories
{
    public class PowerRepository : BaseRepository<Power>, IPowerRepository
    {
        public PowerRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }
    }
}
