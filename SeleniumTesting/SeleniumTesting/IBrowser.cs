using JetBrains.Annotations;

namespace SKBKontur.SeleniumTesting
{
    public interface IBrowser
    {
        [NotNull]
        T GetPageAs<T>() where T : PageBase;

        [NotNull]
        Browser OpenUrl([NotNull] string url);

        [NotNull]
        string GetCurrentUrl();

        void Close();
        void SaveScreenshot([NotNull] string testName);
        void SetAuthTokenCookie([NotNull] string cookieValue);
    }
}