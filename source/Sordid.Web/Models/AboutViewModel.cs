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
            // TODO: For some reason this didn't work when deployed to Azure
            //var attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
            //if (attributes.Length > 0)
            //{
            //    var infoVersionAttr = (AssemblyInformationalVersionAttribute) attributes[0];
            //    Version = infoVersionAttr.InformationalVersion;
            //}

            Version = this.GetType().Assembly.GetName().Version.ToString();
        }

        public string Version { get; private set; }
    }
}