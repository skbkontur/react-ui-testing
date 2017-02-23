using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;
using SKBKontur.SeleniumTesting.Controls;

namespace SKBKontur.SeleniumTesting
{
    public class LinkAssertions : ControlBaseAssertions<Link, LinkAssertions>
    {
        public LinkAssertions(IAssertable<Link> subject)
            : base(subject)
        {
        }

        public PropertyControlContext<Link, string, LinkAssertions> Text { get { return HaveComplexProperty(x => x.GetText(), "текст"); } }
    }
}