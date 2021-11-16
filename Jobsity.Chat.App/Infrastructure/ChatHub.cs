using System.Threading.Tasks;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Entities;
using Jobsity.Chat.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace Jobsity.Chat.App.Infrastructure
{
    public class ChatHub : Hub
    {
        private ICommandService _commandService;
        private IMessageService _messageService;
        private IUserService _userService;
        private IUserProducer _userBotQueueProducer;
        public ChatHub(ICommandService commandService, IMessageService messageService, IUserService userService, IUserProducer userBotQueueProducer)
        {
            _commandService = commandService;
            _messageService = messageService;
            _userService = userService;
            _userBotQueueProducer = userBotQueueProducer;
        }

        public async void SendAll(Core.Entities.Message message)
        {

            if (_commandService.IsCommand(message.TextMessage))
            {
                StockCommand infos = _commandService.GetCommandInfos(message.TextMessage);
                await Broadcast(message);

                if (infos.Error != null)
                {
                    await Broadcast(AdminMessage(infos.Error));
                }
                else
                {
                    _userBotQueueProducer.SearchStockFromCode(infos.Parameter);
                }
            }
            else
            {
                string userId = message.UserID;
                UserChat user = _userService.GetUser(userId);
                var dbMessage = new Jobsity.Chat.Core.Models.Message(message.TextMessage, user);
                _messageService.AddMessage(dbMessage);
                message.CreateTime = dbMessage.CreateTime;
                await Broadcast(message);
            }
        }

        private Core.Entities.Message AdminMessage(string text)
        {
            return new Core.Entities.Message
            {
                TextMessage = text,
                UserName = "Admin",
            };
        }

        private async Task Broadcast(Core.Entities.Message chatMessage)
        {
            await Clients.All.SendAsync("receive", chatMessage);
        }
    }
}