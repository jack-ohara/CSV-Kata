using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataConverter.Tests
{
    public class ProgramTests : IAsyncLifetime
    {
        private TextWriter _consoleOutput;
        private StringBuilder _stringBuilder;

        public async Task DisposeAsync()
        {
            await _consoleOutput.DisposeAsync();
        }

        public Task InitializeAsync()
        {
            _stringBuilder = new StringBuilder();
            _consoleOutput = new StringWriter(_stringBuilder);

            Console.SetOut(_consoleOutput);

            return Task.CompletedTask;
        }

        [Fact]
        public void Writes_an_error_to_the_console_when_an_unrecognised_argument_is_supplied()
        {
            var args = new[] { "--test", "hello" };

            var result = Program.Main(args);
            var consoleOutput = GetConsoleOutput();

            Assert.Equal(1, result);
            Assert.Contains("Option 'test' is unknown", consoleOutput);
        }

        private string GetConsoleOutput()
        {
            return _stringBuilder.ToString();
        }
    }
}
