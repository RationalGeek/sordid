using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;

namespace Sordid.Core.Repositories
{
    public class AspectRepository : BaseRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
        {
        }
    }
}
