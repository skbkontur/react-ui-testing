using OpenQA.Selenium.Remote;

using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.DatePickerTests
{
    [AutoFillControls]
    public class DatePickerTestPage : PageBase
    {
        public DatePickerTestPage(RemoteWebDriver webDriver)
            : base(webDriver)
        {
        }

        public DatePicker SimpleDatePicker { get; private set; }
    }
}