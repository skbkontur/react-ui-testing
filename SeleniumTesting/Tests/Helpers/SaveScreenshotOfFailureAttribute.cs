using System;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Internals.Commons;

namespace SKBKontur.SeleniumTesting.Tests.Helpers
{
    public class SaveScreenshotOfFailureAttribute : Attribute, ITestAction
    {
        public void BeforeTest(TestDetails testDetails)
        {
        }

        public void AfterTest(TestDetails testDetails)
        {
            if(TestContext.CurrentContext.Result.Status == TestStatus.Failed)
            {
                var now = DateTime.Now;
                ScreenshotSaver.Save(Convert.FromBase64String(BrowserSetUpFixture.browser.GetScreenshot()), TestContext.CurrentContext.Test.FullName, now);
            }
        }

        public ActionTargets Targets { get { return ActionTargets.Test; } }
    }
}