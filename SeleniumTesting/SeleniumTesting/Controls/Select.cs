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

        public void SelectValueByText(string text, Timings timings = null)
        {
            Click();
            var controlList = portal.FindList().Of<Label>("MenuItem").By("Menu");
            controlList.IsPresent.Wait().That(x => x.AssertEqualTo(true), timings);
            controlList.First(x => x.Text.Get() == text).Click();
        }

        public void SelectValueByValue(object value, Timings timings = null)
        {
            Click();
            var items = GetReactProp<JArray>("items");
            var index = items.ToList().FindIndex(x => ElementMatchToValue(value, x));
            var controlList = portal.FindList().Of<Label>("MenuItem").By("Menu");
            controlList[index].IsPresent.Wait().That(x => x.AssertEqualTo(true), timings);
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

        public IControlProperty<string> SelectedValueText => ValueFromElement(x => x.Text);
        public IControlProperty<bool> IsDisabled => ReactProperty<bool>("disabled", null);

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
                var renderContainer = container.SearchGlobal(new BySelector(By.CssSelector($"[data-rendered-container-id='{renderContainerId}']")));
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
                    renderContainer.FindElements(By.CssSelector($"[data-comp-name='{"MenuItem"}']"))
                                   .Skip(index)
                                   .FirstOrDefault()
                                   .Click();
                },
                          $"SelectItemByIndex({index})");
        }

        private readonly Portal portal;
    }
}