using Kontur.Selone.Properties;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Link : ControlBase
    {
        public Link(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public IProp<bool> IsDisabled => ReactProperty<bool>("disabled", null);
        public IProp<string> Url => ValueFromElement(element => element.GetAttribute("href"));
        public IProp<string> Text => ValueFromElement(element => element.Text);
    }
}