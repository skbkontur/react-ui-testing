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
                        var inputElement = x.FindElement(By.CssSelector("input"));
                        if(text == "")
                        {
                            inputElement.SendKeys(Keys.Control + "a");
                            inputElement.SendKeys(Keys.Backspace);
                        }
                        else
                        {
                            inputElement.Clear();
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
                        var element = x.FindElement(By.CssSelector("input"));
                        element.SendKeys(Keys.Control + "a");
                        element.SendKeys(Keys.Backspace);
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