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
                    // Очень странная хрень. Очень редко код `element.SendKeys(Keys.Control + "a");` не срабатывает
                    CreateWebDriverActions().SendKeys(Keys.Control + "a").Perform();
                    element.SendKeys(Keys.Backspace);
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
                $"ClearAndInputText({text})"
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
                $"ClearAndInputDate({date})"
                );
        }

        public IControlProperty<string> Value => ValueFromElement(x => container.ExecuteScript("return arguments[0].value", x.FindElement(By.CssSelector("input"))) as string);
        public IControlProperty<bool> IsDisabled => ReactProperty<bool>("disabled");
    }
}