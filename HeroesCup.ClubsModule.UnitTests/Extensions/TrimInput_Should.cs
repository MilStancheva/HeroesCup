using ClubsModule.Common;
using Xunit;

namespace HeroesCup.ClubsModule.UnitTests.Extensions
{
    public class TrimInput_Should
    {
        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Double_Quotes_Is_Passed()
        {
            var input = "\"inputToTrim\"";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Single_Quotes_Is_Passed()
        {
            var input = "'inputToTrim'";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Dots_Is_Passed()
        {
            var input = "..inputToTrim...";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Spaces_Is_Passed()
        {
            var input = " inputToTrim    ";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Special_Quotes_Is_Passed()
        {
            var input = "“inputToTrim”";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Special_Quotes_And_Spaces_Is_Passed()
        {
            var input = " “  inputToTrim ”  ";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Special_Quotes_Dots_And_Spaces_Is_Passed()
        {
            var input = " “  inputToTrim ” .. ";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Double_Quotes_And_Spaces_Is_Passed()
        {
            var input = " \"  inputToTrim \"  ";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_A_Trimmed_String_When_String_With_Double_Quotes_Dots_And_Spaces_Is_Passed()
        {
            var input = " \"  inputToTrim \".  ";
            var trimmedInput = input.TrimInput();
            var expected = "inputToTrim";

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_Empty_String_When_Input_Strinf_Is_Empty()
        {
            var input = "";
            var trimmedInput = input.TrimInput();
            var expected = string.Empty;

            Assert.Equal(expected, trimmedInput);
        }

        [Fact]
        public void Return_Empty_String_When_Input_Strinf_Is_White_Space()
        {
            var input = " ";
            var trimmedInput = input.TrimInput();
            var expected = string.Empty;

            Assert.Equal(expected, trimmedInput);
        }
    }
}
