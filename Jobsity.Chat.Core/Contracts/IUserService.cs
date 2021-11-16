using Jobsity.Chat.Core.Models;

namespace Jobsity.Chat.Core.Contracts
{
    public interface IUserService
    {
        UserChat GetUser(string id);
    }
}
