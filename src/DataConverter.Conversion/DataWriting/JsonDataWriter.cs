using System.Text.Json;

namespace DataConverter.Conversion.DataWriting
{
    public class JsonDataWriter : IStructuredDataWriter
    {
        public StructuredData WriteData(object interpretedData)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            return 
                new StructuredData
                {
                    Format = StructuredDataFormat.Json,
                    Contents = JsonSerializer.Serialize(interpretedData, options)
                };
        }
    }
}