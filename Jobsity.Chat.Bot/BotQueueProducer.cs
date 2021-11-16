using Jobsity.Chat.Core.Entities;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Helpers;
using Jobsity.Chat.Core.Services;

namespace Jobsity.Chat.Bot
{
    public class BotQueueProducer : ProducerService, IBotProducer
    {
        public BotQueueProducer(string rabbitConnectionString) : base(rabbitConnectionString)
        {

        }

        public void SendToAll(string message)
        {
            Message chatMessage = new Message
            {
                TextMessage = message,
                UserName = "Bot"
            };
            base.RabbitProduce<Message>(Constant.BOT_QUEUE, chatMessage);
        }
    }
}