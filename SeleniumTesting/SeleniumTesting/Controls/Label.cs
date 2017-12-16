using System;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class Label : ControlBase
    {
        public Label(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public IControlProperty<bool> IsDisabled => ReactProperty<bool>("disabled");

        public IControlProperty<string> Text => TextProperty();

        [Obsolete]
        public string TextObsolete => Text.Get();
    }
}