using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using OpenQA.Selenium;

using SKBKontur.SeleniumTesting.Internals;
using SKBKontur.SeleniumTesting.Internals.Selectors;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Select : ControlBase
    {
        public Select(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        [NotNull]
        public List<string> GetItems()
        {
            return GetReactProp<string[]>("items").ToList();
            //return ExecuteOnElement(x => x.FindElements(By.CssSelector("[data-comp-name='MenuItem']"))).Select(x => x.Text).ToList();
        }

        [CanBeNull]
        public string GetSelectedItemId()
        {
            var result = GetReactProp<string>("value");
            if(result == "undefined")
            {
                return null;
            }
            return result;
        }

        private IWebElement GetRenderContainer()
        {
            Waiter.Wait(() => GetRenderContainerInternal() != null, "Не появился RenderContainer");
            return GetRenderContainerInternal();
        }

        private IWebElement GetRenderContainerInternal()
        {
            try
            {
                var noScriptElement = ExecuteOnElement(x => x.FindElement(By.CssSelector("noscript")));
                var renderContainerId = noScriptElement.GetAttribute("data-render-container-id");
                var renderContainer = container.SearchGlobal(new BySelector(By.CssSelector(string.Format("[data-rendered-container-id='{0}']", renderContainerId))));
                return renderContainer;
            }
            catch
            {
                return null;
            }
        }

        public void SelectItemByIndex(int index)
        {
            EnsureElementExistsAndExecute(x =>
                {
                    x.Click();
                    var renderContainer = GetRenderContainer();
                    renderContainer.FindElements(By.CssSelector(string.Format("[data-comp-name='{0}']", "MenuItem")))
                                   .Skip(index)
                                   .FirstOrDefault()
                                   .Click();
                },
                                          string.Format("SelectItemByIndex({0})", index));
        }
    }
}