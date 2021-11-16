using Jobsity.Chat.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Jobsity.Chat.Core.Services;
using Jobsity.Chat.Core.Helpers;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.SignalR;
using Jobsity.Chat.Core.Entities;

namespace Jobsity.Chat.App.Infrastructure
{
    public class BotUsersQueueConsumer : BackgroundService, IBotConsumer
    {
        private IConsumerService _consumerService;
        private IHubContext<ChatHub> _hubContext;

        public BotUsersQueueConsumer(IConfiguration configuration, IHubContext<ChatHub> hubContext) :
            base()
        {
            _consumerService = new ConsumerService(Helper.GetConnection(configuration, "RabbitConnectionString"));
            _hubContext = hubContext;
        }


        public void WaitBotResponse()
        {
            _consumerService.RabbitConsume<Message>
            (
                Constant.BOT_QUEUE,
                async botMessage =>
                {
                    await _hubContext.Clients.All.SendAsync("receive", botMessage);
                }
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            this.WaitBotResponse();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _consumerService.Dispose();
            base.Dispose();
        }
    }
}