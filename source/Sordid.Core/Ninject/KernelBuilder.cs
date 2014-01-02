using Ninject;

namespace Sordid.Core.Ninject
{
    public class KernelBuilder
    {
        public IKernel BuildKernel()
        {
            var kernel = new StandardKernel();

            return kernel;
        }
    }
}
