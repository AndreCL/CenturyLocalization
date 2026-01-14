using CenturyLocalization;
using System.IO;
using System.Linq;
using Xunit;

namespace LocalizationTests
{
    public class AvailableCulturesTests
    {

        [Fact]
        public void GetAvailableCultures_ReturnsExpectedCultures()
        {
            var local = new Localization();

            var cultures = local.GetAvailableCultures().Select(c => c.Name).ToList();

            // Assert that English exists (because invariant & en-US are in your tests)
            Assert.Contains("en", cultures);

            // Assert that Spanish exists (due to your “Ok_es” string test)
            Assert.Contains("es", cultures);
        }



        [Fact]
        public void GetAvailableCultures_IncludesNeutralEnglishInsteadOfInvariant()
        {
            var local = new Localization();
            var cultures = local.GetAvailableCultures().Select(c => c.Name).ToList();

            // Should include "en" (neutral) instead of invariant (“”)
            Assert.Contains("en", cultures);
            Assert.DoesNotContain("", cultures);
        }


        [Fact]
        public void GetAvailableCultures_DoesNotIncludeInvalidFolders()
        {
            var assemblyDir = Path.GetDirectoryName(typeof(Localization).Assembly.Location)!;
            var bogusDir = Path.Combine(assemblyDir, "not-a-culture");

            try
            {
                Directory.CreateDirectory(bogusDir);

                var local = new Localization();
                var cultures = local.GetAvailableCultures().Select(c => c.Name).ToList();

                Assert.DoesNotContain("not-a-culture", cultures);
            }
            finally
            {
                if (Directory.Exists(bogusDir))
                    Directory.Delete(bogusDir, true);
            }
        }


    }
}
