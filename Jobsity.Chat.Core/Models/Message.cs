using System;
using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Core.Models
{
    public class Message
    {
        public Message() { }
        public Message(string text, UserChat writter)
        {
            this.TextMessage = text;
            this.Writter = writter;
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Required]
        public string TextMessage { get; set; }

        public string UserId { get; set; }

        public virtual UserChat Writter { get; set; }
    }
}
