using System;

namespace Jobsity.Chat.Core.Entities
{
    public class Message
    {
        public Message() { }
        public Message(Models.Message dbMessage)
        {
            this.CreateTime = dbMessage.CreateTime;
            this.TextMessage = dbMessage.TextMessage;
            this.UserName = dbMessage.Writter.UserName;
            this.UserID = dbMessage.Writter.Id;
        }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string TextMessage { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }
    }
}