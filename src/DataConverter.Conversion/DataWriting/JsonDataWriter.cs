using System.Collections.Generic;

namespace DataConverter.Conversion.DataWriting
{
    internal class JsonDataWriter : IStructuredDataWriter
    {
        public StructuredData WriteData(IEnumerable<dynamic> interpretedData)
        {
            return new StructuredData
            {
                Format = StructuredDataFormat.Json,
                Contents = string.Empty
            };
        }
    }
}