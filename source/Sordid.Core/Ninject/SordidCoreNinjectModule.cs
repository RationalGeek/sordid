using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Sordid.Core.Ninject
{
    public class SordidCoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Use standard convention-based configuration for most things
            this.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .Excluding<UnitOfWork>()
                .BindDefaultInterfaces()
                .Configure(b => { b.InTransientScope(); })
                );
        }
    }
}
