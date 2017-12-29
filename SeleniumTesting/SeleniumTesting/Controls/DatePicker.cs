using System;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class DatePicker : ControlBase
    {
        public DatePicker(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void Clear()
        {
            ExecuteAction(x =>
                {
                    var element = x.FindElement(By.CssSelector("input"));
                    element.SendKeys(Keys.End);
                    var length = element.GetAttribute("value").Length;
                    while(length > 0)
                    {
                        element.SendKeys(Keys.Backspace);
                        length--;
                    }
                    element.SendKeys(Keys.Tab);
                }, "Clear");
        }

        public void ClearAndInputText(string text)
        {
            ExecuteAction(
                x =>
                    {
                        var element = x.FindElement(By.CssSelector("input"));
                        element.Clear();
                        element.SendKeys(text);
                        element.SendKeys(Keys.Tab);
                    },
                string.Format("ClearAndInputText({0})", text)
                );
        }

        public void ClearAndInputDate(DateTime date)
        {
            ExecuteAction(
                x =>
                    {
                        var element = x.FindElement(By.CssSelector("input"));
                        element.Clear();
                        element.SendKeys(date.ToUniversalTime().ToString("dd.MM.yyyy"));
                        element.SendKeys(Keys.Tab);
                    },
                string.Format("ClearAndInputDate({0})", date)
                );
        }

        public string Value { get { return GetValueFromElement(x => container.ExecuteScript("return arguments[0].value", x.FindElement(By.CssSelector("input"))) as string); } }
        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}