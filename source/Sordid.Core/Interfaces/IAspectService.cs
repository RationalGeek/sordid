using Sordid.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sordid.Core.Interfaces
{
    public interface IAspectService
    {
        Task<IList<Aspect>> GetStandardAspects();
    }
}
