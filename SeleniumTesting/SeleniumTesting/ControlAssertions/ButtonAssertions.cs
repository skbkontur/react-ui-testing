using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class ButtonAssertions : ControlBaseAssertions<Button, ButtonAssertions>
    {
        public ButtonAssertions(IAssertable<Button> button)
            : base(button)
        {
        }

        public PropertyControlContext<Button, string, ButtonAssertions> Text { get { return HaveComplexProperty(x => x.GetText(), "text"); } }

        public AndContraint<ButtonAssertions> BeDisabled()
        {
            return HaveProperty(x => x.Disabled, "disabled").BeTrue();
        }

        public AndContraint<ButtonAssertions> BeEnabled()
        {
            return HaveProperty(x => x.Disabled, "disabled").BeFalse();
        }
    }
}