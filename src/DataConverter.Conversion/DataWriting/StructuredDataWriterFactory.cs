using DataConverter.Conversion.DataWriting.Json;
using DataConverter.Conversion.DataWriting.Xml;
using System;

namespace DataConverter.Conversion.DataWriting
{
    public class StructuredDataWriterFactory
    {
        public IStructuredDataWriter GetWriter(
            StructuredDataConversionOptions conversionOptions)
        {
            switch (conversionOptions.TargetFormat)
            {
                case StructuredDataFormat.Json:
                    return new JsonDataWriter();

                case StructuredDataFormat.Xml:
                    return new XmlDataWriter(conversionOptions.XmlOptions);

                default:
                    throw new ArgumentException(
                        $"Writing to the format ${conversionOptions.TargetFormat} is not supported.",
                        nameof(conversionOptions));
            }
        }
    }
}
