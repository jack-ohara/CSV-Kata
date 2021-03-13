using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Conversion.DataInterpreting
{
    public class CsvDataInterpreter : IStructuredDataInterpreter
    {
        public IEnumerable<IDictionary<string, object>> Interpret(string csvData)
        {
            var lines = csvData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var headers = lines[0].Split(',');
            var rows = lines[1..].Select(x => x.Split(','));

            return rows.Select(row => GetDynamicRow(headers, row));
        }

        private static IDictionary<string, object> GetDynamicRow(string[] headers, string[] row)
        {
            var result = new Dictionary<string, object>();

            for (var i = 0; i < headers.Length; i++)
            {
                result[headers[i]] = row[i];
            }

            return result;
        }
    }
}