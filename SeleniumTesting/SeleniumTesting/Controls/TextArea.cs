using JetBrains.Annotations;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class TextArea : ControlBase
    {
        public TextArea(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void AppendText(string keys)
        {
            ExecuteAction(x =>
                {
                    var inputElement = GetInputElement(x);
                    inputElement.SendKeys(keys);
                }, string.Format("AppendText({0})", keys));
        }

        public void ClearAndInputText([NotNull] string text)
        {
            ExecuteAction(
                x =>
                    {
                        var inputElement = GetInputElement(x);
                        inputElement.Clear();
                        inputElement.SendKeys(text);
                    },
                string.Format("InputText({0})", text));
        }

        private static IWebElement GetInputElement(IWebElement x)
        {
            var inputElement = x;
            if(inputElement.TagName != "textarea")
            {
                inputElement = x.FindElement(By.TagName("textarea"));
            }
            return inputElement;
        }

        public void Clear()
        {
            ExecuteAction(
                x =>
                    {
                        var inputElement = GetInputElement(x);
                        inputElement.Clear();
                    },
                "Clear");
        }

        public string Value
        {
            get
            {
                return GetValueFromElement(x =>
                    {
                        var inputElement = GetInputElement(x);
                        return ExecuteScript("return arguments[0].value", inputElement) as string;
                    });
            }
        }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}