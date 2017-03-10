namespace SKBKontur.SeleniumTesting.Controls
{
    public class Checkbox : ControlBase
    {
        public Checkbox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
            Label = new Label(this, new UniversalSelector("span:nth-of-type(2)"));
        }

        public Label Label { get; private set; }

        public bool IsChecked { get { return GetReactProp<bool>("checked"); } }
    }
}