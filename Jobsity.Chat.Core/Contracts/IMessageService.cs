using System.Collections.Generic;
using Jobsity.Chat.Core.Models;

namespace Jobsity.Chat.Core.Contracts
{
    public interface IMessageService
    {
        Message AddMessage(Message message);

        List<Message> GetLastMessages(int count = 50);
    }
}