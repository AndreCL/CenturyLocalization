using CenturyLocalization;
using System.Globalization;
using Xunit;

namespace LocalizationTests;

public class EndScreenTests
{
    [Fact]
    public void GetEndScreenString_Winner_ReturnsCorrectValue()
    {
        var local = new Localization
        {
            CurrentLanguage = new CultureInfo("en-US")
        };

        var result = local.GetText("winner");

        Assert.Equal("Winner", result);
    }

    [Fact]
    public void GetEndScreenString_Winner_Spanish_ReturnsCorrectValue()
    {
        var local = new Localization
        {
            CurrentLanguage = new CultureInfo("es-ES")
        };

        var result = local.GetText("winner");

        Assert.Equal("Ganador", result);
    }
}
