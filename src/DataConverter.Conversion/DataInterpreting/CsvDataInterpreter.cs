using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Conversion.DataInterpreting
{
    public class CsvDataInterpreter : IStructuredDataInterpreter
    {
        public IEnumerable<IDictionary<string, object>> Interpret(string csvData)
        {
            if (csvData is null)
            {
                throw new ArgumentNullException(nameof(csvData));
            }

            var lines = csvData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var headers = lines[0].Split(',');
            var rows = lines[1..].Select(x => x.Split(',')).ToList();

            if (rows.Count == 0)
            {
                rows.Add(new string[headers.Length]);
            }

            return rows.Select((row, idx) => GetDataRow(headers, row, idx));
        }

        private static IDictionary<string, object> GetDataRow(string[] headers, string[] row, int rowIndex)
        {
            if (row.Length != headers.Length)
            {
                throw new InvalidCsvDataException(
                    $"Row {rowIndex + 1} of the csv data contains {row.Length} value{(row.Length != 1 ? "s" : "")} but {headers.Length} were expected");
            }

            var result = new Dictionary<string, object>();

            for (var i = 0; i < headers.Length; i++)
            {
                var headerSegments = headers[i].Split('_');

                if (headerSegments.Length > 1)
                {
                    var propertyName = headerSegments[^1];
                    var nestingProperties = headerSegments[..^1];

                    var previousLevelDictionary = result;

                    foreach(var property in nestingProperties)
                    {
                        if (!previousLevelDictionary.ContainsKey(property))
                        {
                            previousLevelDictionary[property] = new Dictionary<string, object>();
                        }

                        previousLevelDictionary = previousLevelDictionary[property].AsDictionary();
                    }

                    previousLevelDictionary[propertyName] = row[i];
                }
                else
                {
                    result[headerSegments[0]] = row[i];
                }
            }

            return result;
        }
    }
}