using JetBrains.Annotations;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class FxInput : Input
    {
        public FxInput(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public override void InputText([NotNull] string text)
        {
            EnsureElementExistsAndExecute(
                x => x.FindElement(By.CssSelector("input")).SendKeys(text),
                string.Format("InputText('{0}')", text)
                );
        }

        public void SetDefault()
        {
            EnsureElementExistsAndExecute(
                element => element.FindElement(By.TagName("button")).Click(),
                "SetDefault"
                );
        }
    }
}