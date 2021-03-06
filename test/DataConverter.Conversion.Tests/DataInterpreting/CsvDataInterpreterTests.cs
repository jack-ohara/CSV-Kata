using DataConverter.Conversion.DataInterpreting.Csv;
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
            var resultRow = result[0];
            Assert.Equal("value1", resultRow["property1"]);
            Assert.Equal("value2", resultRow["property2"]);
            Assert.Equal("nested_value1", resultRow.GetNested("nested")["property1"]);
            Assert.Equal("nested_value2", resultRow.GetNested("nested").GetNested("nested1")["property2"]);
        }

        [InlineData("value1,value2", 2)]
        [InlineData("value1,value2,value3,value4", 4)]
        [Theory]
        public void Throws_an_error_when_a_row_has_a_mismatched_number_of_values(string dataRow, int valueCount)
        {
            var csvData = "property1,property2,property3\n" + dataRow;

            var interpreter = new CsvDataInterpreter();

            var ex = Assert.Throws<InvalidCsvDataException>(() => interpreter.Interpret(csvData).ToList());
            Assert.Equal($"Row 1 of the csv data contains {valueCount} value{(valueCount != 1 ? "s" : "")} but 3 were expected", ex.Message);
        }

        [Fact]
        public void Enters_null_values_when_no_data_is_supplied()
        {
            var csvData = "property1,property2,nested_property3";

            var interpreter = new CsvDataInterpreter();

            var result = interpreter.Interpret(csvData).ToList();

            Assert.Single(result);

            var resultRow = result[0];
            Assert.Null(resultRow["property1"]);
            Assert.Null(resultRow["property2"]);
            Assert.Null(resultRow.GetNested("nested")["property3"]);
        }

        [InlineData("value1,,value3", "value1", "", "value3")]
        [InlineData(",,", "", "", "")]
        [Theory]
        public void Enters_empty_strings_when_no_value_is_supplied_for_a_property(string dataRow, params string[] expectedValues)
        {
            var csvData = "property1,property2,nested_property3\n" + dataRow;

            var interpreter = new CsvDataInterpreter();

            var result = interpreter.Interpret(csvData).ToList();

            Assert.Single(result);

            var resultRow = result[0];
            Assert.Equal(expectedValues[0], resultRow["property1"]);
            Assert.Equal(expectedValues[1], resultRow["property2"]);
            Assert.Equal(expectedValues[2], resultRow.GetNested("nested")["property3"]);
        }

        [Fact]
        public void Throws_exception_when_data_is_null()
        {
            var interpreter = new CsvDataInterpreter();

            var ex = Assert.Throws<ArgumentNullException>(() => interpreter.Interpret(null));
            Assert.Equal("csvData", ex.ParamName);
        }
    }
}
