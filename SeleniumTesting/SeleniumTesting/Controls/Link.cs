namespace SKBKontur.SeleniumTesting.Controls
{
    public class Link : ControlBase
    {
        public Link(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public IControlProperty<bool> IsDisabled => ReactProperty<bool>("disabled", null);
        public IControlProperty<string> Url => ValueFromElement(element => element.GetAttribute("href"));
        public IControlProperty<string> Text => ValueFromElement(element => element.Text);
    }
}