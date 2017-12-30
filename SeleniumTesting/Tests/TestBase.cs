using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.Helpers;

namespace SKBKontur.SeleniumTesting.Tests
{
    [TestFixture("15.4.2", "0.9.0")]
    [TestFixtureSource(typeof(RetailUIAndReactVersions))]
    public abstract class TestBase
    {
        protected TestBase(string reactVersion, string retailUiVersion, string minRetailUiVersion)
            : this(reactVersion, retailUiVersion)
        {
            if(new NpmLibVersion(retailUiVersion) < new NpmLibVersion(minRetailUiVersion))
            {
                //todo переделать на TestFixtureSource, когда в ReSharper починят работу с ним
                //https://youtrack.jetbrains.com/issues?q=TestFixtureSource
                Assert.Ignore($"Тест запускается для верссии react-ui начиная с {minRetailUiVersion}\n");
            }
        }

        protected TestBase(string reactVersion, string retailUiVersion)
        {
            this.reactVersion = reactVersion;
            this.retailUiVersion = retailUiVersion;
        }

        protected Browser OpenUrl(string url)
        {
            return BrowserSetUp.browser.OpenUrl($"{reactVersion}/{retailUiVersion}/{url}");
        }

        private readonly string reactVersion;
        private readonly string retailUiVersion;
    }

    public static class ProcessUtils
    {
        public static string[][] GetRetailAndReactVersions()
        {
            var p = new Process
                {
                    StartInfo =
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            FileName = Path.Combine(PathUtils.FindProjectRootFolder(), "printVersions.bat")
                        }
                };
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(output)
                              .SelectMany(x => x.Value.Select(y => new[] {x.Key, y}))
                              .ToArray();
        }
    }

    public class RetailUIAndReactVersions : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if(!TeamCityEnvironment.IsExecutionViaTeamCity)
            {
                yield break;
            }
            foreach(var versionPair in ProcessUtils.GetRetailAndReactVersions())
            {
                yield return versionPair;
            }
        }
    }
}