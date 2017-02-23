using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class InputAssertions : ControlBaseAssertions<Input, InputAssertions>
    {
        public InputAssertions(IAssertable<Input> subject)
            : base(subject)
        {
        }

        public PropertyControlContext<Input, string, InputAssertions> Text { get { return HaveProperty(x => x.Value, "value"); } }

        public AndContraint<InputAssertions> BeDisabled()
        {
            return HaveProperty(x => x.Disabled, "disabled").BeTrue();
        }

        public AndContraint<InputAssertions> BeEnabled()
        {
            return HaveProperty(x => x.Disabled, "disabled").BeFalse();
        }
    }
}