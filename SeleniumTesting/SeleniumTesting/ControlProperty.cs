using System;

namespace SKBKontur.SeleniumTesting
{
    public static class ControlProperty
    {
        public static IControlProperty<T> Create<T>(Func<T> getValue, string description)
        {
            return new ControlPropertyImplementation<T>(getValue, description);
        }

        private class ControlPropertyImplementation<T> : IControlProperty<T>
        {
            public ControlPropertyImplementation(Func<T> getValue, string description)
            {
                this.getValue = getValue;
                this.description = description;
            }

            public T Get()
            {
                return getValue();
            }

            public string GetDescription()
            {
                return description;
            }

            private readonly Func<T> getValue;
            private readonly string description;
        }
    }
}
