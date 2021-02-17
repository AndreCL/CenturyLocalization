using System.Globalization;
using System.Resources;

namespace CenturyLocalization
{
	public class Localization
	{
		public CultureInfo CurrentLanguage { get; set; }

		private readonly ResourceManager _resourceManager;

		public Localization()
		{
			_resourceManager = Texts.ResourceManager;
			CurrentLanguage = CultureInfo.CurrentUICulture;
		}

		public string GetText(string name)
		{
			return _resourceManager.GetString(name, CurrentLanguage) ?? 
				_resourceManager.GetString("error_string_not_found", CurrentLanguage);
		}

		
	}
}
