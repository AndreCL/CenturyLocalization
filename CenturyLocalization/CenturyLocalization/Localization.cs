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



        public IEnumerable<CultureInfo> GetAvailableCultures(string neutralCultureName = "en")
        {

            var managers = new[]
            {
                _resourceManager,
                _countryNamesResourceManager,
                _actionsResourceManager,
                _populationAttributesManager,
                _warNamesManager
            };

            // Get all framework-known cultures and probe which ones actually have resources.
            // We exclude invariant at this stage and add the neutral explicitly later.
            var all = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                 .Where(c => c != CultureInfo.InvariantCulture);

            var supported = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var culture in all)
            {
                foreach (var rm in managers)
                {
                    try
                    {
                        // tryParents: false ensures we only count cultures with actual satellite resources,
                        // not those satisfied by fallback.
                        var set = rm.GetResourceSet(culture, createIfNotExists: true, tryParents: false);
                        if (set != null)
                        {
                            supported.Add(culture.Name);
                            break; // This culture is supported by at least one RM
                        }
                    }
                    catch
                    {
                        // Ignore probing errors; continue checking other cultures.
                    }
                }
            }

            // Ensure your neutral language is always present instead of invariant
            var neutral = new CultureInfo(neutralCultureName);
            supported.Add(neutral.Name);

            // Return as CultureInfo list; order by NativeName for nicer UX
            return supported
                .Select(name => new CultureInfo(name))
                .OrderBy(c => c.NativeName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
        }
    }
}
