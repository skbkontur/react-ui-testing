using SKBKontur.SeleniumTesting.Internals;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Checkbox : ControlBase
    {
        public Checkbox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
            if (GetRetailUiVersion().IsVersionSatisfyOrUndefined("<=0.7.6"))
                Label = new Label(this, new UniversalSelector("span:nth-of-type(2)"));
            else
                Label = new Label(this, new UniversalSelector("input + span + div"));
        }

        public Label Label { get; private set; }

        public bool IsChecked { get { return GetReactProp<bool>("checked"); } }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}
