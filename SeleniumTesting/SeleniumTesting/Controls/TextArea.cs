using JetBrains.Annotations;

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
                    var element = x;
                    element.SendKeys(keys);
                }, string.Format("AppendText({0})", keys));
        }

        public void ClearAndInputText([NotNull] string text)
        {
            ExecuteAction(
                x =>
                    {
                        var inputElement = x;
                        inputElement.Clear();
                        inputElement.SendKeys(text);
                    },
                string.Format("InputText({0})", text));
        }

        public void Clear()
        {
            ExecuteAction(
                x =>
                    {
                        var element = x;
                        element.Clear();
                    },
                "Clear");
        }

        public string Value { get { return GetValueFromElement(x => ExecuteScript("return arguments[0].value", x) as string); } }
        public bool IsDisabled { get { return GetReactProp<bool>("disabled"); } }
    }
}