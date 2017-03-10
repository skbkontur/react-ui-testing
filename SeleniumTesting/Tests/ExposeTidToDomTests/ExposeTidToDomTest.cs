using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.ExposeTidToDomTests
{
    [DefaultWaitInterval(2000)]
    public class ExposeTidToDomTest : TestBase
    {
        public ExposeTidToDomTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("ExposeTidToDom").GetPageAs<ExposeTidToDomTestPage>();
        }

        [Test]
        public void TestSwithStateWithoutKeys()
        {
            RunTidSwitchCaseTest(page.SameDomElementCase);
        }

        [Test]
        public void TestSwithStateWithKeys()
        {
            RunTidSwitchCaseTest(page.SameDomElementWithKeyCase);
        }

        [Test]
        public void TestNestingComponents()
        {
            page.NestingComponentsCase.NestingComponentsContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 1");
            page.NestingComponentsCase.SwitchState.Click();
            page.NestingComponentsCase.NestingComponentsContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 2");
        }
        
        [Test]
        public void TestNestingDomElements()
        {
            page.NestingDomElementsCase.NestingDomContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 1");
            page.NestingDomElementsCase.SwitchState.Click();
            page.NestingDomElementsCase.NestingDomContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 2");
        }
 
        [Test]
        public void TestDoubleNestingComponents()
        {
            page.DoubleNestingComponentsCase.DoubleNestingContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 1");
            page.DoubleNestingComponentsCase.SwitchState.Click();
            page.DoubleNestingComponentsCase.DoubleNestingContainer
                .ExpectTo().BeDisplayed().And
                .Text.EqualTo("Вложение 2");
        }
        
        [Test]
        public void TestTable()
        {
            page.SimpleTable.Header1.ExpectTo().Text.EqualTo("Header 1");
            page.SimpleTable.Header2.ExpectTo().Text.EqualTo("Header 2");
            page.SimpleTable.Rows[0].Cell1.ExpectTo().Text.EqualTo("Cell 11");
            page.SimpleTable.Rows[0].Cell2.ExpectTo().Text.EqualTo("Cell 12");
            page.SimpleTable.Rows[1].Cell1.ExpectTo().Text.EqualTo("Cell 21");
            page.SimpleTable.Rows[1].Cell2.ExpectTo().Text.EqualTo("Cell 22");
            page.SimpleTable.Footer1.ExpectTo().Text.EqualTo("Footer 1");
            page.SimpleTable.Footer2.ExpectTo().Text.EqualTo("Footer 2");
        }

        [Test]
        public void TestDivInsideParagraph()
        {
            page.DivInsideParagraph.ExpectTo().Text.EqualTo("Value");
        }

        [Test]
        public void TestSwithTidOnSameComponent()
        {
            RunTidSwitchCaseTest(page.ChangeDataTidCase);
        }

        private static void RunTidSwitchCaseTest(SameDomElementCase changeStateCase)
        {
            changeStateCase.State1
                           .ExpectTo().BeDisplayed().And
                           .Text.Contain("Состояние 1").And
                                .Contain("Контент 1");
            changeStateCase.State2.ExpectTo().BeNotDisplayed();

            changeStateCase.SwitchState.Click();

            changeStateCase.State1.ExpectTo().BeNotDisplayed();
            changeStateCase.State2
                           .ExpectTo().BeDisplayed().And
                           .Text.Contain("Состояние 2").And
                                .Contain("Контент 2");
        }

        private ExposeTidToDomTestPage page;
    }
}