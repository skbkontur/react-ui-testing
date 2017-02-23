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

        public virtual void InputText([NotNull] string text)
        {
            EnsureElementExistsAndExecute(x =>
                {
                    var element = x.FindElement(By.CssSelector("input"));
                    element.Clear();
                    element.SendKeys(text);
                }, string.Format("InputText({0})", text));
        }

        public string Value { get { return ExecuteOnElement(x => x.FindElement(By.CssSelector("input")).GetAttribute("value")); } }

        public bool Disabled { get { return GetReactProp<bool>("disabled"); } }
    }
}