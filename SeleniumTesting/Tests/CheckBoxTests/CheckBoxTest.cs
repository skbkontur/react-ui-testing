using NUnit.Framework;

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

        private CheckBoxTestPage page;
    }
}