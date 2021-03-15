using DataConverter.Conversion.DataInterpreting.Csv;
using System;

namespace DataConverter.Conversion.DataInterpreting
{
    public class StructuredDataInterpreterFactory
    {
        public IStructuredDataInterpreter GetInterpreter(StructuredDataFormat format)
        {
            switch (format)
            {
                case StructuredDataFormat.Csv:
                    return new CsvDataInterpreter();

                default:
                    throw new ArgumentException(
                        $"Interpreting data in ${format} format is not supported.",
                        nameof(format));
            }
        }
    }
}
