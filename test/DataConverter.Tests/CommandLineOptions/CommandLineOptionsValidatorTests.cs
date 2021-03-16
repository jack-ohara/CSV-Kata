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

            var sut = new CommandLineOptionsValidator();

            var result = sut.TestValidate(options);

            result.ShouldHaveValidationErrorFor(o => o.CsvInputFileName)
                .WithErrorMessage("'csvInputFile' must not be empty.");
        }
    }
}
