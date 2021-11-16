using System.Collections.Generic;
using Jobsity.Chat.Core.Entities;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Helpers;

namespace Jobsity.Chat.App.Services
{
    public class CommandService : ICommandService
    {
        private StockCommand _commandInfos = new StockCommand();

        private List<string> _mockedCommandList = new List<string>() { "/stock" };

        public string GetCommandError(string text)
        {
            if (!text.Contains("="))
                return Errors.NULL_PARAMETER_INDICATOR;

            string[] splitter = text.Split("=");
            string command = splitter[0];
            string param = splitter[1];
            if (string.IsNullOrWhiteSpace(command.Replace("/", "")))
                return Errors.COMMAND_NULL;

            if (!_mockedCommandList.Contains(command))
                return $"'{command}' " + Errors.NOT_VALID_COMMAND;

            if (string.IsNullOrWhiteSpace(param))
                return Errors.NULL_PARAMETER;

            return null;
        }

        public StockCommand GetCommandInfos(string text)
        {
            string error = GetCommandError(text);
            if (error == null)
            {
                string[] splitter = text.Split("=");
                string command = splitter[0];
                if (!_mockedCommandList.Contains(command))
                    return null;

                string parameter = splitter[1];
                _commandInfos.Command = command;
                _commandInfos.Parameter = parameter;
            }

            _commandInfos.Error = error;
            return _commandInfos;
        }

        public bool IsCommand(string text)
        {
            return text.StartsWith("/");
        }
    }
}