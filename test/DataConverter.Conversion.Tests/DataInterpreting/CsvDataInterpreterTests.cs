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
    }
}
