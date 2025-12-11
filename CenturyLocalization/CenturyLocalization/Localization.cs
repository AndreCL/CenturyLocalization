using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LocalizationTests")]
namespace CenturyLocalization
{
	public class Localization
	{
		public CultureInfo CurrentLanguage { get; set; }

		private readonly ResourceManager _resourceManager;
        private readonly ResourceManager _countryNamesResourceManager;
		private readonly ResourceManager _actionsResourceManager;

        public Localization()
		{
			_resourceManager = Texts.ResourceManager;
            _countryNamesResourceManager = CountryNames.CountryNames.ResourceManager;
			_actionsResourceManager = Actions.Actions.ResourceManager;
            CurrentLanguage = CultureInfo.CurrentUICulture;
		}

		public string GetText(string name)
		{
			return GetText(name, CurrentLanguage);
        }

        public string GetText(string name, CultureInfo cultureInfo)
        {
            return _resourceManager.GetString(name, cultureInfo) ??
                _countryNamesResourceManager.GetString(name, cultureInfo) ??
                _actionsResourceManager.GetString(name, cultureInfo) ??
                _resourceManager.GetString("error_string_not_found", cultureInfo);
        }
    }
}
