using System;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using SKBKontur.SeleniumTesting.Internals;
using SKBKontur.SeleniumTesting.Internals.Commons;

namespace SKBKontur.SeleniumTesting
{
    public class Browser : IBrowser
    {
        public Browser(string defaultDomain, string defaultPort)
        {
            this.defaultDomain = defaultDomain;
            this.defaultPort = defaultPort;
        }

        public Browser OpenUrl(string url)
        {
            try
            {
                WebDriver.Navigate().GoToUrl(GetAbsoluteUrl(url));
                WebDriver.Manage().Cookies.AddCookie(new Cookie("testingMode", "1"));
                return this;
            }
            catch(Exception ex)
            {
                throw new Exception($"Can't open page with url={url}\r\n" + ex.Message);
            }
        }

        public string GetCurrentUrl()
        {
            return WebDriver.Url;
        }

        public void Close()
        {
            WebDriver.Close();
        }

        public void SaveScreenshot(string testName)
        {
            var screenshotBytesInBase64 = GetScreenshot();
            if(screenshotBytesInBase64 == null)
                throw new Exception("Can't take screenshot");
            ScreenshotSaver.Save(Convert.FromBase64String(screenshotBytesInBase64), testName, DateTime.Now);
        }

        public string GetScreenshot()
        {
            return WebDriver.GetScreenshot();
        }

        public void SetAuthTokenCookie(string cookieValue)
        {
            WebDriver.Manage().Cookies.AddCookie(new Cookie("auth.sid", cookieValue, "dev.kontur", "/", null));
        }

        public T GetPageAs<T>() where T : PageBase
        {
            return PageBase.InitializePage<T>(WebDriver);
        }

        private RemoteWebDriverForAccessToProtected WebDriver
        {
            get
            {
                if(webDriver != null) return webDriver;
                webDriver = new RemoteWebDriverForAccessToProtected(new Uri("http://localhost:9515"), GetChromeCapabilities());
                TryFixChromePosition();
                return webDriver;
            }
        }

        private static ICapabilities GetChromeCapabilities()
        {
            var assembliesDirectory = FindAssembliesDirectory();
            var chromePath = Path.Combine(assembliesDirectory, "Chrome", "chrome.exe");
            var chromeOptions = new ChromeOptions
                {
                    BinaryLocation = chromePath
                };
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            return chromeOptions.ToCapabilities();
        }

        private static string FindAssembliesDirectory()
        {
            var currentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            while(true)
            {
                if(currentDirectory == null)
                    throw new Exception("The folder Assemblies not found");
                var directories = currentDirectory.GetDirectories();
                foreach(var directoryInfo in directories)
                {
                    if(directoryInfo.Name == "Assemblies")
                        return directoryInfo.FullName;
                }
                currentDirectory = currentDirectory.Parent;
            }
        }

        private void TryFixChromePosition()
        {
            try
            {
                const string filename = @".chromeposition";
                var fileStream = File.OpenText(Path.Combine(PathUtils.FindContainingDirectory(filename), filename));
                var line = fileStream.ReadLine();
                if(line == null) return;
                // TODO Fix
                //var coordinates = line.Split(' ').Select(int.Parse).ToArray();
                // WebDriver.Manage().Window.Position = new Point(coordinates[0], coordinates[1]);
            }
            catch
            {
                // ignored
            }
            finally
            {
                WebDriver.Manage().Window.Maximize();
            }
        }

        private string GetAbsoluteUrl(string relativeUrl)
        {
            if(relativeUrl.StartsWith("http://") || relativeUrl.StartsWith("https://"))
                return relativeUrl;
            return $"http://{defaultDomain}:{defaultPort}/{relativeUrl}/";
        }

        private RemoteWebDriverForAccessToProtected webDriver;
        private readonly string defaultPort;
        private readonly string defaultDomain;
    }
}