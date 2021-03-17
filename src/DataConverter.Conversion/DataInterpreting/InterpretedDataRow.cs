using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public class InterpretedDataRow
    {
        private readonly IDictionary<string, object> _rowData;

        public InterpretedDataRow()
        {
            _rowData = new Dictionary<string, object>();
        }

        public void AddValue(string key, string value)
        {
            _rowData[key] = value;
        }

        public InterpretedDataNestedValue GetNested(string key)
        {
            if (_rowData.ContainsKey(key))
            {
                return _rowData[key] as InterpretedDataNestedValue;
            }

            var nestedValue = new InterpretedDataNestedValue();

            _rowData[key] = nestedValue;

            return nestedValue;
        }

        public object this[string index]
        {
            get => _rowData[index];
        }

        private void AddNestedValue(string key, string value)
        {
            
        }
    }
}