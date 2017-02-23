using System.Linq;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting
{
    public class UniversalSelector : ISelector
    {
        public UniversalSelector(string selectorString)
        {
            this.selectorString = selectorString;
        }

        public static implicit operator UniversalSelector(string selectorString)
        {
            return new UniversalSelector(selectorString);
        }

        public By SeleniumBy
        {
            get
            {
                var selectorParts = selectorString.Split(' ');
                return By.CssSelector(string.Join(" ", selectorParts.Select(ToCssSelector)));
            }
        }

        private static string ToCssSelector(string improvedSelector)
        {
            if(improvedSelector.StartsWith("##"))
            {
                return string.Format("[data-tid='{0}']", improvedSelector.Replace("##", ""));
            }
            if(IsComponentNameSelector(improvedSelector))
            {
                return string.Format("[data-comp-name='{0}']", improvedSelector);
            }
            return IsTagNameSelector(improvedSelector) ? improvedSelector : improvedSelector;
        }

        public override string ToString()
        {
            return selectorString;
        }

        private static bool IsComponentNameSelector(string improvedSelector)
        {
            return char.IsLetter(improvedSelector[0]) && improvedSelector[0].ToString().ToUpper() == improvedSelector[0].ToString();
        }

        private static bool IsTagNameSelector(string improvedSelector)
        {
            return char.IsLetter(improvedSelector[0]) && improvedSelector[0].ToString().ToLower() == improvedSelector[0].ToString();
        }

        public bool MatchElement(IWebElement cachedContext)
        {
            return true;
        }

        private readonly string selectorString;
    }
}