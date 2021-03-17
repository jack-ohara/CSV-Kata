using DataConverter.Conversion.DataInterpreting;
using System.Collections.Generic;

namespace DataConverter.Conversion.DataWriting
{
    public interface IStructuredDataWriter
    {
        StructuredData WriteData(IEnumerable<InterpretedDataRow> interpretedRows);
        StructuredData WriteData(InterpretedDataRow interpretedRow);
    }
}