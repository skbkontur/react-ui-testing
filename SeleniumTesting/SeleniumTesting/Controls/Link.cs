using JetBrains.Annotations;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Link : ControlBase
    {
        public Link(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        [NotNull]
        public string GetUrl()
        {
            return ExecuteOnElement(element => element.GetAttribute("href"));
        }
    }
}