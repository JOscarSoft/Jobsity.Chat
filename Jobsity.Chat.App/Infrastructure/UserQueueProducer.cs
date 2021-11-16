using Jobsity.Chat.Core;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Services;
using Jobsity.Chat.Core.Helpers;
using Microsoft.Extensions.Configuration;


namespace Jobsity.Chat.App.Infrastructure
{
    public class UserBotQueueProducer : ProducerService, IUserProducer
    {
        public UserBotQueueProducer(IConfiguration configuration) :
            base(Helper.GetConnection(configuration, "RabbitConnectionString"))
        {

        }

        public void SearchStockFromCode(string Code)
        {
            base.RabbitProduce<string>(Constant.USER_QUEUE, Code);
        }
    }
}