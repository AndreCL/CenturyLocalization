using CenturyLocalization;
using CenturyLocalization.Actions;
using CenturyLocalization.CountryNames;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Xunit;

namespace LocalizationTests;

public class DataConsistencyTests
{
    [Fact]
    public void CheckForDuplicateResourceNames()
    {
        // Get all resource names from each ResourceManager
        var textsResourceNames = GetResourceNames(Texts.ResourceManager);
        var countryNamesResourceNames = GetResourceNames(CountryNames.ResourceManager);
        var actionsResourceNames = GetResourceNames(Actions.ResourceManager);

        // Create a dictionary to track resource names and their sources
        var resourceNameSources = new Dictionary<string, List<string>>();

        // Add all resource names from each source
        AddResourceNamesFromSource(resourceNameSources, textsResourceNames, "Texts");
        AddResourceNamesFromSource(resourceNameSources, countryNamesResourceNames, "CountryNames");
        AddResourceNamesFromSource(resourceNameSources, actionsResourceNames, "Actions");

        // Find duplicates
        var duplicates = resourceNameSources
            .Where(kvp => kvp.Value.Count > 1)
            .ToList();

        // Assert no duplicates exist
        if (duplicates.Count != 0)
        {
            var duplicateDetails = string.Join("\n", duplicates.Select(d =>
                $"'{d.Key}' found in: {string.Join(", ", d.Value)}"));

            Assert.Fail($"Found duplicate resource names:\n{duplicateDetails}");
        }
    }

    private static List<string> GetResourceNames(ResourceManager resourceManager)
    {
        var resourceNames = new List<string>();
        var resourceSet = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);

        if (resourceSet != null)
        {
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key is string key)
                {
                    resourceNames.Add(key);
                }
            }
        }

        return resourceNames;
    }

    private static void AddResourceNamesFromSource(Dictionary<string, List<string>> resourceNameSources,
        List<string> resourceNames, string source)
    {
        foreach (var resourceName in resourceNames)
        {
            if (!resourceNameSources.TryGetValue(resourceName, out List<string> value))
            {
                value = [];
                resourceNameSources[resourceName] = value;
            }

            value.Add(source);
        }
    }
}
