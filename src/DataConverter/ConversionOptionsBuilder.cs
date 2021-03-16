using DataConverter.Conversion;
using DataConverter.Conversion.DataWriting.Xml;

namespace DataConverter.Console
{
    public class ConversionOptionsBuilder
    {
        private readonly StructuredDataConversionOptions _options;

        public ConversionOptionsBuilder()
        {
            _options = new StructuredDataConversionOptions
            {
                InputData = new StructuredData(),
                XmlOptions = new XmlConversionOptions()
            };
        }

        public ConversionOptionsBuilder WithInputFormat(StructuredDataFormat format)
        {
            _options.InputData.Format = format;
            return this;
        }

        public ConversionOptionsBuilder WithInputContents(string contents)
        {
            _options.InputData.Contents = contents;
            return this;
        }

        public ConversionOptionsBuilder WithTargetFormat(StructuredDataFormat format)
        {
            _options.TargetFormat = format;
            return this;
        }

        public ConversionOptionsBuilder WithXmlRootName(string xmlRootName)
        {
            _options.XmlOptions.RootNodeName = xmlRootName;
            return this;
        }

        public ConversionOptionsBuilder WithXmlRowName(string xmlRowName)
        {
            _options.XmlOptions.RowNodeName = xmlRowName;
            return this;
        }

        public StructuredDataConversionOptions Build()
        {
            return _options;
        }
    }
}