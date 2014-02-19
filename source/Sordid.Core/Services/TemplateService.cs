using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class TemplateService : ITemplateService
    {
        private ITemplateRepository _templateRepo;

        public TemplateService(ITemplateRepository templateRepo)
        {
            _templateRepo = templateRepo;
        }

        public async Task<IList<Template>> GetAllTemplates()
        {
            var query = _templateRepo.GetQueryable().OrderBy(t => t.Name);
            return (await _templateRepo.Query(query));
        }
    }
}
