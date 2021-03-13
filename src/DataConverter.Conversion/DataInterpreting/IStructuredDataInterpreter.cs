using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public interface IStructuredDataInterpreter
    {
        IEnumerable<IDictionary<string, object>> Interpret(string structuredData);
    }
}