using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.RadioGroupTests
{
    [AutoFillControls]
    public class RadioGroupTestPage : ReactPage
    {
        public RadioGroup SimpleRadioGroup { get; private set; }
    }
}