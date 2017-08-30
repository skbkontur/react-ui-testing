using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.ComboBoxTests
{
    [AutoFillControls]
    public class ComboBoxesTestPage : ReactPage
    {
        public ComboBox SimpleComboBox { get; private set; }

        [Selector("##SimpleComboBox"), ChildSelector("noscript:portal MenuItem")]
        public ControlList<Label> SimpleComboBoxItems { get; private set; }

        public ComboBox ComboBoxNoPortal { get; private set; }
    }
}