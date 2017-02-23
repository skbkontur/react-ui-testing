namespace SKBKontur.SeleniumTesting.Controls
{
    public class CheckBox : ControlBase
    {
        public CheckBox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public bool IsChecked()
        {
            return GetReactProp<bool>("checked");
        }
    }
}