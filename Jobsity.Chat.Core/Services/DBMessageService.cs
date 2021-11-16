using System;
using System.Collections.Generic;
using System.Linq;
using Jobsity.Chat.Core.DBContext;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobsity.Chat.Core.Services
{
    public class DBMessageService : IMessageService
    {
        private AppDbContext _context;
        public DBMessageService(AppDbContext context)
        {
            _context = context;
        }

        public Message AddMessage(Message message)
        {
            message.Id = (Guid.NewGuid()).ToString();
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public List<Message> GetLastMessages(int count = 50)
        {
            List<Message> messages = _context
                                        .Messages?
                                        .Include(i => i.Writter)
                                        .OrderBy(o => o.CreateTime)
                                        .Take(count)
                                        .ToList();
            return messages;
        }
    }
}