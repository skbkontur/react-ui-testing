using System;

using NUnit.Framework;
using NUnit.Framework.Interfaces;

using SKBKontur.SeleniumTesting.Internals.Commons;

namespace SKBKontur.SeleniumTesting.Tests.Helpers
{
    public class SaveScreenshotOfFailureAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
        }

        public void AfterTest(ITest test)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var now = DateTime.Now;
                ScreenshotSaver.Save(Convert.FromBase64String(BrowserSetUpFixture.browser.GetScreenshot()), TestContext.CurrentContext.Test.FullName, now);
            }
        }

        public ActionTargets Targets { get { return ActionTargets.Test; } }
    }
}