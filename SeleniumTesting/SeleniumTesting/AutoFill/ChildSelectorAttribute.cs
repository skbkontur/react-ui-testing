using System;

namespace SKBKontur.SeleniumTesting.AutoFill
{
    public class ChildSelectorAttribute : Attribute
    {
        public ChildSelectorAttribute(string selector)
        {
            Selector = new UniversalSelector(selector);
        }

        public ISelector Selector { get; private set; }
    }
}