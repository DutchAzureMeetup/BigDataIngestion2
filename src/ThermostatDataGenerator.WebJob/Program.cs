using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amqp;
using Amqp.Framing;
using Microsoft.Azure;
using Newtonsoft.Json;

namespace ThermostatDataGenerator.WebJob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Task t = MainAsync(CreateOptions());
                t.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        static Options CreateOptions()
        {
            string connectionString = CloudConfigurationManager.GetSetting("AzureWebJobsEventHub");
            
            Options options = new Options();
            options.CustomerId = CloudConfigurationManager.GetSetting("ProjectName");
            options.EventHubName = GetConnectionStringPart("EntityPath=", connectionString);
            options.Namespace = GetConnectionStringPart("Endpoint=", connectionString).Replace("sb://", "").Split('.')[0];
            options.PolicyName = GetConnectionStringPart("SharedAccessKeyName=", connectionString);
            options.SasToken = GetConnectionStringPart("SharedAccessKey=", connectionString);
            return options;
        }

        static string GetConnectionStringPart(string partName, string connectionString)
        {
            int indexOfPart = connectionString.IndexOf(partName, StringComparison.Ordinal);
            int indexOfPartEnd = connectionString.IndexOf(";", indexOfPart, StringComparison.Ordinal);
            if (indexOfPartEnd == -1) // in case of EntityPath there is no ;
                indexOfPartEnd = connectionString.Length;

            int indexOfBeginPartValue = indexOfPart + partName.Length;
            int lengthOfPartValue = indexOfPartEnd - indexOfBeginPartValue;

            return connectionString.Substring(indexOfBeginPartValue, lengthOfPartValue);
        }

        static async Task MainAsync(Options options)
        {
            string namespaceUrl = $"{options.Namespace}.servicebus.windows.net";
            string policyName = options.PolicyName;
            string sasToken = WebUtility.UrlEncode(options.SasToken);

            string connectionString = $"amqps://{policyName}:{sasToken}@{namespaceUrl}/";

            Address address = new Address(connectionString);
            Connection connection;
            try
            {
                connection = await Connection.Factory.CreateAsync(address);
            }
            catch (Exception ex)
            {
                throw new Exception($"The namespace {options.Namespace} is probably wrong.", ex);
            }

            Session session = new Session(connection);

            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                DateTime date = DateTime.Now;
                Console.WriteLine($"{date} Generating data");

                ThermostatData data = new ThermostatData()
                {
                    Date = DateTime.Now,
                    ElectricityUsage = rnd.Next(0, 500),
                    CustomerId = options.CustomerId
                };

                string serializedJson = JsonConvert.SerializeObject(data);

                Message message = new Message
                {
                    BodySection = new Data { Binary = Encoding.UTF8.GetBytes(serializedJson) }
                };

                SenderLink sender = new SenderLink(session, "sender-link", options.EventHubName);

                try
                {
                    await sender.SendAsync(message);
                }
                catch (AmqpException ex)
                {
                    if (ex.Error.Condition.ToString().Contains("amqp:unauthorized-access"))
                    {
                        throw new Exception($"The policyname {options.PolicyName} or the SAS token {options.SasToken} is probably wrong.", ex);
                    }
                    else if (ex.Error.Condition.ToString().Contains("amqp:not-found"))
                    {
                        throw new Exception($"The eventhub name {options.EventHubName} is probably wrong.", ex);
                    }
                }
                
                await sender.CloseAsync();
            }

        }
    }

    class Options
    {

        public string Namespace { get; set; }

        public string PolicyName { get; set; }

        public string EventHubName { get; set; }

        public string SasToken { get; set; }

        public string CustomerId { get; set; }
    }

    class ThermostatData
    {
        public DateTime Date { get; set; }

        public int ElectricityUsage { get; set; }

        public string CustomerId { get; set; }
    }
}
