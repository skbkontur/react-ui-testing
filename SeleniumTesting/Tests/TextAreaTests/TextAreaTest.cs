using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.TextAreaTests
{
    [DefaultWaitInterval(2000)]
    public class TextAreaTest : TestBase
    {
        public TextAreaTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("Textarea").GetPageAs<TextAreaTestPage>();
        }

        [Test]
        public void TestPresence()
        {
            page.SimpleTextarea.ExpectTo().BePresent();
        }

        private TextAreaTestPage page;
    }
}