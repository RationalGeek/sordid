using Sordid.Core.Model;
using System.Threading.Tasks;

namespace Sordid.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUser();
    }
}
