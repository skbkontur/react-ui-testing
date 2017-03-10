using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.TextAreaTests
{
    [AutoFillControls]
    public class TextAreaTestPage : ReactPage
    {
        public TextArea SimpleTextarea { get; private set; }
    }
}