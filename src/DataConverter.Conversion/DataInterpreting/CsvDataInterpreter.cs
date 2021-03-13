using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public class CsvDataInterpreter : IStructuredDataInterpreter
    {
        public IEnumerable<dynamic> Interpret(string csvData)
        {
            return new List<dynamic>();
        }
    }
}