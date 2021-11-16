using System;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Helpers;
using Jobsity.Chat.App.Services;
using Xunit;

namespace Jobsity.Chat.Tests
{
    public class CommandServiceTests
    {
        private ICommandService _commandService = new CommandService();

        [Fact]
        public void IsNullParameter_ShouldReturnNullParameterMessage()
        {
            string text = "/stock=";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Equal(errorMessage, Errors.NULL_PARAMETER);
        }

        [Fact]
        public void IsNullCommand_ShouldReturnNullCommandMessage()
        {
            string text = "/=";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Equal(errorMessage, Errors.COMMAND_NULL);
        }

        [Fact]
        public void IsNullParameterIndicator_ShouldReturnNullParameterIndicatorMessage()
        {
            string text = "/";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Equal(errorMessage, Errors.NULL_PARAMETER_INDICATOR);
        }


        [Fact]
        public void IsValidCommand_ShouldReturnTrue()
        {
            string completeCommand = "/command";
            bool isCommand = _commandService.IsCommand(completeCommand);
            Assert.True(isCommand);
        }


        [Fact]
        public void IsNotFoundCommand_ShouldReturnCommandNotFound()
        {
            string text = "/InvalidOne=123";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Contains(Errors.NOT_VALID_COMMAND, errorMessage);
        }

        [Fact]
        public void IsValidCommand_ShouldReturnNull()
        {
            string text = "/stock=AAPL.us";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void IsNotACommand_ShouldReturnFalse()
        {
            string notACommand = "Simple message";
            bool isCommand = _commandService.IsCommand(notACommand);
            Assert.False(isCommand);
        }
    }
}
