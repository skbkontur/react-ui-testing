using FluentAssertions;

using NUnit.Framework;

using SKBKontur.SeleniumTesting.Tests.PageActionAttributesTests.PageActionAttributes;

namespace SKBKontur.SeleniumTesting.Tests.PageActionAttributesTests
{
    public class PageActionAttributesTest : TestBase
    {
        public PageActionAttributesTest(string reactVersion, string retailUiVersion)
            : base(reactVersion, retailUiVersion)
        {
        }

        [Test]
        public void TestPageActionOrder()
        {
            TestPageActionAttribute.executedActions.Clear();
            OpenUrl("Input").GetPageAs<PageActionAttributeTestPage>();
            TestPageActionAttribute.executedActions.ShouldAllBeEquivalentTo(new[]
                {
                    "IPageInterface2",
                    "Base2PageBase",
                    "IPageInterface1",
                    "Base1PageBase",
                    "IPageInterface",
                    "Root",
                }, x => x.WithStrictOrdering());
        }
    }
}