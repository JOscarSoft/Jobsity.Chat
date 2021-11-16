using Jobsity.Chat.Core.Entities;

namespace Jobsity.Chat.Core.Contracts
{
    public interface ICommandService
    {
        string GetCommandError(string text);
        StockCommand GetCommandInfos(string text);
        bool IsCommand(string text);
    }
}