using SKBKontur.SeleniumTesting.Tests.AutoFill;

namespace SKBKontur.SeleniumTesting.Tests.ModalTests
{
    [AutoFillControls]
    public class ModalsTestPage : ReactPage
    {
        public ModalWithStatelessComponentWithShowPropsCase ModalWithStatelessComponentWithShowPropsCase { get; private set; }
        public ModalWithStatefullComponentWithShowPropsCase ModalWithStatefullComponentWithShowPropsCase { get; private set; }
    }
}