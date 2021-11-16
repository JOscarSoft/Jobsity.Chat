namespace Jobsity.Chat.Core.Contracts
{
    public interface IBotProducer
    {
        void SendToAll(string message);
    }
}