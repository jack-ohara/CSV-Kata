using DataConverter.CommandLineOptions;
using FluentValidation.TestHelper;
using Xunit;

namespace DataConverter.Tests.CommandLineOptions
{
    public class CommandLineOptionsValidatorTests
    {
        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void Returns_an_error_when_csv_input_is_empty(string csvInput)
        {
            var options = new Options { CsvInputFileName = csvInput };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.CsvInputFileName)
                .WithErrorMessage("'csvInputFile' must not be empty.");
        }

        [Fact]
        public void Returns_an_error_when_specified_csv_file_does_not_exist()
        {
            var options = new Options { CsvInputFileName = "notHere.csv" };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.CsvInputFileName)
                .WithErrorMessage($"Unable to find input file: 'notHere.csv'. Please verify the file name and try again");
        }

        [Fact]
        public void Returns_an_error_when_the_format_is_not_supported()
        {
            var options = new Options { TargetFormat = "yaml" };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.TargetFormat)
                .WithErrorMessage($"The specified format 'yaml' is unrecognised or unsupported. Supported formats are: Json, Xml");
        }

        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void Returns_an_error_when_the_xml_root_name_is_empty(string xmlRootName)
        {
            var options = new Options { XmlRootName = xmlRootName };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.XmlRootName)
                .WithErrorMessage("'xmlRootName' must not be empty.");
        }

        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void Returns_an_error_when_the_xml_row_name_is_empty(string xmlRowName)
        {
            var options = new Options { XmlRowName = xmlRowName };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.XmlRowName)
                .WithErrorMessage("'xmlRowName' must not be empty.");
        }

        [Fact]
        public void Validation_is_successful_for_a_valid_config()
        {
            var options = new Options
            {
                CsvInputFileName = "./CommandLineOptions/exists.csv",
                TargetFormat = "json"
            };

            var result = new CommandLineOptionsValidator().TestValidate(options);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
