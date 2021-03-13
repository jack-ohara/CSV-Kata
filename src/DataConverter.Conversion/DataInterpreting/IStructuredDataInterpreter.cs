using System.Collections.Generic;

namespace DataConverter.Conversion.DataInterpreting
{
    public interface IStructuredDataInterpreter
    {
        IEnumerable<dynamic> Interpret(string contents);
    }
}