using CommandLine;

namespace DataConverter.Console.CommandLineOptions
{
    public class Options
    {
        [Option('c', "csvInputFile", Required = true, HelpText = "The CSV input file to convert.")]
        public string CsvInputFileName { get; set; }

        [Option('f', "format", Default = "json", HelpText = "The target format to convert to.")]
        public string TargetFormat { get; set; }

        [Option('o', "outputFile", Required = true, HelpText = "The file to write the output to. Will be created if it does not exist")]
        public string OutputFile { get; set; }

        [Option("xmlRootName", Default = "root", HelpText = "The root node name to use during xml conversion")]
        public string XmlRootName { get; set; }

        [Option("xmlRowName", Default = "row", HelpText = "The name to use for every row of data in the input")]
        public string XmlRowName { get; set; }
    }
}
