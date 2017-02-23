using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Internals.Selectors
{
    internal class BySelector : ISelector
    {
        public BySelector(By by)
        {
            this.@by = @by;
        }

        public override string ToString()
        {
            return @by.ToString();
        }

        public By SeleniumBy { get { return @by; } }

        public bool MatchElement(IWebElement cachedContext)
        {
            return true;
        }

        private readonly By @by;
    }
}