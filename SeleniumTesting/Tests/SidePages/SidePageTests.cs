using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;
using SKBKontur.SeleniumTesting.Tests.TestEnvironment;

namespace SKBKontur.SeleniumTesting.Tests.SidePages
{
    [DefaultWaitInterval(5000)]
    public class SidePageTests: TestBase
    {
        public SidePageTests(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion, "0.11.0")
        {
        }

        [SetUp]
        public void SetUp()
        {
            page = OpenUrl("SidePage").GetPageAs<SidePageTestPage>();
        }

        [Test]
        public void Test_Stateless_OpenAndClose()
        {
            page.OpenStateless.Click();
            page.StatelessSidePage.IsPresent.Wait().That(Is.True);
            page.StatelessSidePage.Header.Text.Wait().That(Is.EqualTo("Header"));

            page.StatelessSidePage.Close();
            page.StatelessSidePage.IsPresent.Wait().That(Is.False);
        }

        [Test]
        public void Test_Statefull_OpenAndClose()
        {
            page.OpenStatefull.Click();
            page.StatefullSidePage.IsPresent.Wait().That(Is.True);
            page.StatefullSidePage.Header.Text.Wait().That(Is.EqualTo("Header"));

            page.StatefullSidePage.Close();
            page.StatefullSidePage.IsPresent.Wait().That(Is.False);
        }

        private SidePageTestPage page;
    }
}