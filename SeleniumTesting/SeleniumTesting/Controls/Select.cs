using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using Newtonsoft.Json.Linq;

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
            this.portal = this.Find<Portal>().By("noscript");
        }

        [Obsolete]
        public void SetValue(object value)
        {
            SelectValueByValue(value);
        }

        public void SelectValueByText(string text)
        {
            Click();
            var controlList = portal.FindList().Of<Label>("MenuItem").By("Menu");
            controlList.ExpectTo().BePresent();
            controlList.First(x => x.TextObsolete == text).Click();
        }

        public void SelectValueByValue(object value)
        {
            Click();
            var items = GetReactProp<JArray>("items");
            var index = items.ToList().FindIndex(x => ElementMatchToValue(value, x));
            var controlList = portal.FindList().Of<Label>("MenuItem").By("Menu");
            controlList[index].ExpectTo().BePresent();
            controlList[index].Click();
        }

        private static bool ElementMatchToValue(object value, JToken x)
        {
            object actualValue = null;
            if(x is JArray)
            {
                if(x[0] is JValue)
                {
                    actualValue = ((JValue)x[0]).Value;
                }
            }
            else
            {
                if(x is JValue)
                {
                    actualValue = ((JValue)x).Value;
                }
            }
            if(actualValue == null)
            {
                return value == null;
            }
            return 
                actualValue.Equals(value) || 
                actualValue.ToString().Equals(value.ToString()) || 
                actualValue.ToString().ToLower().Equals(value.ToString().ToLower());
        }

        public string SelectedValueText { get { return GetValueFromElement(x => x.Text); } }
        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }

        [NotNull]
        public List<string> GetItems()
        {
            return GetReactProp<string[]>("items").ToList();
            //return ExecuteOnElement(x => x.FindElements(By.CssSelector("[data-comp-name='MenuItem']"))).Select(x => x.TextObsolete).ToList();
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

        public void SelectItemByIndex(int index)
        {
            ExecuteAction(x =>
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

        private readonly Portal portal;
    }
}