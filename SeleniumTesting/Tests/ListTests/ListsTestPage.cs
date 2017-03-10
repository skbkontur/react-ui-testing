using SKBKontur.SeleniumTesting.Controls;
using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.ListTests
{
    [AutoFillControls]
    public class ListsTestPage : ReactPage
    {
        [ChildSelector("Input")]
        public ControlList<Input> InputWithoutTidList { get; set; }

        [ChildSelector("Input")]
        public ControlList<Input> NotExistentList { get; set; }

        [Selector("Case ##CompositeReadonlyElementList"), ChildSelector("##Item")]
        public ControlList<Item> CompositeReadonlyElementListCase { get; set; }
    }

    [AutoFillControls]
    public class Item : CompoundControl
    {
        public Item(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public Label Value1 { get; private set; }
        public Label Value2 { get; private set; }
        public Label NotExitingValue { get; private set; }
        public Label ExistsInSingleItem { get; private set; }
    }
}