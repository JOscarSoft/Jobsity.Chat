using Jobsity.Chat.App.Models;
using Jobsity.Chat.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Jobsity.Chat.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService _messageService;

        public HomeController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var chatMessages = new List<Core.Entities.Message>();
            var dbMessages = _messageService.GetLastMessages();
            if(dbMessages != null && dbMessages.Count > 0)
            {
                chatMessages = dbMessages.Select(s => new Core.Entities.Message(s)).ToList();
            }
            return View(chatMessages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
