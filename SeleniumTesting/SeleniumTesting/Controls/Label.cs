namespace SKBKontur.SeleniumTesting.Controls
{
    public class Label : ControlBase
    {
        public Label(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }

        public string Text { get { return GetValueFromElement(element => element.Text); } }
    }
}
