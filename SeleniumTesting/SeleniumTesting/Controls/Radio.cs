namespace SKBKontur.SeleniumTesting.Controls
{
    public class Radio : ControlBase
    {
        public Radio(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public void Select()
        {
            EnsureElementExistsAndExecute(element => element.Click(), "Select");
        }

        public bool IsSelected()
        {
            var isSelected = ExecuteOnElement(element => element.GetAttribute("checked"));
            return isSelected == "true";
        }
    }
}