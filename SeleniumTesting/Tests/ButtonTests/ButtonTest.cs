using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.ButtonTests
{
    [DefaultWaitInterval(2000)]
    public class ButtonTest : TestBase
    {
        public ButtonTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("Button").GetPageAs<ButtonTestPage>();
        }

        [Test]
        public void TestPresence()
        {
            page.SimpleButton.ExpectTo().BeDisplayed();
        }

        private ButtonTestPage page;
    }
}