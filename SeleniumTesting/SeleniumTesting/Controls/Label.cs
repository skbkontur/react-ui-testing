namespace SKBKontur.SeleniumTesting.Controls
{
    public class Label : ControlBase
    {
        public Label(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        public string Text { get { return ExecuteOnElement(element => element.Text); } }
    }
}