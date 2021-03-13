using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataConverter.Tests
{
    public class ConversionTests
    {
        [Fact]
        public async Task Converts_CSV_to_expected_json()
        {
            var options = new ConverterOptions
            {
                InputFormat = StructuredDataFormat.Csv,
                InputContents = await File.ReadAllTextAsync("./input.csv"),
                TargetFormat = StructuredDataFormat.Json
            };

            var converter = new StructuredDataConverter();

            var result = converter.Convert(options);

            Assert.Equal(StructuredDataFormat.Json, result.Format);
            Assert.Equal(await File.ReadAllTextAsync("./expected.json"), result.Contents);
        }
    }
}
