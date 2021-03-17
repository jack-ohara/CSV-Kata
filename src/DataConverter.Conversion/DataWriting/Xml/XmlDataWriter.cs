using DataConverter.Conversion.DataInterpreting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace DataConverter.Conversion.DataWriting.Xml
{
    public class XmlDataWriter : IStructuredDataWriter
    {
        private readonly XmlConversionOptions _options;

        public XmlDataWriter(XmlConversionOptions options)
        {
            _options = options;
        }

        public StructuredData WriteData(IEnumerable<InterpretedDataRow> interpretedRows)
        {
            if (interpretedRows is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Xml,
                    Contents = string.Empty
                };
            }

            var data = new Dictionary<string, object> 
            {
                [_options.RowNodeName] = interpretedRows.Select(x => x.RowData)
            };

            return WriteObject(data);
        }

        public StructuredData WriteData(InterpretedDataRow interpretedRow)
        {
            if (interpretedRow is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Xml,
                    Contents = string.Empty
                };
            }

            return WriteObject(interpretedRow.RowData);
        }

        private StructuredData WriteObject(object data)
        {
            if (data is null)
            {
                return new StructuredData
                {
                    Format = StructuredDataFormat.Xml,
                    Contents = string.Empty
                };
            }

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true, MaxDepth = 0 };

            var json = System.Text.Json.JsonSerializer.Serialize(data, jsonOptions);

            var xmlDoc = JsonConvert.DeserializeXmlNode(json, _options.RootNodeName);

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
