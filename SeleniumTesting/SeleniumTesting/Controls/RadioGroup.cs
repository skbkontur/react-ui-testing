using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class RadioGroup : ControlBase
    {
        public RadioGroup(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void SelectItemById([NotNull] string id)
        {
            var index = GetReactProp<object[][]>("items").Select(x => x[0].ToString()).ToList().IndexOf(id);
            ExecuteOnElement(x => x.FindElements(By.CssSelector(string.Format("[data-comp-name='{0}']", "Radio")))).ElementAt(index).Click();
        }

        [CanBeNull]
        public string GetSelectedItemId()
        {
            return GetReactProp<string>("value");
        }

        [NotNull]
        public List<string> GetItems()
        {
            return GetReactProp<object[][]>("items").Select(x => x[0].ToString()).ToList();
        }
    }
}