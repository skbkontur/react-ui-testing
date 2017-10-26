using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests
{
//    [TestFixture("0.14.3", "0.6.10")]
//    [TestFixture("15.3.0", "0.6.10")]
//    [TestFixture("0.14.3", "0.7.4")]
//    [TestFixture("15.3.0", "0.7.4")]
    [TestFixture("16.0.0", "0.9.7")]
    public abstract class TestBase
    {
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
