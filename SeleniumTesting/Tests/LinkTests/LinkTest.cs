using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.LinkTests
{
    [DefaultWaitInterval(2000)]
    public class LinkTest : TestBase
    {
        public LinkTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("Link").GetPageAs<LinkTestPage>();
        }

        [Test]
        public void TestPresence()
        {
            page.SimpleLink.ExpectTo().BeDisplayed();
        }

        private LinkTestPage page;
    }
}