using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using OpenQA.Selenium;

using SKBKontur.SeleniumTesting.Internals;
using SKBKontur.SeleniumTesting.Internals.Selectors;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class ComboBox : ControlBase
    {
        public ComboBox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void InputText([NotNull] string text)
        {
            EnsureElementExistsAndExecute(
                x =>
                    {
                        x.Click();
                        x.FindElement(By.CssSelector("input")).SendKeys(text);
                    },
                string.Format("InputText('{0}')", text));
        }

        [NotNull]
        public List<object> GetResults()
        {
            try
            {
                var renderContainer = GetRenderContainer();
                return renderContainer.FindElements(By.CssSelector(string.Format("[data-comp-name='{0}']", "MenuItem"))).Select(x => (object)x.Text).ToList();
            }
            catch(NoSuchElementException)
            {
                return new List<object>();
            }
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

        public void SelectByIndex(int index)
        {
            var renderContainer = GetRenderContainer();
            renderContainer.FindElements(By.CssSelector(string.Format("[data-comp-name='{0}']", "MenuItem")))
                           .Skip(index)
                           .FirstOrDefault()
                           .Click();
        }
    }
}