using DataConverter.Conversion.DataInterpreting;
using DataConverter.Conversion.DataWriting.Json;
using System;
using Xunit;

namespace DataConverter.Conversion.Tests.DataWriting.Json
{
    public class JsonDataWriterTests
    {
        [Fact]
        public void Returns_empty_string_if_input_is_null()
        {
            var sut = new JsonDataWriter();

            InterpretedDataRow data = null;

            var result = sut.WriteData(data);

            Assert.Equal(StructuredDataFormat.Json, result.Format);
            Assert.Equal(string.Empty, result.Contents);
        }

        [Fact]
        public void Returns_expected_json_for_object()
        {
            var data = new InterpretedDataRow();

            data.AddValue("key1", "value1");
            var nested = data.GetNested("nested");
            nested.AddValue("key2", "value2");

            var newLine = Environment.NewLine;

            var expected = 
                $"{{{newLine}" +
                $"  \"key1\": \"value1\",{newLine}" +
                $"  \"nested\": {{{newLine}" +
                $"    \"key2\": \"value2\"{newLine}" +
                $"  }}{newLine}" +
                $"}}";

            var sut = new JsonDataWriter();

            var result = sut.WriteData(data);

            Assert.Equal(StructuredDataFormat.Json, result.Format);
            Assert.Equal(expected, result.Contents);
        }
    }
}
