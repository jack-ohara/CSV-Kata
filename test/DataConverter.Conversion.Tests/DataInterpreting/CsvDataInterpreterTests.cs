using DataConverter.Conversion.DataInterpreting;
using System;
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
                "property1,property2,nested_property1,nested_nested1_property2\n" +
                "value1,value2,nested_value1,nested_value2";

            var interpreter = new CsvDataInterpreter();

            var result = interpreter.Interpret(csvData).ToList();

            Assert.Single(result);
            Assert.Equal("value1", result[0]["property1"]);
            Assert.Equal("value2", result[0]["property2"]);
            Assert.Equal("nested_value1", result[0]["nested"].AsDictionary()["property1"]);
            Assert.Equal("nested_value2", result[0]["nested"].AsDictionary()["nested1"].AsDictionary()["property2"]);
        }

        [InlineData("value1,value2", 2)]
        [InlineData("value1,value2,value3,value4", 4)]
        [InlineData("", 4)]
        [Theory]
        public void Throws_an_error_when_a_row_has_a_mismatched_number_of_values(string dataRow, int valueCount)
        {
            var csvData = "property1,property2,property3\n" + dataRow;

            var interpreter = new CsvDataInterpreter();

            var ex = Assert.Throws<InvalidCsvDataException>(() => interpreter.Interpret(csvData));
            Assert.Equal($"Row 1 of the csv data contains {valueCount} value{(valueCount != 1 ? "s" : "")} but 3 were expected", ex.Message);
        }

        // Test cases:
        // Row with no value for a property
        // Row with too few/too many values for the headers
    }
}
