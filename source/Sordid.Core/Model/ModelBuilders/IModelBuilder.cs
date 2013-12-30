using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sordid.Core.Model.ModelBuilders
{
    public interface IModelBuilder
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
