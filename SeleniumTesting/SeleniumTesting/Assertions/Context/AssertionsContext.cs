using System;

using NUnit.Framework;

namespace SKBKontur.SeleniumTesting.Assertions.Context
{
    internal static class AssertionsContext
    {
        public static TimeSpan GetDefaultWaitInterval()
        {
            if(TestContext.CurrentContext != null)
            {
                var defaultIntervalFromContext = (TimeSpan?)TestContext.CurrentContext.Test.Properties["ReactWebTests_DefaultWaitInterval"];
                return defaultIntervalFromContext ?? defaultInterval;
            }
            return defaultInterval;
        }

        public static void SetDefaultWaitInterval(TimeSpan value)
        {
            TestContext.CurrentContext.Test.Properties["ReactWebTests_DefaultWaitInterval"] = value;
        }

        private static readonly TimeSpan defaultInterval = TimeSpan.FromSeconds(20);
    }
}