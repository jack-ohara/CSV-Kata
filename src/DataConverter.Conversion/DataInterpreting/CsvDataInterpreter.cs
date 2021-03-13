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

            return rows.Select(row => GetDataRow(headers, row));
        }

        private static IDictionary<string, object> GetDataRow(string[] headers, string[] row)
        {
            var result = new Dictionary<string, object>();

            for (var i = 0; i < headers.Length; i++)
            {
                var headerSegments = headers[i].Split('_');

                if (headerSegments.Length > 1)
                {
                    if (result.ContainsKey(headerSegments[0]))
                    {
                        result[headerSegments[0]].AsDictionary()[headerSegments[1]] = row[i];
                    }
                    else
                    {
                        result[headerSegments[0]] = new Dictionary<string, object>
                        {
                            [headerSegments[1]] = row[i]
                        };
                    }
                }
                else
                {
                    result[headerSegments[0]] = row[i];
                }
            }

            return result;
        }
    }

    internal static class ObjectExtensions
    {
        public static Dictionary<string, object> AsDictionary(this object obj)
        {
            return (Dictionary<string, object>)obj;
        }
    }
}