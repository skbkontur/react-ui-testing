using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class CheckBoxAssertions : ControlBaseAssertions<Checkbox, CheckBoxAssertions>
    {
        public CheckBoxAssertions(IAssertable<Checkbox> subject)
            : base(subject)
        {
        }

        public IAndContraint<IPropertyControlContext<bool>> BeChecked()
        {
            return Checked.BeTrue();
        }

        public IAndContraint<IPropertyControlContext<bool>> BeUnchecked()
        {
            return Checked.BeFalse();
        }

        public IAndContraint<CheckBoxAssertions> BeDisabled()
        {
            HaveProperty(x => x.IsDisabled, "disabled").BeTrue();
            return AndThis();
        }

        public IAndContraint<CheckBoxAssertions> BeEnabled()
        {
            HaveProperty(x => x.IsDisabled, "disabled").BeFalse();
            return AndThis();
        }

        public PropertyControlContext<Checkbox, bool> Checked { get { return HaveComplexProperty(x => x.IsChecked, "checked"); } }

        public PropertyControlContext<Checkbox, bool> Disabled { get { return HaveComplexProperty(x => x.IsDisabled, "disabled"); } }
    }
}