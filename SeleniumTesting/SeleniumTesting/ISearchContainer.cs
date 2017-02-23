using JetBrains.Annotations;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting
{
    public interface ISearchContainer
    {
        [NotNull]
        IWebElement Search([NotNull] ISelector selector);

        [NotNull]
        IWebElement SearchGlobal([NotNull] ISelector selector);

        [CanBeNull]
        object ExecuteScript([NotNull] string script, [NotNull] params object[] arguments);

        [NotNull]
        string GetAbsolutePathBySelectors();
    }
}