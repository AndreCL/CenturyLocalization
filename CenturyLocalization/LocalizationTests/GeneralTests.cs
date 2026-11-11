using CenturyLocalization;
using System.Linq;
using Xunit;

namespace LocalizationTests
{
	public class GeneralTests
	{
		[Fact]
		public void CultureInfoIsCorrect()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            Assert.Equal("en-US", local.CurrentLanguage.Name);
		}

		[Fact]
		public void GettingExistingStringAndDefaultEnglish()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("unittest_onlyen");

			Assert.Equal("Ok", result);
		}

		[Fact]
		public void GetNonExistingFileLocaleReturnsEnglish()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("smj-SE")
            };

            var result = local.GetText("unittest_onlyen");

			Assert.Equal("smj-SE", local.CurrentLanguage.Name);
			Assert.Equal("Ok", result);
		}

		[Fact]
		public void GetNonExistingStringReturnsNull()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("unittest_DOESNTEXIST");

			Assert.Equal("Text not found", result);
		}

		[Fact]
		public void GetOtherLocaleString()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("es-ES")
            };

            var result = local.GetText("unittest_both");

			Assert.Equal("Ok_es", result);
		}

		[Fact]
		public void GetOtherLocaleStringDifferentCountry()
		{
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("es-AR")
            };

            var result = local.GetText("unittest_both");

			Assert.Equal("Ok_es", result);
		}

        [Fact]
        public void GettingCountryNameInEnglish()
        {
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("acre");

            Assert.Equal("Acre", result);
        }

        [Fact]
        public void GettingActionInEnglish()
        {
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("add_soldiers");

            Assert.Equal("Add soldiers", result);
        }

        [Fact]
        public void GettingPopulationAttributes()
        {
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("male");

            Assert.Equal("Male", result);
        }

        [Fact]
        public void GettingGameSettingsInEnglishAndFrench()
        {
            var local = new Localization();

            Assert.Equal("Settings", local.GetText("settings", new System.Globalization.CultureInfo("en")));
            Assert.Equal("Sélectionner la langue", local.GetText("select_language", new System.Globalization.CultureInfo("fr")));
            Assert.Equal("1 semaine", local.GetText("settings_time_1_week", new System.Globalization.CultureInfo("fr")));
            Assert.Equal("{choice} (Par défaut)", local.GetText("settings_default_choice", new System.Globalization.CultureInfo("fr")));
        }

        [Fact]
        public void GettingWarnamesAttributes()
        {
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetText("acre_war");

            Assert.Equal("The Acre War", result);
        }

        [Fact]
        public void GetAllCountryNamesReturnsAtLeast81Items()
        {
            var local = new Localization
            {
                CurrentLanguage = new System.Globalization.CultureInfo("en-US")
            };

            var result = local.GetAllCountryNames();

            Assert.True(result.Count() >= 81);
        }
    }
}
