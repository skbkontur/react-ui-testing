namespace SKBKontur.SeleniumTesting.Controls
{
    public class CompoundControl : InternalCompoundControl
    {
        protected CompoundControl(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
            ExecuteInitAction();
        }

        public string Text
        {
            get { return GetValueFromElement(x => x.Text); }
        }

        private void ExecuteInitAction()
        {
            foreach(var controlActionAttribute in GetType().GetCurrentTypeAttributes<ICompoundControlActionAttribute>())
                controlActionAttribute.OnInit(this);
        }
    }

    public interface ICompoundControlActionAttribute
    {
        void OnInit(CompoundControl control);
    }
}