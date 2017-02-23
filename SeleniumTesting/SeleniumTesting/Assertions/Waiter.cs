using System;
using System.Diagnostics;
using System.Threading;

using NUnit.Framework;

using OpenQA.Selenium;

using SKBKontur.SeleniumTesting.Internals.Commons;

namespace SKBKontur.SeleniumTesting.Assertions
{
    internal static class Waiter
    {
        public static void Wait(Func<bool> tryFunc, Func<TimeSpan, Exception, string> actionDescription, TimeSpan timeout)
        {
            timeout = IncreseFirstTimeoutIfNeedForTeamcity(timeout);
            DoWait(tryFunc, exception => Assert.Fail(actionDescription(timeout, exception)), timeout);
        }

        private static TimeSpan IncreseFirstTimeoutIfNeedForTeamcity(TimeSpan timeout)
        {
            if(TeamCityEnvironment.IsExecutionViaTeamCity)
            {
                if(TestContext.CurrentContext != null && TestContext.CurrentContext.Test != null && TestContext.CurrentContext.Test.FullName != null)
                {
                    if(firstTestName == null || firstTestName == TestContext.CurrentContext.Test.FullName)
                    {
                        timeout = TimeSpan.FromMilliseconds(timeout.TotalMilliseconds * firstTestTimeoutFactor);
                        firstTestName = TestContext.CurrentContext.Test.FullName;
                    }
                }
            }
            return timeout;
        }

        private static void DoWait(Func<bool> tryFunc, Action<Exception> doIfWaitFails, TimeSpan timeout)
        {
            Exception lastException = null;
            var w = Stopwatch.StartNew();
            do
            {
                try
                {
                    if(tryFunc())
                        return;
                }
                catch(StaleElementReferenceException e)
                {
                    throw e;
                }
                catch(Exception exception)
                {
                    lastException = exception;
                }
                Thread.Sleep(waitTimeout);
            } while(w.Elapsed < timeout);
            doIfWaitFails(lastException);
        }

        private const int waitTimeout = 100;
        private const int firstTestTimeoutFactor = 3;
        private static string firstTestName;
    }
}