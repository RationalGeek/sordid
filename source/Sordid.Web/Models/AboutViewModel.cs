using System.Reflection;

namespace Sordid.Web.Models
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            GetVersionString();
        }

        private void GetVersionString()
        {
            var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
            if (attributes.Length > 0)
            {
                var infoVersionAttr = (AssemblyInformationalVersionAttribute) attributes[0];
                Version = infoVersionAttr.InformationalVersion;
            }
        }

        public string Version { get; private set; }
    }
}