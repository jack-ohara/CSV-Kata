using DataConverter.Conversion;
using DataConverter.Conversion.DataInterpreting;
using DataConverter.Conversion.DataWriting;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataConverter.Tests
{
    public class ConversionTests
    {
        [InlineData("./input.csv", "./expected.json")]
        [InlineData("./multi-row.csv", "./multi-row-expected.json")]
        [Theory]
        public async Task Converts_CSV_to_expected_json(string inputFilePath, string expectedFilePath)
        {
            var options = new StructuredDataConversionOptions
            {
                InputData = new StructuredData
                {
                    Format = StructuredDataFormat.Csv,
                    Contents = await File.ReadAllTextAsync(inputFilePath)
                },
                TargetFormat = StructuredDataFormat.Json
            };

            var converter = new StructuredDataConverter(
                new StructuredDataInterpreterFactory(),
                new StructuredDataWriterFactory());

            var result = converter.Convert(options);

            Assert.Equal(StructuredDataFormat.Json, result.Format);
            Assert.Equal(await File.ReadAllTextAsync(expectedFilePath), result.Contents);
        }

        [InlineData("./input.csv", "./expected.xml")]
        [InlineData("./multi-row.csv", "./multi-row-expected.xml")]
        [Theory]
        public async Task Converts_CSV_to_expected_xml(string inputFilePath, string expectedFilePath)
        {
            var options = new StructuredDataConversionOptions
            {
                InputData = new StructuredData
                {
                    Format = StructuredDataFormat.Csv,
                    Contents = await File.ReadAllTextAsync(inputFilePath)
                },
                TargetFormat = StructuredDataFormat.Xml
            };

            var converter = new StructuredDataConverter(
                new StructuredDataInterpreterFactory(),
                new StructuredDataWriterFactory());

            var result = converter.Convert(options);

            Assert.Equal(StructuredDataFormat.Xml, result.Format);
            Assert.Equal(await File.ReadAllTextAsync(expectedFilePath), result.Contents);
        }
    }
}
