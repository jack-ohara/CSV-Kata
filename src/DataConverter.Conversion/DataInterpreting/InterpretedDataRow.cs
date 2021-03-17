using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataConverter.Conversion.DataInterpreting
{
    public class InterpretedDataRow
    {
        public IDictionary<string, object> RowData { get; }

        public InterpretedDataRow()
        {
            RowData = new Dictionary<string, object>();
        }

        public void AddValue(string key, string value)
        {
            RowData[key] = value;
        }

        public InterpretedDataNestedValue GetNested(string key)
        {
            if (RowData.ContainsKey(key))
            {
                return RowData[key] as InterpretedDataNestedValue;
            }

            var nestedValue = new InterpretedDataNestedValue();

            RowData[key] = nestedValue;

            return nestedValue;
        }

        public object this[string index]
        {
            get => RowData[index];
        }
    }
}