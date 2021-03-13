using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    internal class CsvDataInterpreter : IStructuredDataInterpreter
    {
        public IEnumerable<dynamic> Interpret(string contents)
        {
            return new List<dynamic>();
        }
    }
}