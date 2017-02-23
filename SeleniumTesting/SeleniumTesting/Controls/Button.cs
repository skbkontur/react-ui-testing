namespace SKBKontur.SeleniumTesting.Controls
{
    public class Button : ControlBase
    {
        public Button(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public bool Disabled { get { return GetReactProp<bool>("disabled"); } }
    }
}