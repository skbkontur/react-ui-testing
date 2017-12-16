using FluentAssertions;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests.ModalTests
{
    [DefaultWaitInterval(5000)]
    public class ModalsTest : TestBase
    {
        public ModalsTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("Modals").GetPageAs<ModalsTestPage>();
        }

        [Test]
        public void Test_ModalWithStatelessComponentWithShowPropsCase_OpenAndClose()
        {
            page.ModalWithStatelessComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
            page.ModalWithStatelessComponentWithShowPropsCase.Open.Click();
            page.ModalWithStatelessComponentWithShowPropsCase.Modal.ExpectTo().BePresent();
            page.ModalWithStatelessComponentWithShowPropsCase.Modal.CloseButton.Click();
            page.ModalWithStatelessComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
        }

        [Test]
        public void Test_ModalWithStatefullComponentWithShowPropsCase_OpenAndClose()
        {
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
            page.ModalWithStatefullComponentWithShowPropsCase.Open.Click();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BePresent();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.CloseButton.Click();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
        }

        [Test]
        public void Test_Present_ErrorMessage()
        {
            Following
                .Code(() => page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BePresent())
                .ShouldThrow<AssertionException>().Which.Message
                .Should().Be(To.Text(
                    @"TestModal(##ModalWithStatefullComponentWithShowPropsCase ##Modal): ожидалось присутствие",
                    @"Время ожидания: 5 секунд."
                                 ));
        }

        [Test]
        public void Test_Absent_ErrorMessage()
        {
            Following
                .Code(() =>
                    {
                        page.ModalWithStatefullComponentWithShowPropsCase.Open.Click();
                        page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
                    })
                .ShouldThrow<AssertionException>().Which.Message
                .Should().Be(To.Text(
                    @"TestModal(##ModalWithStatefullComponentWithShowPropsCase ##Modal): ожидалось отсутствие",
                    @"Время ожидания: 5 секунд."
                                 ));
        }

        [Test]
        public void TestModalHeader()
        {
            page.ModalWithStatefullComponentWithShowPropsCase.Open.Click();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.Header.ExpectTo().Text.Contain("Modal header");
        }

        [Test]
        public void TestCloseViaCloseButton()
        {
            page.ModalWithStatefullComponentWithShowPropsCase.Open.Click();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BePresent();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.Close();
            page.ModalWithStatefullComponentWithShowPropsCase.Modal.ExpectTo().BeAbsent();
        }

        private ModalsTestPage page;
    }
}