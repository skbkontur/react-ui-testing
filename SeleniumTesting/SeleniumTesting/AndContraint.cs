namespace SKBKontur.SeleniumTesting
{
    public class AndContraint<T>
    {
        public AndContraint(T context)
        {
            this.context = context;
        }

        public T And { get { return context; } }
        private readonly T context;
    }
}