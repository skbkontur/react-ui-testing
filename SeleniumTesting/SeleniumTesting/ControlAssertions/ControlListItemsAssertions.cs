using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class ControlListItemsAssertions<T> where T : ControlBase
    {
        public ControlListItemsAssertions(IAssertable<ControlList<T>> subject)
        {
            this.subject = subject;
        }

        public ControlListAnyOfItemAssertions<T> AnyOf()
        {
            return new ControlListAnyOfItemAssertions<T>(subject);
        }

        private readonly IAssertable<ControlList<T>> subject;
    }
}