using DataConverter.CommandLineOptions;
using DataConverter.Conversion;
using DataConverter.Conversion.DataInterpreting;
using DataConverter.Conversion.DataWriting;
using FluentValidation;
using System;
using System.IO;

namespace DataConverter
{
    public class DataConverter
    {
        private readonly CommandLineOptionsValidator _optionsValidator;
        private readonly ConversionOptionsBuilder _conversionOptionsBuilder;

        public DataConverter(
            CommandLineOptionsValidator validator,
            ConversionOptionsBuilder conversionOptionsBuilder)
        {
            _optionsValidator = validator;
            _conversionOptionsBuilder = conversionOptionsBuilder;
        }

        public void RunConversion(Options commandLineOptions)
        {
            var validationResult = _optionsValidator.Validate(commandLineOptions);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var fileContents = File.ReadAllText(commandLineOptions.CsvInputFileName);

            var conversionOptions = _conversionOptionsBuilder
                .WithInputFormat(StructuredDataFormat.Csv)
                .WithInputContents(fileContents)
                .WithTargetFormat((StructuredDataFormat)Enum.Parse(typeof(StructuredDataFormat), commandLineOptions.TargetFormat, true))
                .Build();

            var converter = new StructuredDataConverter(
                new StructuredDataInterpreterFactory(),
                new StructuredDataWriterFactory());

            var result = converter.Convert(conversionOptions);

            File.WriteAllText(commandLineOptions.OutputFile, result.Contents);
        }
    }
}
