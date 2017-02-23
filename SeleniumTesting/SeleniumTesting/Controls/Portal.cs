using OpenQA.Selenium;

using SKBKontur.SeleniumTesting.Internals.Selectors;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Portal : CompoundControl
    {
        public Portal(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        private IWebElement GetPortalElement()
        {
            return ExecuteOnElement(x =>
                {
                    var renderContainerId = x.GetAttribute("data-render-container-id");
                    try
                    {
                        return container.SearchGlobal(new BySelector(By.CssSelector(string.Format("[data-rendered-container-id='{0}']", renderContainerId))));
                    }
                    catch(NoSuchElementException)
                    {
                        return null;
                    }
                });
        }

        public override bool IsDisplayed()
        {
            try
            {
                return ExecuteOnElement(x => x != null) && GetPortalElement() != null;
            }
            catch
            {
                return false;
            }
        }

        public override IWebElement Search(ISelector selector)
        {
            return ExecuteOnElement(x =>
                {
                    var renderContainerId = x.GetAttribute("data-render-container-id");
                    var portal = container.SearchGlobal(new BySelector(By.CssSelector(string.Format("[data-rendered-container-id='{0}']", renderContainerId))));
                    return portal.FindElement(selector.SeleniumBy);
                });
        }
    }
}