using FluentAssertions;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;
using SKBKontur.SeleniumTesting.Tests.TestEnvironment;

namespace SKBKontur.SeleniumTesting.Tests.ComboBoxTests
{
    public class ComboBoxTest : TestBase
    {
        public ComboBoxTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("ComboBoxes").GetPageAs<ComboBoxesTestPage>();
        }

        [Test]
        public void TestEnterAndSelectValue()
        {
            page.SimpleComboBox.InputText("Item 1");
            page.SimpleComboBox.SelectByIndex(0);
            throw new AssertionException("Неоднозначность");
            //page.SimpleComboBox.ExpectTo().TextObsolete.EqualTo("Item 1");
        }

        [Test]
        public void TestSelectMultipleItems()
        {
            page.SimpleComboBox.InputText("Item");
            page.SimpleComboBox.SelectByIndex(5);
            throw new AssertionException("Неоднозначность");
            //page.SimpleComboBox.ExpectTo().TextObsolete.EqualTo("Item 6");
        }

        [Test]
        public void TestSelectViaCustomPortalSelector()
        {
            page.SimpleComboBox.Click();
            page.SimpleComboBoxItems.Count.Wait().That(Is.EqualTo(17));
        }

        [Test]
        public void Test_ComboBox_DisablePortal()
        {
            page.ComboBoxNoPortal.Click();
            page.ComboBoxNoPortal.GetResults().Count.Should().Be(17);
            page.ComboBoxNoPortal.InputTextAndSelectSingle("Item 1");
        }

        private ComboBoxesTestPage page;
    }
}