using System;

namespace Jobsity.Chat.Core.Contracts
{
    public interface IConsumerService : IDisposable
    {
        void RabbitConsume<T>(string queue, Action<T> execute);
    }
}