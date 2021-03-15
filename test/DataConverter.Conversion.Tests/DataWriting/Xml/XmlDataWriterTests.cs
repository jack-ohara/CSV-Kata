using DataConverter.Conversion.DataWriting.Json;
using DataConverter.Conversion.DataWriting.Xml;
using Xunit;

namespace DataConverter.Conversion.Tests.DataWriting.Xml
{
    public class XmlDataWriterTests
    {
        [Fact]
        public void Returns_empty_string_if_input_is_null()
        {
            var sut = new XmlDataWriter(new JsonDataWriter(), new XmlConversionOptions());

            var result = sut.WriteData(null);

            Assert.Equal(StructuredDataFormat.Xml, result.Format);
            Assert.Equal(string.Empty, result.Contents);
        }
    }
}
