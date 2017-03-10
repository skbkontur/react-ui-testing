using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.CheckBoxTests
{
    [AutoFillControls]
    public class CheckBoxTestPage : ReactPage
    {
        public Checkbox SimpleCheckbox { get; set; }
        public Checkbox CheckboxWithLabel { get; set; }
    }
}