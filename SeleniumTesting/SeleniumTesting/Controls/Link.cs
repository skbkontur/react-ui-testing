using JetBrains.Annotations;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Link : ControlBase
    {
        public Link(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
        public string Url { get { return GetValueFromElement(element => element.GetAttribute("href")); } }
        public string Text { get { return GetValueFromElement(element => element.Text); } }
    }
}