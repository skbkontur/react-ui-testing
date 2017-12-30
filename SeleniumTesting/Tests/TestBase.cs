using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests;
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
            var output = GetOutputWithRetries();
            return JsonConvert.DeserializeObject<Dictionary<string, string[]>>(output)
                              .SelectMany(x => x.Value.Select(y => new[] {x.Key, y}))
                              .ToArray();
        }

        private static string GetOutputWithRetries()
        {
            for(var i = 0; i < 10; i++)
            {
                var result = GetOutput();
                if(string.IsNullOrWhiteSpace(result) || string.IsNullOrWhiteSpace(result.Trim()))
                {
                    continue;
                }
                return result;
            }
            throw new Exception("Cannot extract versions");
        }

        private static string GetOutput()
        {
            var p = new Process
                {
                    StartInfo =
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            FileName = Path.Combine(PathUtils.FindProjectRootFolder(), "printVersions.bat")
                        }
                };
            p.Start();
            var output = p.StandardOutput.ReadToEnd();
            var errorOutput = p.StandardError.ReadToEnd();
            p.WaitForExit();
            if(p.ExitCode != 0)
            {
                throw new Exception(errorOutput);
            }
            return output;
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
                yield return new TestFixtureData(versionPair[0], versionPair[1]);
            }
        }
    }
}