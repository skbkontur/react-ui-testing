using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests
{
    [SetUpFixture]
    [SaveScreenshotOfFailure]
    public class BrowserSetUpFixture
    {
        [SetUp]
        public void SetUp()
        {
            browser = new Browser("localhost", "8083");
        }

        [TearDown]
        public void TearDown()
        {
            browser.Close();
            browser = null;
        }

        public static Browser browser;
    }
}