using CommandLine;
using System;
using System.Collections.Generic;

namespace DataConverter
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var parser = new Parser(x => x.HelpWriter = Console.Out);

            var result = parser.ParseArguments<CommandLineOptions>(args)
                .MapResult(RunConversion, HandleErrors);

            return result;
        }

        private static int RunConversion(CommandLineOptions options)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return 1;
            }
        }

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return 1;
        }
    }

    class CommandLineOptions
    {
        [Option('c', "csvInputFile", Required = true, HelpText = "Specifies the CSV input file to convert.")]
        public string CsvInputFileName { get; set; }

        [Option('f', "format", Default = "json", HelpText = "Specifies the target format to convert to.")]
        public string TargetFormat { get; set; }

    }
}
