using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.ButtonTests
{
    [AutoFillControls]
    public class ButtonTestPage : ReactPage
    {
        public Button SimpleButton { get; private set; }
    }
}