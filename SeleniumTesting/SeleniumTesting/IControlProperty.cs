namespace SKBKontur.SeleniumTesting
{
    public interface IControlProperty<out T>
    {
        T Get();
        string GetDescription();
    }
}