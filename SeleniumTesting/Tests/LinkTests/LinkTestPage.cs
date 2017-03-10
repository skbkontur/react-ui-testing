using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.LinkTests
{
    [AutoFillControls]
    public class LinkTestPage : ReactPage
    {
        public Link SimpleLink { get; private set; }
    }
}