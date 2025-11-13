using System.Globalization;
using System.Resources;

namespace CenturyLocalization
{
	public class Localization
	{
		public CultureInfo CurrentLanguage { get; set; }

		private readonly ResourceManager _resourceManager;
        private readonly ResourceManager _countryNamesResourceManager;

        public Localization()
		{
			_resourceManager = Texts.ResourceManager;
            _countryNamesResourceManager = CountryNames.CountryNames.ResourceManager;
            CurrentLanguage = CultureInfo.CurrentUICulture;
		}

		public string GetText(string name)
		{
			return _resourceManager.GetString(name, CurrentLanguage) ?? 
				_countryNamesResourceManager.GetString(name, CurrentLanguage) ??
                _resourceManager.GetString("error_string_not_found", CurrentLanguage);
		}

		
	}
}
