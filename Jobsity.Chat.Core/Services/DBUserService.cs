using System;
using System.Collections.Generic;
using System.Linq;
using Jobsity.Chat.Core.DBContext;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Models;

namespace Jobsity.Chat.Core.Services
{
    public class DBUserService : IUserService
    {
        private AppDbContext _context;
        public DBUserService(AppDbContext context)
        {
            _context = context;
        }

        public UserChat GetUser(string id)
        {
            UserChat user = _context.UsersChat.FirstOrDefault(f => f.Id == id);
            return user;
        }
    }
}