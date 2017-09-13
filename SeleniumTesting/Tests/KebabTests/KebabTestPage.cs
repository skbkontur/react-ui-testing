using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.KebabTests
{
    [AutoFillControls]
    public class KebabTestPage : ReactPage
    {
        public Kebab DisabledKebab { get; private set; }
        public Kebab SimpleKebab { get; private set; }
        public Label Output { get; private set; }
    }
}