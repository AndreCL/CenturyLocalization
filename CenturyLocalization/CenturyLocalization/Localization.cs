using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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



        public IEnumerable<CultureInfo> GetAvailableCultures(string neutralCultureName = "en")
        {
            var assembly = typeof(Localization).Assembly;
            string baseDir = Path.GetDirectoryName(assembly.Location)!;

            var cultures = new List<CultureInfo>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var dir in Directory.GetDirectories(baseDir))
            {
                string folderName = Path.GetFileName(dir);

                if (!LooksLikeCulture(folderName))
                    continue;

                CultureInfo? culture = TryCreateCulture(folderName);
                if (culture is null) continue;

                if (HasResourcesForCulture(_resourceManager, culture) ||
                    HasResourcesForCulture(_countryNamesResourceManager, culture) ||
                    HasResourcesForCulture(_actionsResourceManager, culture))
                {
                    if (seen.Add(culture.Name))
                        cultures.Add(culture);
                }
            }

            // Replace invariant with your neutral language (en)
            var neutral = new CultureInfo(neutralCultureName);
            if (seen.Add(neutral.Name))
                cultures.Add(neutral);

            // Sort for stable output (optional)
            cultures = cultures
                .DistinctBy(c => c.Name)
                .OrderBy(c => c.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return cultures;
        }

        private static bool LooksLikeCulture(string name)
        {
            // Fast path to avoid exceptions; allows "xx" or "xx-YY"
            return name.Length is 2 or 5 && (char.IsLetter(name[0]) && char.IsLetter(name[1]));
        }

        private static CultureInfo? TryCreateCulture(string name)
        {
            try { return new CultureInfo(name); }
            catch (CultureNotFoundException) { return null; }
        }

        private static bool HasResourcesForCulture(ResourceManager manager, CultureInfo culture)
        {
            try
            {
                // 'tryParents: false' ensures we only count cultures that have actual satellite resources
                var rmSet = manager.GetResourceSet(culture, createIfNotExists: true, tryParents: false);
                return rmSet != null;
            }
            catch
            {
                return false;
            }
        }


    }
}
