using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace ServiceBusUI.WebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            HttpClient Client = new HttpClient
            {
                BaseAddress = new Uri("http://" + Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME"))
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(new { Body = "Hello, world!" }),
                Encoding.UTF8,
                "application/json");

            Client.PostAsync("/api/Message", content).GetAwaiter().GetResult();


            var config = new JobHostConfiguration()
            {
                // Use a custom NameResolver to get topic/subscription names from config.
                NameResolver = new ConfigurationNameResolver()
            };

            config.UseServiceBus();

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
