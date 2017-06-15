using JetBrains.Annotations;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Input : ControlBase
    {
        public Input(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void AppendText(string keys)
        {
            ExecuteAction(x =>
                {
                    var element = x.FindElement(By.CssSelector("input"));
                    element.SendKeys(keys);
                }, string.Format("AppendText({0})", keys));
        }

        public void ClearAndInputText([NotNull] string text)
        {
            ExecuteAction(
                x =>
                    {
                        Clear();
                        if(text != "")
                        {
                            var inputElement = x.FindElement(By.CssSelector("input"));
                            inputElement.SendKeys(text);
                        }
                    },
                string.Format("InputText({0})", text));
        }

        public void Clear()
        {
            ExecuteAction(
                x =>
                    {
                        var inputElement = x.FindElement(By.CssSelector("input"));
                        inputElement.SendKeys(Keys.End);
                        var length = inputElement.GetAttribute("value").Length;
                        while(length > 0)
                        {
                            inputElement.SendKeys(Keys.Backspace);
                            length--;
                        }
                    },
                "Clear");
        }

        public string Value
        {
            get
            {
                return GetValueFromElement(x => ExecuteScript("return arguments[0].value", x.FindElement(By.CssSelector("input"))) as string);
            }
        }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}