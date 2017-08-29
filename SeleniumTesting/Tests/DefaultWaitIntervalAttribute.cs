using System;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Assertions.Context;

namespace SKBKontur.SeleniumTesting.Tests
{
    public class DefaultWaitIntervalAttribute : TestActionAttribute
    {
        public DefaultWaitIntervalAttribute(double milliseconds)
        {
            defaultWaitInterval = TimeSpan.FromMilliseconds(milliseconds);
        }

        public override ActionTargets Targets { get { return ActionTargets.Test; } }

        public override void BeforeTest(TestDetails testDetails)
        {
            base.BeforeTest(testDetails);
            previousWaitInterval = AssertionsContext.GetDefaultWaitInterval();
            AssertionsContext.SetDefaultWaitInterval(defaultWaitInterval);
        }

        public override void AfterTest(TestDetails testDetails)
        {
            AssertionsContext.SetDefaultWaitInterval(previousWaitInterval);
            base.AfterTest(testDetails);
        }

        private readonly TimeSpan defaultWaitInterval;
        private TimeSpan previousWaitInterval;
    }
}