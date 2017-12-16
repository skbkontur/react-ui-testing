using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests.CheckBoxTests
{
    [DefaultWaitInterval(2000)]
    public class CheckBoxTest : TestBase
    {
        public CheckBoxTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("CheckBox").GetPageAs<CheckBoxTestPage>();
        }

        [Test]
        public void TestPresenceAndNoLabelVisible()
        {
            page.SimpleCheckbox.ExpectTo().BePresent();
            page.SimpleCheckbox.Label.ExpectTo().BeAbsent();
        }

        [Test]
        public void TestLabelPresenceOnCheckboxWithLabel()
        {
            page.CheckboxWithLabel.Label.ExpectTo().BePresent().And.Text.EqualTo("Checkbox label");
        }

        [Test]
        public void TestCheckboxDisabled()
        {
            page.CheckboxWithDisabledState.ExpectTo().BeEnabled();
            page.CheckboxToDisable.Label.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeDisabled();
        }

        [Test]
        public void TestCheckboxDisabledAndCheckedStates()
        {
            page.CheckboxWithDisabledState.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeEnabled().And.BeChecked();

            page.CheckboxToDisable.Label.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeDisabled().And.BeChecked();

            page.CheckboxWithDisabledState.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeDisabled().And.BeChecked();

            page.CheckboxToDisable.Label.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeEnabled().And.BeChecked();

            page.CheckboxWithDisabledState.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeEnabled().And.BeUnchecked();

            page.CheckboxToDisable.Label.Click();
            page.CheckboxWithDisabledState.ExpectTo().BeDisabled().And.BeUnchecked();
        }

        [Test]
        public void TestCheckboxDisabledNegative()
        {
            Following.CodeFails(() => page.CheckboxWithDisabledState.ExpectTo().BeDisabled());
            page.CheckboxToDisable.Click();
            Following.CodeFails(() => page.CheckboxWithDisabledState.ExpectTo().BeEnabled());
        }

        private CheckBoxTestPage page;
    }
}