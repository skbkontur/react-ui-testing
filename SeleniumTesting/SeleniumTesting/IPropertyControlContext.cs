using System;

using SKBKontur.SeleniumTesting.Assertions.ErrorMessages;

namespace SKBKontur.SeleniumTesting
{
    public interface IPropertyControlContext<out T, TAssertions>
    {
        AndContraint<TAssertions> ExecuteAssert(Func<T, bool> func, Func<IErrorMessageBuilder, IErrorMessageBuilder> messageBuilder);
    }
}