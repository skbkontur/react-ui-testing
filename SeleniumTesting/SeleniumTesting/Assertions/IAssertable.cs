using System;

using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;

namespace SKBKontur.SeleniumTesting.Assertions
{
    public interface IAssertable<out T> where T : ControlBase
    {
        void ExecuteAssert(Func<T, bool> action, Func<T, IErrorMessageBuilder, IErrorMessageBuilder> messageBuilder);
    }
}