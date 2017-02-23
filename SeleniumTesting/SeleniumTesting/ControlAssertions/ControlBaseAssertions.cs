using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;

namespace SKBKontur.SeleniumTesting
{
    public class ControlBaseAssertions : ControlBaseAssertions<ControlBase, ControlBaseAssertions>
    {
        public ControlBaseAssertions(IAssertable<ControlBase> subject)
            : base(subject)
        {
        }

        public PropertyControlContext<ControlBase, string, ControlBaseAssertions> Text { get { return HaveComplexProperty(x => x.GetText(), "текст"); } }
    }
}