using System;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    [Obsolete]
    public class PlacedInPortalCompoundControl : ControlBase, ISearchContainer
    {
        protected PlacedInPortalCompoundControl(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public virtual IWebElement Search(ISelector selector)
        {
            return container.SearchGlobal(selector);
        }

        public IWebElement SearchGlobal(ISelector selector)
        {
            return container.SearchGlobal(selector);
        }

        public object ExecuteScript(string script, params object[] arguments)
        {
            return container.ExecuteScript(script, arguments);
        }
    }
}