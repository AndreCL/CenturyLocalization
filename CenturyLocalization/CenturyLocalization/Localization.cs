using System;
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
        private readonly ResourceManager _populationAttributesManager;
        private readonly ResourceManager _warNamesManager;

        public Localization()
        {
            _resourceManager = Texts.ResourceManager;
            _countryNamesResourceManager = CountryNames.CountryNames.ResourceManager;
            _actionsResourceManager = Actions.Actions.ResourceManager;
            _populationAttributesManager = PopulationAttributes.PopulationAttributes.ResourceManager;
            _warNamesManager = WarNames.WarNames.ResourceManager;
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
                _populationAttributesManager.GetString(name, cultureInfo) ??
                _warNamesManager.GetString(name, cultureInfo) ??
                _resourceManager.GetString("error_string_not_found", cultureInfo);
        }



        // Hardcoded list of cultures that have satellite resource assemblies.
        // Update this list when adding or removing a language from the .resx files.
        private static readonly string[] _supportedCultureNames =
        [
            "en", "ar", "da", "de", "es", "id", "nl", "pt-BR", "pt-PT", "ru", "sk", "tr"
        ];

        public IEnumerable<CultureInfo> GetAvailableCultures(string neutralCultureName = "en")
        {
            return _supportedCultureNames
                .Select(name => new CultureInfo(name))
                .OrderBy(c => c.NativeName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
        }
    }
}
