using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataConverter.Console.Tests
{
    public class ConsoleE2ETests : IAsyncLifetime
    {
        private static readonly string _jsonOutputFilePath = "./output.json";
        private static readonly string _xmlOutputFilePath = "./output.json";

        public Task DisposeAsync()
        {
            File.Delete(_jsonOutputFilePath);
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            File.Delete(_jsonOutputFilePath);
            return Task.CompletedTask;
        }

        [InlineData("./input.csv", "./expected.json")]
        [InlineData("./multi-row.csv", "./multi-row-expected.json")]
        [Theory]
        public async Task Converts_CSV_to_expected_json(string inputFilePath, string expectedFilePath)
        {
            var args = new[] { "-c", inputFilePath, "-f", "json", "-o", _jsonOutputFilePath };

            Program.Main(args);

            Assert.True(File.Exists(_jsonOutputFilePath));

            var outputFileContents = await File.ReadAllTextAsync(_jsonOutputFilePath);
            Assert.Equal(await File.ReadAllTextAsync(expectedFilePath), outputFileContents);
        }

        [InlineData("./input.csv", "./expected.xml")]
        [InlineData("./multi-row.csv", "./multi-row-expected.xml")]
        [Theory]
        public async Task Converts_CSV_to_expected_xml(string inputFilePath, string expectedFilePath)
        {
            var args = new[] { "-c", inputFilePath, "-f", "xml", "-o", _xmlOutputFilePath};

            Program.Main(args);

            Assert.True(File.Exists(_xmlOutputFilePath));

            var outputFileContents = await File.ReadAllTextAsync(_xmlOutputFilePath);
            Assert.Equal(await File.ReadAllTextAsync(expectedFilePath), outputFileContents);
        }

        [Fact]
        public async Task Uses_custom_xml_field_names_when_supplied()
        {
            var args = new[] 
            {
                "-c", "./multi-row.csv",
                "-f", "xml",
                "-o", _xmlOutputFilePath,
                "--xmlRootName", "clients",
                "--xmlRowName", "client"
            };

            Program.Main(args);

            Assert.True(File.Exists(_xmlOutputFilePath));

            var outputFileContents = await File.ReadAllTextAsync(_xmlOutputFilePath);
            Assert.Equal(await File.ReadAllTextAsync("./expected-custom-fields.xml"), outputFileContents);
        }
    }
}
