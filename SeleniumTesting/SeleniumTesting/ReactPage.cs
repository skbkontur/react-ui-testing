using System;

using OpenQA.Selenium.Remote;

namespace SKBKontur.SeleniumTesting
{
    public class ReactPage : PageBase
    {
        public ReactPage()
            : base(null)
        {
        }

        internal void SetRemoteWebDriver(RemoteWebDriver newWebDriver)
        {
            if(webDriver != null)
            {
                throw new InvalidOperationException("SetRemoteWebDriver should be called once");
            }
            webDriver = newWebDriver;
        }
    }
}