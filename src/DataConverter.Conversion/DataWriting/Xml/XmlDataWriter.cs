using DataConverter.Conversion.DataWriting.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DataConverter.Conversion.DataWriting.Xml
{
    public class XmlDataWriter : IStructuredDataWriter
    {
        private readonly JsonDataWriter _jsonDataWriter;
        private readonly XmlConversionOptions _options;

        public XmlDataWriter(JsonDataWriter jsonDataWriter, XmlConversionOptions options)
        {
            _jsonDataWriter = jsonDataWriter;
            _options = options;
        }

        public StructuredData WriteData(object interpretedData)
        {
            if (interpretedData is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Xml,
                    Contents = string.Empty
                };
            }

            var dataToConvert = interpretedData is IEnumerable<IDictionary<string, object>> dataRows
                ? new Dictionary<string, object> { [_options.RowNodeName] = dataRows } :
                interpretedData;

            var json = _jsonDataWriter.WriteData(dataToConvert);

            var xmlDoc = JsonConvert.DeserializeXmlNode(json.Contents, _options.RootNodeName);

            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using var stringWriter = new StringWriter();
            using var xmlTextWriter = XmlWriter.Create(stringWriter, settings);

            xmlDoc.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            var xmlString = stringWriter.GetStringBuilder().ToString();

            return new StructuredData
            {
                Format = StructuredDataFormat.Xml,
                Contents = xmlString
            };
        }
    }
}
