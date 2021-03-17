using DataConverter.Conversion.DataWriting.Json;
using DataConverter.Conversion.DataWriting.Xml;
using System;

namespace DataConverter.Conversion.DataWriting
{
    public class StructuredDataWriterFactory
    {
        public IStructuredDataWriter GetWriter(
            StructuredDataFormat dataFormat,
            StructuredDataConversionOptions conversionOptions)
        {
            switch (dataFormat)
            {
                case StructuredDataFormat.Json:
                    return new JsonDataWriter();

                case StructuredDataFormat.Xml:
                    return new XmlDataWriter(conversionOptions.XmlOptions);

                default:
                    throw new ArgumentException(
                        $"Writing to the format ${dataFormat} is not supported.",
                        nameof(dataFormat));
            }
        }
    }
}
