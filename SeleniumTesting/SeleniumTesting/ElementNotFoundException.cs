using System;

namespace SKBKontur.SeleniumTesting
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(ControlBase control, ISearchContainer container, Type getType, ISelector selector, Exception exception)
            : base(string.Format("Ёлемент {0} по правилу {1} не найден внутри элемента {2}", getType, selector, container.GetType().Name), exception)
        {
            Control = control;
            Container = container;
            ControlType = getType;
            ControlSelector = selector;
        }

        public ControlBase Control { get; set; }
        public ISearchContainer Container { get; set; }
        public Type ControlType { get; set; }
        public ISelector ControlSelector { get; set; }
    }
}