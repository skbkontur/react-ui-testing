using System;

using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Tests.DatePickerTests
{
    [DefaultWaitInterval(2000)]
    public class DatePickerTest : TestBase
    {
        public DatePickerTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("DatePicker").GetPageAs<DatePickerTestPage>();
        }

        [Test]
        public void TestPresence()
        {
            page.SimpleDatePicker.ExpectTo().BePresent();
        }

        [Test]
        public void TestInputDateInEmpty()
        {
            page.SimpleDatePicker.ClearAndInputDate(new DateTime(2017, 11, 30, 12, 0, 0));
            Assert.AreEqual("30.11.2017", page.SimpleDatePicker.Value);
        }

        [Test]
        public void TestInputTextInEmpty()
        {
            page.SimpleDatePicker.ClearAndInputText("30.11.2017");
            Assert.AreEqual("30.11.2017", page.SimpleDatePicker.Value);
        }

        [Test]
        public void TestInputDateInFilled()
        {
            page.FilledDatePicker.ClearAndInputDate(new DateTime(2017, 11, 30, 12, 0, 0));
            Assert.AreEqual("30.11.2017", page.FilledDatePicker.Value);
        }

        [Test]
        public void TestInputTextInFilled()
        {
            page.FilledDatePicker.ClearAndInputText("30.11.2017");
            Assert.AreEqual("30.11.2017", page.FilledDatePicker.Value);
        }

        [Test]
        public void TestValueInFilled()
        {
            Assert.AreEqual("29.08.2016", page.FilledDatePicker.Value);
        }

        [Test]
        public void TestClearFilled()
        {
            page.FilledDatePicker.Clear();
            Assert.AreEqual(string.Empty, page.FilledDatePicker.Value);
        }

        [Test]
        public void TestDisabled()
        {
            Assert.AreEqual(true, page.DisabledDatePicker.IsDisabled);
        }

        [Test]
        public void TestNotDisabled()
        {
            Assert.AreEqual(false, page.FilledDatePicker.IsDisabled);
        }

        [Test]
        public void TestValueInDisabled()
        {
            Assert.AreEqual("31.12.2017", page.DisabledDatePicker.Value);
        }

        private DatePickerTestPage page;
    }
}