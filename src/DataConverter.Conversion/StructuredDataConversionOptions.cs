using DataConverter.Conversion.DataWriting.Xml;

namespace DataConverter.Conversion
{
    public class StructuredDataConversionOptions
    {
        public StructuredData InputData { get; set; }
        public StructuredDataFormat TargetFormat { get; set; }
        public XmlConversionOptions XmlOptions { get; set; }
    }
}
