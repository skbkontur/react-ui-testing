using SKBKontur.SeleniumTesting.Internals;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Checkbox : ControlBase
    {
        public Checkbox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
            Label = new Label(this, new UniversalSelector("*:first-child + * + *"));
        }

        public Label Label { get; private set; }

        public bool IsChecked { get { return GetReactProp<bool>("checked"); } }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}