using System.Threading.Tasks;

namespace Sordid.Core.Interfaces
{
    public interface IUnitOfWork
    {
        SordidDbContext Context { get; }
        Task Save();
    }
}
