using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Conversion.DataInterpreting.Csv
{
    public class CsvDataInterpreter : IStructuredDataInterpreter
    {
        public IEnumerable<InterpretedDataRow> Interpret(string csvData)
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

        private static InterpretedDataRow GetDataRow(string[] headers, string[] row, int rowIndex)
        {
            if (row.Length != headers.Length)
            {
                throw new InvalidCsvDataException(
                    $"Row {rowIndex + 1} of the csv data contains {row.Length} value{(row.Length != 1 ? "s" : "")} but {headers.Length} were expected");
            }

            var result = new InterpretedDataRow();

            for (var i = 0; i < headers.Length; i++)
            {
                var headerSegments = headers[i].Split('_');

                if (headerSegments.Length > 1)
                {
                    var propertyName = headerSegments[^1];
                    var nestingProperties = headerSegments[..^1];

                    var nestedValue = result.GetNested(nestingProperties[0]);

                    foreach (var property in nestingProperties[1..])
                    {
                        nestedValue = nestedValue.GetNested(property);
                    }

                    nestedValue.AddValue(propertyName, row[i]);
                }
                else
                {
                    result.AddValue(headerSegments[0], row[i]);
                }
            }

            return result;
        }
    }
}