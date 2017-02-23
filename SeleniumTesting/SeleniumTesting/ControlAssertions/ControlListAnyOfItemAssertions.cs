using System;
using System.Linq;

using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class ControlListAnyOfItemAssertions<T> : IAssertable<T> where T : ControlBase
    {
        public ControlListAnyOfItemAssertions(IAssertable<ControlList<T>> subject)
        {
            this.subject = subject;
        }

        public void ExecuteAssert(Func<T, bool> action, Func<T, IErrorMessageBuilder, IErrorMessageBuilder> messageBuilder)
        {
            subject.ExecuteAssert(
                x => x.GetItems().Any(action),
                (x, me) =>
                    {
                        me.WithListQuantifier(string.Format("для одного из {0}", x.GetRelativePathToItems()));
                        if(!x.IsDisplayed())
                        {
                            messageBuilder(null, me);
                        }
                        else
                        {
                            foreach(var item in x.GetItems())
                            {
                                messageBuilder(item, me);
                            }
                        }
                        return me;
                    }
                );
        }

        private readonly IAssertable<ControlList<T>> subject;
    }
}