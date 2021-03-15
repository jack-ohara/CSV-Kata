using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DataConverter.Conversion.DataWriting
{
    public class XmlDataWriter : IStructuredDataWriter
    {
        private readonly JsonDataWriter _jsonDataWriter;
        public XmlDataWriter(JsonDataWriter jsonDataWriter)
        {
            _jsonDataWriter = jsonDataWriter;
        }

        public StructuredData WriteData(object interpretedData)
        {
            var dataToConvert = interpretedData is IEnumerable<IDictionary<string, object>> dataRows
                ? new Dictionary<string, object> { ["row"] = dataRows } :
                interpretedData;

            var json = _jsonDataWriter.WriteData(dataToConvert);

            var xmlDoc =  JsonConvert.DeserializeXmlNode(json.Contents, "root");

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
