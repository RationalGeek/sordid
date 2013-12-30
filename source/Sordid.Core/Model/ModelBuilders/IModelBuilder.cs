using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public interface IModelBuilder
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
