using Sordid.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sordid.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        public SordidDbContext Context { get; private set; }

        /// <summary>
        /// Simple GUID.  Should only be used to track UoWs across debugging sessions.
        /// </summary>
        public Guid Identity { get; private set; }

        public UnitOfWork(SordidDbContext context)
        {
            Context = context;
            Identity = Guid.NewGuid();
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
