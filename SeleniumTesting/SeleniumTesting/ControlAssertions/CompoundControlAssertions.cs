using SKBKontur.SeleniumTesting.Assertions;
using SKBKontur.SeleniumTesting.Assertions.Bases;

namespace SKBKontur.SeleniumTesting
{
    public class CompoundControlAssertions<T> : ControlBaseAssertions<T, CompoundControlAssertions<T>> where T : ControlBase
    {
        public CompoundControlAssertions(IAssertable<T> subject)
            : base(subject)
        {
        }

        public PropertyControlContext<T, string, CompoundControlAssertions<T>> Text { get { return HaveComplexProperty(x => x.GetText(), "текст"); } }
    }
}