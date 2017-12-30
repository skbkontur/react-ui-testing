using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests.TestEnvironment
{
    [TestFixture("15.4.2", "0.10.8")]
    [TestFixtureSource(typeof(RetailUIAndReactVersions))]
    public abstract class TestBase
    {
        protected TestBase(string reactVersion, string retailUiVersion, string minRetailUiVersion)
            : this(reactVersion, retailUiVersion)
        {
            if(new NpmLibVersion(retailUiVersion) < new NpmLibVersion(minRetailUiVersion))
            {
                //todo переделать на TestFixtureSource, когда в ReSharper починят работу с ним
                //https://youtrack.jetbrains.com/issues?q=TestFixtureSource
                Assert.Ignore($"Тест запускается для верссии react-ui начиная с {minRetailUiVersion}\n");
            }
        }

        protected TestBase(string reactVersion, string retailUiVersion)
        {
            this.reactVersion = reactVersion;
            this.retailUiVersion = retailUiVersion;
        }

        protected Browser OpenUrl(string url)
        {
            return BrowserSetUp.browser.OpenUrl($"{reactVersion}/{retailUiVersion}/{url}");
        }

        private readonly string reactVersion;
        private readonly string retailUiVersion;
    }
}
