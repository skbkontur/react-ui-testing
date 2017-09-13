using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests
{
    [SetUpFixture]
    [SaveScreenshotOfFailure]
    public class BrowserSetUpFixture
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            browser = new Browser("localhost", "8083");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            browser.Close();
            browser = null;
        }

        public static Browser browser;
    }
}