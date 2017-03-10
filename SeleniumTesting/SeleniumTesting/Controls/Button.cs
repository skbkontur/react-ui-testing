using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Button : ControlBase
    {
        public Button(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void ClickViaJavascript()
        {
            ExecuteAction(
                x => ExecuteScript("arguments[0].click();", x.FindElement(By.TagName("button"))),
                "ClickViaJavascript");
            
        }

        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
        public string Text { get { return GetValueFromElement(x => x.Text); } }
    }
}