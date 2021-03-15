namespace DataConverter.Conversion.DataWriting
{
    public interface IStructuredDataWriter
    {
        StructuredData WriteData(object interpretedData);
    }
}