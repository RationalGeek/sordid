using Ninject.Extensions.Logging;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sordid.Core.Repositories
{
    public class PowerLevelRepository : BaseRepository<PowerLevel>, IPowerLevelRepository
    {
        public PowerLevelRepository(IUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }
    }
}
