using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.DatePickerTests
{
    [DefaultWaitInterval(2000)]
    public class DatePickerTest : TestBase
    {
        public DatePickerTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("DatePicker").GetPageAs<DatePickerTestPage>();
        }

        [Test]
        public void TestPresence()
        {
            page.SimpleDatePicker.ExpectTo().BePresent();
        }

        private DatePickerTestPage page;
    }
}