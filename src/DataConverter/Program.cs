using CommandLine;
using DataConverter.Console.CommandLineOptions;
using System;
using System.Collections.Generic;

namespace DataConverter.Console
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var parser = new Parser(x => x.HelpWriter = System.Console.Out);

            var result = parser.ParseArguments<Options>(args)
                .MapResult(RunConversion, HandleErrors);

            return result;
        }

        private static int RunConversion(Options options)
        {
            try
            {
                var converter = new DataConverter(
                    new CommandLineOptionsValidator(),
                    new ConversionOptionsBuilder());

                converter.RunConversion(options);

                return 0;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());

                return 1;
            }
        }

        private static int HandleErrors(IEnumerable<Error> errors)
        {
            return 1;
        }
    }
}
