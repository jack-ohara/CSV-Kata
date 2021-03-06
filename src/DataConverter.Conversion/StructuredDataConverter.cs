using DataConverter.Conversion.DataInterpreting;
using DataConverter.Conversion.DataWriting;
using System.Linq;

namespace DataConverter.Conversion
{
    public class StructuredDataConverter
    {
        private readonly StructuredDataInterpreterFactory _dataInterpreterFactory;
        private readonly StructuredDataWriterFactory _dataWriterFactory;

        public StructuredDataConverter(
            StructuredDataInterpreterFactory dataInterpreterFactory,
            StructuredDataWriterFactory dataWriterFactory)
        {
            _dataInterpreterFactory = dataInterpreterFactory;
            _dataWriterFactory = dataWriterFactory;
        }

        public StructuredData Convert(StructuredDataConversionOptions conversionOptions)
        {
            var interpreter = _dataInterpreterFactory.GetInterpreter(conversionOptions.InputData.Format);

            var interpretedData = interpreter.Interpret(conversionOptions.InputData.Contents);

            var writer = _dataWriterFactory.GetWriter(conversionOptions);

            return interpretedData.Count() == 1 ?
                writer.WriteData(interpretedData.First()) :
                writer.WriteData(interpretedData);
        }
    }
}
