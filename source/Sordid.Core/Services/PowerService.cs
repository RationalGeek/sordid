using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class PowerService : IPowerService
    {
        private IPowerRepository _powerRepo;

        public PowerService(IPowerRepository powerRepo)
        {
            _powerRepo = powerRepo;
        }

        public async Task<IList<Power>> GetStockPowers()
        {
            var query = _powerRepo.GetQueryable().Where(p => p.Type == PowerType.Stock).OrderBy(s => s.Name);
            return (await _powerRepo.Query(query));
        }
    }
}
