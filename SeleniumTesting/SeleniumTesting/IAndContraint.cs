namespace SKBKontur.SeleniumTesting
{
    public interface IAndContraint<out T>
    {
        T And { get; }
    }

    public class AndContraint<T> : IAndContraint<T>
    {
        public AndContraint(T context)
        {
            this.context = context;
        }

        public T And { get { return context; } }
        private readonly T context;
    }
}