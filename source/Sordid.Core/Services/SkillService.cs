using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class SkillService : ISkillService
    {
        private ISkillRepository _skillRepo;

        public SkillService(ISkillRepository skillRepo)
        {
            _skillRepo = skillRepo;
        }

        public async Task<IList<Skill>> GetStandardSkills()
        {
            var query = _skillRepo.GetQueryable().Where(s => s.Type == SkillType.Standard).OrderBy(s => s.Name);
            return (await _skillRepo.Query(query));
        }
    }
}
