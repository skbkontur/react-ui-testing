namespace SKBKontur.SeleniumTesting.Controls
{
    public class MenuItem : ControlBase
    {
        public MenuItem(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public IControlProperty<string> Text => TextProperty();
    }
}