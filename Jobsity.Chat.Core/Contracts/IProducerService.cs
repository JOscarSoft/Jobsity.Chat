
namespace Jobsity.Chat.Core.Contracts
{
    public interface IProducerService
    {
        void RabbitProduce<T>(string queue, T message);
    }
}