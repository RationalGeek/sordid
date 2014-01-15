using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class AspectService : IAspectService
    {
        private IAspectRepository _aspectRepo;

        public AspectService(IAspectRepository aspectRepo)
        {
            _aspectRepo = aspectRepo;
        }

        public async Task<IList<Aspect>> GetStandardAspects()
        {
            var query = _aspectRepo.GetQueryable().OrderBy(a => a.Order);
            return (await _aspectRepo.Query(query));
        }
    }
}
