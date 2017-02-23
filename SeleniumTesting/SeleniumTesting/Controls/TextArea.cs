using System;

using JetBrains.Annotations;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class TextArea : ControlBase
    {
        public TextArea(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void InputText([NotNull] string text)
        {
            EnsureElementExistsAndExecute(x => { x.SendKeys(text); },
                                          string.Format("InputText({0})", text));
        }

        public void Clear()
        {
            EnsureElementExistsAndExecute(element => element.Clear(), "Clear");
        }

        [NotNull]
        [Obsolete]
        public override string GetText()
        {
            return ExecuteOnElement(element => element.GetAttribute("value"));
        }
    }
}