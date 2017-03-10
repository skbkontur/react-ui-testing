using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.DatePickerTests
{
    [AutoFillControls]
    public class DatePickerTestPage : ReactPage
    {
        public DatePicker SimpleDatePicker { get; private set; }
    }
}