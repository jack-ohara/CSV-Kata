using CommandLine;
using DataConverter.CommandLineOptions;
using System;
using System.Collections.Generic;

namespace DataConverter
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var parser = new Parser(x => x.HelpWriter = Console.Out);

            var result = parser.ParseArguments<Options>(args)
                .MapResult(RunConversion, HandleErrors);

            return result;
        }

        private static int RunConversion(Options options)
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
}
