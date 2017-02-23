using System;

using JetBrains.Annotations;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SKBKontur.SeleniumTesting.Internals
{
    internal class RemoteWebDriverForAccessToProtected : RemoteWebDriver
    {
        public RemoteWebDriverForAccessToProtected([NotNull] Uri uri, [CanBeNull] ICapabilities desiredCapabilities)
            : base(uri, desiredCapabilities)
        {
        }

        [CanBeNull]
        public new string GetScreenshot()
        {
            return Execute(DriverCommand.Screenshot, null).Value.ToString();
        }
    }
}