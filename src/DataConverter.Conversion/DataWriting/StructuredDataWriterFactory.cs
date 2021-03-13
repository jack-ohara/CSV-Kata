using System;

namespace DataConverter.Conversion.DataWriting
{
    public class StructuredDataWriterFactory
    {
        public IStructuredDataWriter GetWriter(StructuredDataFormat dataFormat)
        {
            switch (dataFormat)
            {
                case StructuredDataFormat.Json:
                    return new JsonDataWriter();

                default:
                    throw new ArgumentException(
                        $"Writing to the format ${dataFormat} is not supported.",
                        nameof(dataFormat));
            }
        }
    }
}
