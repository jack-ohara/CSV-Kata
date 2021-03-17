using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public interface IStructuredDataInterpreter
    {
        IEnumerable<InterpretedDataRow> Interpret(string structuredData);
    }
}