using DataConverter.Conversion.DataWriting.Json;
using Xunit;

namespace DataConverter.Conversion.Tests.DataWriting.Json
{
    public class JsonDataWriterTests
    {
        [Fact]
        public void Returns_empty_string_if_input_is_null()
        {
            var sut = new JsonDataWriter();

            var result = sut.WriteData(null);

            Assert.Equal(StructuredDataFormat.Json, result.Format);
            Assert.Equal(string.Empty, result.Contents);
        }
    }
}
