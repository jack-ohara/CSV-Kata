using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataConverter.Conversion.DataInterpreting
{
    public class InterpretedDataNestedValueConverter : JsonConverter<InterpretedDataNestedValue>
    {
        public override InterpretedDataNestedValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, InterpretedDataNestedValue value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            JsonSerializer.Serialize(writer, value.Data, options);

            writer.WriteEndObject();
        }
    }
}