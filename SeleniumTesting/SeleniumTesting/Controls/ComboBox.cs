using System;
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
            this.portal = this.Find<Portal>().By("noscript");
        }

        [Obsolete]
        public string Text { get { return GetValueFromElement(x => x.Text); } }

        public ControlListBase<T> GetItemsAs<T>(Func<ISearchContainer, ISelector, T> z) where T : ControlBase
        {
            return new ControlListBase<T>(portal, new UniversalSelector("Menu"), new UniversalSelector("MenuItem"), z);
        }

        public void InputTextAndSelectSingle(string inputText)
        {
            Click();
            InputText(inputText);
            GetItemsAs((x, y) => new Label(x, y)).ExpectTo().Count.GreaterThan(0);
            var result = GetItemsAs((x, y) => new Label(x, y)).First();
            if(result != null)
            {
                SelectByIndex(0);
            }
        }

        public void InputText([NotNull] string text)
        {
            ExecuteAction(
                x =>
                    {
                        x.Click();
                        x.FindElement(By.CssSelector("input")).Clear();
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
                var noScriptElement = GetValueFromElement(x => x.FindElement(By.CssSelector("noscript")));
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

        protected Portal portal;
    }
}