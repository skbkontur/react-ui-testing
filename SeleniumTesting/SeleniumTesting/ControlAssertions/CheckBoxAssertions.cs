using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class CheckBoxAssertions : ControlBaseAssertions<CheckBox, CheckBoxAssertions>
    {
        public CheckBoxAssertions(IAssertable<CheckBox> subject)
            : base(subject)
        {
        }

        public AndContraint<CheckBoxAssertions> BeChecked()
        {
            return Checked.BeTrue();
        }

        public AndContraint<CheckBoxAssertions> BeUnchecked()
        {
            return Checked.BeFalse();
        }

        public PropertyControlContext<CheckBox, bool, CheckBoxAssertions> Checked { get { return HaveComplexProperty(x => x.IsChecked(), "checked"); } }
    }
}