using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.RadioTests
{
    [AutoFillControls]
    public class RadioTestPage : ReactPage
    {
        public Radio SimpleRadio { get; private set; }
    }
}