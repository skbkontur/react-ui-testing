using System;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Assertions.Context;

namespace SKBKontur.SeleniumTesting
{
    public class DefaultWaitIntervalAttribute : TestActionAttribute
    {
        public DefaultWaitIntervalAttribute(TimeSpan defaultWaitInterval)
        {
            this.defaultWaitInterval = defaultWaitInterval;
        }

        public DefaultWaitIntervalAttribute(double milliseconds)
        {
            defaultWaitInterval = TimeSpan.FromMilliseconds(milliseconds);
        }

        public override ActionTargets Targets { get { return ActionTargets.Test; } }

        public override void BeforeTest(TestDetails testDetails)
        {
            base.BeforeTest(testDetails);
            AssertionsContext.SetDefaultWaitInterval(defaultWaitInterval);
        }

        private readonly TimeSpan defaultWaitInterval;
    }
}