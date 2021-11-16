using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Services;
using Jobsity.Chat.Core.Helpers;

namespace Jobsity.Chat.Bot
{
    public class UserQueueConsumer : ConsumerService, IUserConsumer
    {
        private HttpClient _client;
        private string _url = "https://stooq.com/q/l/?s=#stock&f=sd2t2ohlcv&h&e=csv";
        private IBotProducer _producer;

        public UserQueueConsumer(string rabbitConnection, IBotProducer producer) : base(rabbitConnection)
        {
            _client = new HttpClient();
            _producer = producer;
        }

        private string GetStockMessage(string code)
        {
            try
            {
                string response = _client.GetStringAsync(_url.Replace("#stock", code)).Result;

                /* Gets second line of csv */
                string[] lines = response.Split('\n');
                string secondLine = lines[1];

                /* Get properties */
                List<string> properties = secondLine.Split(",").ToList();
                string stockName = properties.First();
                properties.Reverse();
                string closePrice = properties[1];
                if (closePrice == "N/D")
                    throw new Exception("Not found!");
                return $"{stockName} quote is ${closePrice} per share";
            }
            catch (Exception ex)
            {
                return $"Error trying to get stock \"{code}\": " + ex.Message;
            }
        }

        public void WaitForStockCode()
        {
            base.RabbitConsume<string>(Constant.USER_QUEUE, (code) =>
            {
                string message = GetStockMessage(code);
                /* Produces message */
                _producer.SendToAll(message);
            });
        }
    }
}