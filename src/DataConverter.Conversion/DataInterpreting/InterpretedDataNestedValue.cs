using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public class InterpretedDataNestedValue
    {
        private readonly IDictionary<string, object> _nestedData;

        public InterpretedDataNestedValue()
        {
            _nestedData = new Dictionary<string, object>();
        }

        public void AddValue(string key, string value)
        {
            _nestedData[key] = value;
        }

        public InterpretedDataNestedValue GetNested(string key)
        {
            if (_nestedData.ContainsKey(key))
            {
                return _nestedData[key] as InterpretedDataNestedValue;
            }

            var nestedValue = new InterpretedDataNestedValue();

            _nestedData[key] = nestedValue;

            return nestedValue;
        }

        public object this[string index]
        {
            get => _nestedData[index];
        }
    }
}