using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class InternalCompoundControl : ControlBase, ISearchContainer
    {
        protected InternalCompoundControl(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public virtual IWebElement Search(ISelector selector)
        {
            return ExecuteOnElement(x => x.FindElement(selector.SeleniumBy));
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