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
            ExecuteAction(element => element.Click(), "Select");
        }

        public bool Selected
        {
            get
            {
                var isSelected = GetValueFromElement(element => element.GetAttribute("checked"));
                return isSelected == "true";
            }
        }
    }
}