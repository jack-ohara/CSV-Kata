using CommandLine;

namespace DataConverter.CommandLineOptions
{
    public class Options
    {
        [Option('c', "csvInputFile", Required = true, HelpText = "Specifies the CSV input file to convert.")]
        public string CsvInputFileName { get; set; }

        [Option('f', "format", Default = "json", HelpText = "Specifies the target format to convert to.")]
        public string TargetFormat { get; set; }

        [Option('o', "outputFile", Required = true, HelpText = "Specifies the file to write the output to. Will be created if it does not exist")]
        public string OutputFile { get; set; }
    }
}
