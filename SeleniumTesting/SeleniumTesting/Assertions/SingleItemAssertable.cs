using System;

using SKBKontur.SeleniumTesting.Assertions.Context;
using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;

namespace SKBKontur.SeleniumTesting.Assertions
{
    public class SingleItemAssertable<T> : IAssertable<T> where T : ControlBase
    {
        public SingleItemAssertable(T subject, TimeSpan? waitInterval = null)
        {
            this.subject = subject;
            this.waitInterval = waitInterval ?? AssertionsContext.GetDefaultWaitInterval();
        }

        public void ExecuteAssert(Func<T, bool> action, Func<T, IErrorMessageBuilder, IErrorMessageBuilder> messageBuilder)
        {
            Waiter.Wait(() => action(subject), (timeout, exception) =>
                {
                    IErrorMessageBuilder m = new ErrorMessageBuilder();
                    m.WithSubject(subject).WithTimeout(timeout);
                    if(exception != null)
                    {
                        var notFoundException = exception as ElementNotFoundException;
                        if(notFoundException != null)
                        {
                            m = messageBuilder(notFoundException.Control as T, m);
                            m.WithFailedToFindControl(notFoundException);
                        }
                        else
                        {
                            throw new Exception(string.Format("Невозможно построить сообщений об ошибке. Произошла непредвиденная ошибка: {0}", exception), exception);
                        }
                    }
                    else
                    {
                        m = messageBuilder(subject, m);
                    }

                    return m.Build();
                }, waitInterval);
        }

        private readonly T subject;
        private readonly TimeSpan waitInterval;
    }
}