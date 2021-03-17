using DataConverter.Conversion.DataInterpreting;
using Xunit;

namespace DataConverter.Conversion.Tests.DataInterpreting
{
    public class InterpretedDataRowTests
    {
        [Fact]
        public void Creates_expected_dictionary_with_no_nested_keys()
        {
            var row = new InterpretedDataRow();

            row.AddValue("key1", "value1");
            row.AddValue("key2", "value2");

            Assert.Equal("value1", row["key1"]);
            Assert.Equal("value2", row["key2"]);
        }

        [Fact]
        public void Creates_expected_dictionary_with_nested_keys()
        {
            var row = new InterpretedDataRow();

            row.AddValue("key1", "value1");

            var nested = row.GetNested("nested");
            nested.AddValue("key2", "nestedValue1");

            var inner = nested.GetNested("nested1");
            inner.AddValue("key3", "nestedValue2");

            Assert.Equal("value1", row["key1"]);
            Assert.Equal("nestedValue1", row.GetNested("nested")["key2"]);
            Assert.Equal("nestedValue2", row.GetNested("nested").GetNested("nested1")["key3"]);
        }
    }
}
