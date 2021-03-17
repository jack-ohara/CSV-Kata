using DataConverter.Conversion.DataInterpreting;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DataConverter.Conversion.DataWriting.Json
{
    public class JsonDataWriter : IStructuredDataWriter
    {
        public StructuredData WriteData(IEnumerable<InterpretedDataRow> interpretedRows)
        {
            var data = interpretedRows?.Select(x => x.RowData);

            return WriteObject(data);
        }

        public StructuredData WriteData(InterpretedDataRow interpretedRow)
        {
            return WriteObject(interpretedRow?.RowData);
        }

        private StructuredData WriteObject(object data)
        {
            if (data is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Json,
                    Contents = string.Empty
                };
            }

            var options = new JsonSerializerOptions { WriteIndented = true, MaxDepth = 0 };

            return new StructuredData
            {
                Format = StructuredDataFormat.Json,
                Contents = JsonSerializer.Serialize(data, options)
            };
        }
    }
}