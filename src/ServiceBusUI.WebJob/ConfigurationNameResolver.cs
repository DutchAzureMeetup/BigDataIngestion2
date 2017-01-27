using Microsoft.Azure;
using Microsoft.Azure.WebJobs;

namespace ServiceBusUI.WebJob
{
    public class ConfigurationNameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return CloudConfigurationManager.GetSetting(name);
        }
    }
}
