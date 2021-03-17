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

            row.AddValue("nested_key1", "value1");
            row.AddValue("nested_nested2_key2", "value2");

            Assert.Equal("value1", row["nested"].AsDictionary()["key1"]);
            Assert.Equal("value2", row["nested"].AsDictionary()["nested2"].AsDictionary()["key2"]);
        }
    }
}
