using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.PagingTests
{
    [AutoFillControls]
    public class PagingTestPage : ReactPage
    {
        public Paging Paging1 { get; private set; }
        public Paging Paging7 { get; private set; }
        public Paging Paging8 { get; private set; }
        public Paging Paging20 { get; private set; }
    }
}