using System.Text.Json;

namespace DataConverter.Conversion.DataWriting.Json
{
    public class JsonDataWriter : IStructuredDataWriter
    {
        public StructuredData WriteData(object interpretedData)
        {
            if (interpretedData is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Json,
                    Contents = string.Empty
                };
            }

            var options = new JsonSerializerOptions { WriteIndented = true };

            return new StructuredData
            {
                Format = StructuredDataFormat.Json,
                Contents = JsonSerializer.Serialize(interpretedData, options)
            };
        }
    }
}