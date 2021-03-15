using System;
using System.Runtime.Serialization;

namespace DataConverter.Conversion.DataInterpreting.Csv
{
    public class InvalidCsvDataException : Exception
    {
        public InvalidCsvDataException()
        {
        }

        public InvalidCsvDataException(string message) : base(message)
        {
        }

        public InvalidCsvDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCsvDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
