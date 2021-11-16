using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Jobsity.Chat.Bot
{
    class Program
    {
        static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");
            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                var builder =
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                rabbitConnection = configuration.GetConnectionString("RabbitConnectionString");
            }

            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                Console.WriteLine("Rabbit`s connection string is required!");
                return;
            }

            BotQueueProducer producer = new BotQueueProducer(rabbitConnection);
            UserQueueConsumer consumer = new UserQueueConsumer(rabbitConnection, producer);
            consumer.WaitForStockCode();
            await Task.Delay(Timeout.Infinite, Cts.Token).ConfigureAwait(false);
        }
    }
}
