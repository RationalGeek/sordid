using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class PowerLevelService : IPowerLevelService
    {
        private IPowerLevelRepository _powerLevelRepo;

        public PowerLevelService(IPowerLevelRepository powerLevelRepo)
        {
            _powerLevelRepo = powerLevelRepo;
        }

        public async Task<IList<PowerLevel>> GetAllPowerLevels()
        {
            var query = _powerLevelRepo.GetQueryable().OrderBy(p => p.MaxSkillRank);
            return (await _powerLevelRepo.Query(query));
        }
    }
}
