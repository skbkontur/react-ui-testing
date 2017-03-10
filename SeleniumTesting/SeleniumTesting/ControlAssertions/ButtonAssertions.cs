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

        public PropertyControlContext<Button, string> Text { get { return HaveComplexProperty(x => x.Text, "text"); } }

        public IAndContraint<ButtonAssertions> BeDisabled()
        {
            HaveProperty(x => x.IsDisabled, "disabled").BeTrue();
            return AndThis();
        }

        public IAndContraint<ButtonAssertions> BeEnabled()
        {
            HaveProperty(x => x.IsDisabled, "disabled").BeFalse();
            return AndThis();
        }
    }
}