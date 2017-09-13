using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests
{
    [TestFixture("0.14.3", "0.6.10")]
    [TestFixture("15.3.0", "0.6.10")]
    [TestFixture("0.14.3", "0.7.4")]
    [TestFixture("15.3.0", "0.7.4")]
    [TestFixture("15.4.2", "0.9.0")]
    [TestFixture("16.0.0", "0.9.7")]
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
            return BrowserSetUpFixture.browser.OpenUrl(string.Format("{0}/{1}/{2}", reactVersion, retailUiVersion, url));
        }

        private readonly string reactVersion;
        private readonly string retailUiVersion;
    }
}