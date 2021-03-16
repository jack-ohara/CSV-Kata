using DataConverter.Conversion;
using FluentValidation;
using System;
using System.IO;
using System.Linq;

namespace DataConverter.CommandLineOptions
{
    public class CommandLineOptionsValidator : AbstractValidator<Options>
    {
        private static string ValidFormats => string.Join(", ", Enum.GetNames(typeof(StructuredDataFormat))
            .Where(x => !x.Equals("csv", StringComparison.OrdinalIgnoreCase) ));

        public CommandLineOptionsValidator()
        {
            RuleFor(o => o.CsvInputFileName)
                .NotEmpty()
                .Must(c => File.Exists(c))
                .WithName("csvInputFile")
                .WithMessage(o => $"Unable to find input file: '{o.CsvInputFileName}'. Please verify the file name and try again");

            RuleFor(o => o.TargetFormat)
                .NotEmpty()
                .Must(f => Enum.TryParse(typeof(StructuredDataFormat), f, true, out _))
                .WithMessage(o => $"The specified format '{o.TargetFormat}' is unrecognised or unsupported. Supported formats are: {ValidFormats}");

            RuleFor(o => o.XmlRootName)
                .NotEmpty()
                .WithName("xmlRootName");

            RuleFor(o => o.XmlRowName)
                .NotEmpty()
                .WithName("xmlRowName");
        }
    }
}
