using DataConverter.Conversion.DataInterpreting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataConverter.Conversion.Tests.DataInterpreting
{
    public class CsvDataInterpreterTests
    {
        [Fact]
        public void Produces_expected_data_from_csv_string()
        {
            const string csvData =
                "property1,property2\n" +
                "value1-1,value2-1\n" +
                "value1-2,value2-2";

            var interpreter = new CsvDataInterpreter();

            var result = interpreter.Interpret(csvData).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("value1-1", result[0]["property1"]);
            Assert.Equal("value2-1", result[0]["property2"]);
            Assert.Equal("value1-2", result[1]["property1"]);
            Assert.Equal("value2-2", result[1]["property2"]);
        }

        [Fact]
        public void Produces_expected_data_from_csv_string_with_nested_properties()
        {
            const string csvData =
                "property1,property2,nested_property1,nested_property2\n" +
                "value1,value2,nested_value1,nested_value2";

            var interpreter = new CsvDataInterpreter();

            var result = interpreter.Interpret(csvData).ToList();

            Assert.Single(result);
            Assert.Equal("value1", result[0]["property1"]);
            Assert.Equal("value2", result[0]["property2"]);
            Assert.Equal("nested_value1", ((Dictionary<string, object>)result[0]["nested"])["property1"]);
            Assert.Equal("nested_value2", ((Dictionary<string, object>)result[0]["nested"])["property2"]);
        }

        // Test cases:
        // Row with now value for a property
        // Row with too few/too many values for the headers
    }
}
