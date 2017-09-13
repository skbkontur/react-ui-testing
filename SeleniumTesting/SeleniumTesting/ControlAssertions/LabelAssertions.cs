using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class LabelAssertions : ControlBaseAssertions<Label, LabelAssertions>
    {
        public LabelAssertions(IAssertable<Label> subject)
            : base(subject)
        {
        }

        public PropertyControlContext<Label, string> Text { get { return HaveProperty(x => x.TextObsolete, "текст"); } }
    }
}