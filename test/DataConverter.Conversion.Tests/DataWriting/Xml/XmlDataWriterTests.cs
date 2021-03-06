using DataConverter.Conversion.DataInterpreting;
using DataConverter.Conversion.DataWriting.Json;
using DataConverter.Conversion.DataWriting.Xml;
using System.Collections.Generic;
using Xunit;

namespace DataConverter.Conversion.Tests.DataWriting.Xml
{
    public class XmlDataWriterTests
    {
        [Fact]
        public void Returns_empty_string_if_input_is_null()
        {
            var sut = new XmlDataWriter(new XmlConversionOptions());

            InterpretedDataRow data = null;

            var result = sut.WriteData(data);

            Assert.Equal(StructuredDataFormat.Xml, result.Format);
            Assert.Equal(string.Empty, result.Contents);
        }
        [Fact]
        public void Returns_empty_string_if_input_is_null_for_multiple_rows()
        {
            var sut = new XmlDataWriter(new XmlConversionOptions());

            IEnumerable<InterpretedDataRow> data = null;

            var result = sut.WriteData(data);

            Assert.Equal(StructuredDataFormat.Xml, result.Format);
            Assert.Equal(string.Empty, result.Contents);
        }
    }
}
