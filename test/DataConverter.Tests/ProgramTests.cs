using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataConverter.Console.Tests
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

            System.Console.SetOut(_consoleOutput);

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

        [Fact]
        public void Writes_an_error_to_the_console_when_the_csv_input_is_not_supplied()
        {
            var args = new[] { "-f", "json" };

            var result = Program.Main(args);
            var consoleOutput = GetConsoleOutput();

            Assert.Equal(1, result);
            Assert.Contains("Required option 'c, csvInputFile' is missing", consoleOutput);
        }

        [Fact]
        public void Writes_an_error_to_the_console_when_the_output_file_is_not_supplied()
        {
            var args = new[] { "-c", "input.csv" };

            var result = Program.Main(args);
            var consoleOutput = GetConsoleOutput();

            Assert.Equal(1, result);
            Assert.Contains("Required option 'o, outputFile' is missing", consoleOutput);
        }

        private string GetConsoleOutput()
        {
            return _stringBuilder.ToString();
        }
    }
}
