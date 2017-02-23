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

        public void InputDate(DateTime date)
        {
            var element = ExecuteOnElement(x => x.FindElement(By.CssSelector("input")));
            element.Clear();
            element.SendKeys(date.ToUniversalTime().ToString("dd.MM.yyyy"));
            element.SendKeys("\t");
        }

        public DateTime GetSelectedDate()
        {
            return GetReactProp<DateTime>("value");
        }
    }
}